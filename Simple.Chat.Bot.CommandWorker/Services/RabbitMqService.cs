using System;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Simple.Chat.Bot.CommandWorker.Helpers;
using Simple.Chat.Bot.CommandWorker.Infrastructure.Commands;

namespace Simple.Chat.Bot.CommandWorker.Services
{
  public interface IRabbitMQService
  {
    void Connect();
    void SendResponse(string message);
  }
  public class RabbitMQService : IRabbitMQService
  {
    protected readonly ConnectionFactory _factory;
    protected readonly IConnection _connection;
    protected readonly IModel _channel;
    protected readonly IConfiguration _configuration;
    protected readonly ConnectionFactorySettings _connectionFactorySettings;
    protected readonly ICommandProcessor _commandProcessor;
    public RabbitMQService(IConfiguration configuration, ICommandProcessor commandProcessor)
    {
      // Opens the connections to RabbitMQ
      _configuration = configuration;
      _connectionFactorySettings = _configuration.GetSection("RabbitMQSettings:ConnectionFactorySettings").Get<ConnectionFactorySettings>();
      _factory = new ConnectionFactory()
      {
        HostName = _connectionFactorySettings.HostName,
        UserName = _connectionFactorySettings.UserName,
        Password = _connectionFactorySettings.Password,
        Port = _connectionFactorySettings.Port
      };
      _connection = _factory.CreateConnection();
      _channel = _connection.CreateModel();
      _commandProcessor = commandProcessor;
    }

    public virtual void Connect()
    {
      // Declare a RabbitMQ Queue
      _channel.QueueDeclare(queue: _connectionFactorySettings.CommandQueue, durable: false, exclusive: false, autoDelete: false);
      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += delegate (object model, BasicDeliverEventArgs ea)
      {
        var command = Encoding.UTF8.GetString(ea.Body.ToArray());
        var response = _commandProcessor.GetCommandResponse(command);
        SendResponse(response);
      };
      _channel.BasicConsume(queue: _connectionFactorySettings.CommandQueue, autoAck: true, consumer: consumer);
    }

    public void SendResponse(string message)
    {
      _channel.QueueDeclare(queue: _connectionFactorySettings.MessageQueue, durable: true, exclusive: false, autoDelete: false);
      var body = Encoding.UTF8.GetBytes(message);
      _channel.BasicPublish(exchange: "", routingKey: _connectionFactorySettings.MessageQueue, basicProperties: null, body: body);
    }

  }
}

