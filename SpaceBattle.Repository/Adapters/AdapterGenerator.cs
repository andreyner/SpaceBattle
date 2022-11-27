using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Runtime.Loader;
using System.Reflection;
using Microsoft.CodeAnalysis;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;

namespace SpaceBattle.Repository.Adapters
{
	public class CodeGenerator
	{

		public static object CreateAdapter(Type target, params object[] paramArray)
		{
			string[] trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

			var references = new List<PortableExecutableReference>();

			foreach (var refAsm in trustedAssembliesPaths)
			{
				references.Add(MetadataReference.CreateFromFile(refAsm));
			}

			var prefix = target.Name.Remove(0, 1);

			var className = $"{prefix}Adapter";

			var classBody = String.Empty;

			var properties = target.GetProperties();

			foreach (var property in properties)
			{
				var propBody = string.Empty;

				if (property.CanRead)
				{
					propBody = String.Join(Environment.NewLine, propBody, "get { return IoC.Resolve<" + $"{property.PropertyType.Name}>(\"{prefix}.{property.Name}.Get\", _obj);" + "}");
				}

				if (property.CanWrite)
				{
					propBody = String.Join(Environment.NewLine, propBody, "set { IoC.Resolve<" + $"{property.PropertyType.Name}>(\"{prefix}.{property.Name}.Set\", _obj, \"{property.Name}\", value);" + "}");
				}

				var propName = $"public {property.PropertyType.Name} {property.Name}";

				classBody = String.Join(Environment.NewLine, classBody, propName, "{", propBody, "}");

			}

			var adapterBody = String.Join(Environment.NewLine,
							"using System;",
							$"using {target.GetTypeInfo().Namespace};",
							"using SpaceBattle.Repository.Container;",
							$"public class {className} : {target.Name}",
								"{",
									"Uobject _obj;",
									$"public {className}(Uobject obj)",
									"{",
										"_obj = obj;",
									"}",
									$"{classBody}",
								"}");


			var compilation = CSharpCompilation.Create("a")
			   .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
			   .AddReferences(MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location))
			   .AddReferences(MetadataReference.CreateFromFile(target.GetTypeInfo().Assembly.Location))
			   .AddReferences(references)
			   .AddSyntaxTrees(CSharpSyntaxTree.ParseText(adapterBody));

			using var assemblyLoadContext = new CollectibleAssemblyLoadContext();
			using var ms = new MemoryStream();

			var cr = compilation.Emit(ms);
			if (!cr.Success)
			{
				throw new InvalidOperationException("Error in expression: " + cr.Diagnostics.First(e =>
					e.Severity == DiagnosticSeverity.Error).GetMessage());
			}

			ms.Seek(0, SeekOrigin.Begin);
			var assembly = assemblyLoadContext.LoadFromStream(ms);

			var outerClassType = assembly.GetType($"{className}");

			var obj = Activator.CreateInstance(outerClassType, paramArray);

			return obj;
		}


		private class CollectibleAssemblyLoadContext : AssemblyLoadContext, IDisposable
		{
			public CollectibleAssemblyLoadContext() : base(true)
			{ }

			protected override Assembly Load(AssemblyName assemblyName)
			{
				return null;
			}

			public void Dispose()
			{
				Unload();
			}
		}
	}

}
