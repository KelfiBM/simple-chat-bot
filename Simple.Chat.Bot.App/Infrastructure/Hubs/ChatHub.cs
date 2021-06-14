using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Simple.Chat.Bot.App.Data;
using Simple.Chat.Bot.App.Helpers;
using Simple.Chat.Bot.App.Infrastructure.CommandWorker;
using Simple.Chat.Bot.App.Models;
using Simple.Chat.Bot.App.ViewModels;
using System;
using System.Threading.Tasks;

namespace Simple.Chat.Bot.App.Infrastructure.Hubs
{
  public class ChatHub : Hub
  {
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IRabbitMQService _rabbitMQService;
    public ChatHub(
      UserManager<User> userManager,
      ApplicationDbContext context,
      IRabbitMQService rabbitMQService)
    {
      _userManager = userManager;
      _context = context;
      _rabbitMQService = rabbitMQService;
    }

    private async Task SaveMessage(string message, DateTime datePosted, string userId)
    {
      var model = new ChatMessage
      {
        Message = message,
        DatePosted = datePosted,
        UserId = userId
      };
      await _context.AddAsync(model);
      await _context.SaveChangesAsync();
    }

    private void SendCommand(string message)
    {
      var command = CommandParser.GetCommand(message);
      _rabbitMQService.SendCommand(command);
    }

    public async Task SendMessage(string message)
    {
      if (CommandParser.IsCommand(message))
      {
        SendCommand(message);
        return;
      }

      var user = await _userManager.GetUserAsync(Context.User);
      var datePosted = DateTime.Now;
      var chatMessage = new ChatMessageViewModel
      {
        DatePosted = datePosted,
        Message = message,
        Nickname = user.Nickname
      };
      await SaveMessage(message, datePosted, user.Id);
      await PublishMessage(chatMessage);
    }

    private async Task PublishMessage(ChatMessageViewModel chatMessage)
    {
      await Clients.All.SendAsync("ReceiveMessage", chatMessage);
    }
  }
}