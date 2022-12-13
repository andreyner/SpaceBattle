using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceBattle.GameAgent
{
	public class RabbitMqService : IRabbitMqService
	{
		public void SendMessage(string message)
		{

			var factory = new ConnectionFactory() {
				Uri = new Uri("amqps://csqawfil:EmAa2oBZLCtFPpEsewxt8_PR1mF-dA8s@hawk.rmq.cloudamqp.com/csqawfil")
			};
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "MyQueue",
							   durable: false,
							   exclusive: false,
							   autoDelete: false,
							   arguments: null);

				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
							   routingKey: "MyQueue",
							   basicProperties: null,
							   body: body);
			}
		}

		public void SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message);
		}
	}
}
