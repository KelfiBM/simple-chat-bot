using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simple.Chat.Bot.App.Models;
using Microsoft.AspNetCore.Authorization;
using Simple.Chat.Bot.App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Simple.Chat.Bot.App.ViewModels;

namespace Simple.Chat.Bot.App.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public HomeController(
        ILogger<HomeController> logger,
        ApplicationDbContext context,
        UserManager<User> userManager)
    {
      _logger = logger;
      _context = context;
      _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
      var user = await _userManager.GetUserAsync(User);
      ViewData["Nickname"] = user.Nickname;
      var model = await _context.ChatMessages
      .Include(x => x.User)
      .OrderByDescending(x => x.DatePosted)
      .Take(50)
      .OrderBy(x => x.DatePosted)
      .Select(x => new ChatMessageViewModel
      {
        DatePosted = x.DatePosted,
        Message = x.Message,
        Nickname = x.User.Nickname
      })
      .ToListAsync();
      return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
