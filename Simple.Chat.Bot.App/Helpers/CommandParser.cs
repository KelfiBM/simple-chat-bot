using System.Text.RegularExpressions;

namespace Simple.Chat.Bot.App.Helpers
{
  public class CommandParser
  {
    private static readonly string _commandRegex = @"^(\/\w+\=)(\w|.+)$";
    public static bool IsCommand(string command)
    {
      return Regex.IsMatch(command, _commandRegex);
    }
  }
}