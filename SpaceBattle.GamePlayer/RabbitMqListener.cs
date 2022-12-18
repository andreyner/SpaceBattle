using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpaceBattle.Model;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.RabbitMq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.GameServer
{
	public class RabbitMqListener : BackgroundService
	{
		private IConnection _connection;
		private IModel _channel;

		public RabbitMqListener()
		{
			var factory = new ConnectionFactory { Uri = new Uri("amqps://csqawfil:EmAa2oBZLCtFPpEsewxt8_PR1mF-dA8s@hawk.rmq.cloudamqp.com/csqawfil") };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (ch, ea) =>
			{
				_channel.BasicAck(ea.DeliveryTag, false);

				var content = Encoding.UTF8.GetString(ea.Body.ToArray());

				var message = JsonConvert.DeserializeObject<CommandMessage>(content);

				var currentQueue = IoC.Resolve<ConcurrentQueue<ICommand>>("QueueCommand.Get");

				currentQueue.Enqueue(new InetrpretCommand(message));

			};

			_channel.BasicConsume("MyQueue", false, consumer);

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_channel.Close();
			_connection.Close();
			base.Dispose();
		}
	}
}
