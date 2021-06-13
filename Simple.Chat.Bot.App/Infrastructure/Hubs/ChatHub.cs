using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Simple.Chat.Bot.App.Data;
using Simple.Chat.Bot.App.Models;
using Simple.Chat.Bot.App.ViewModels;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
  public class ChatHub : Hub
  {
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;
    public ChatHub(UserManager<User> userManager, ApplicationDbContext context)
    {
      _userManager = userManager;
      _context = context;
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

    public async Task SendMessage(string message)
    {
      var user = await _userManager.GetUserAsync(Context.User);
      var datePosted = DateTime.Now;
      await SaveMessage(message, datePosted, user.Id);
      var chatMessage = new ChatMessageViewModel
      {
        DatePosted = datePosted,
        Message = message,
        Nickname = user.Nickname
      };
      await Clients.All.SendAsync("ReceiveMessage", chatMessage);
    }
  }
}