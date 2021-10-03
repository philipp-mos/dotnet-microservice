using System;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            
            int.TryParse(_configuration["RabbitMQPost"], out int connectionPort);
            var factory = new ConnectionFactory() 
                { HostName =  _configuration["RabbitMQHost"], Port = connectionPort };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }
            catch(Exception ex)
            {
                
            }
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {

        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {

        }
    }
}