using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simple.Chat.Bot.App.Models;

namespace Simple.Chat.Bot.App.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<ChatMessage>().HasKey(x => x.Id);
      builder.Entity<ChatMessage>().HasOne(x => x.User).WithMany(x => x.ChatMessages).IsRequired();
      builder.Entity<User>().HasIndex(x => x.Nickname).IsUnique();
    }
  }
}
