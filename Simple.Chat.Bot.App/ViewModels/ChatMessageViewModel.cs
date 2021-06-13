using System;

namespace Simple.Chat.Bot.App.ViewModels
{
  public class ChatMessageViewModel
  {
    public DateTime DatePosted { get; set; }
    public string DatePostedFormatted
    {
      get
      {
        return DatePosted.ToString("MM/dd/yyyy, HH:mm:ss");
      }
    }
    public string Message { get; set; }
    public string Nickname { get; set; }
  }
}