using System;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Simple.Chat.Bot.App.Helpers.ModelSettings;
using Simple.Chat.Bot.App.Infrastructure.Hubs;
using Simple.Chat.Bot.App.ViewModels;

namespace Simple.Chat.Bot.App.Infrastructure.CommandWorker
{
  public interface IRabbitMQService
  {
    void Connect();
    void SendCommand(string command);
  }
  public class RabbitMQService : IRabbitMQService
  {
    protected readonly ConnectionFactory _factory;
    protected readonly IConnection _connection;
    protected readonly IModel _channel;
    protected readonly IConfiguration _configuration;
    protected readonly IServiceProvider _serviceProvider;
    protected readonly ConnectionFactorySettings _connectionFactorySettings;

    public RabbitMQService(IServiceProvider serviceProvider, IConfiguration configuration)
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

      _serviceProvider = serviceProvider;
    }

    public virtual void Connect()
    {
      // Declare a RabbitMQ Queue
      _channel.QueueDeclare(queue: _connectionFactorySettings.MessageQueue, durable: true, exclusive: false, autoDelete: false);

      var consumer = new EventingBasicConsumer(_channel);

      // When we receive a message from SignalR
      consumer.Received += delegate (object model, BasicDeliverEventArgs ea)
      {
        // Get the ChatHub from SignalR (using DI)
        var chatHub = (IHubContext<ChatHub>)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));
        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
        // Send message to all users in SignalR
        var chatMessage = new ChatMessageViewModel
        {
          DatePosted = DateTime.Now,
          Message = message,
          Nickname = "Simple Bot"
        };
        chatHub.Clients.All.SendAsync("ReceiveMessage", chatMessage);

      };

      // Consume a RabbitMQ Queue
      _channel.BasicConsume(queue: _connectionFactorySettings.MessageQueue, autoAck: true, consumer: consumer);
    }

    public void SendCommand(string command)
    {
      _channel.QueueDeclare(queue: _connectionFactorySettings.CommandQueue, exclusive: false, autoDelete: false);
      var body = Encoding.UTF8.GetBytes(command);
      _channel.BasicPublish(exchange: "", routingKey: _connectionFactorySettings.CommandQueue, basicProperties: null, body: body);
    }
  }
}

