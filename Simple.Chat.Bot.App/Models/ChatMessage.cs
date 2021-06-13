using System;
using System.ComponentModel.DataAnnotations;

namespace Simple.Chat.Bot.App.Models
{
  public class ChatMessage
  {

    public int Id { get; set; }
    [Required]
    public DateTime DatePosted { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public string UserId { get; set; }
    public virtual User User { get; set; }
  }
}