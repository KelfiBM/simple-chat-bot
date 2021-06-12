using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Simple.Chat.Bot.App.Models
{
  public class User : IdentityUser
  {
    [Required]
    [Display(Name = "Nickname")]
    public string Nickname { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
  }
}