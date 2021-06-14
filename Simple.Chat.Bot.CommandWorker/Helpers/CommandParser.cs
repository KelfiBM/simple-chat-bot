using System.Text.RegularExpressions;

namespace Simple.Chat.Bot.CommandWorker.Helpers
{
  public class CommandParser
  {
    private static readonly string _commandRegex = @"^(\/\w+\=)(\w|.+)$";
    public static string GetCommand(string command)
    {
      if (!IsCommand(command)) return null;
      return Regex.Match(command, _commandRegex).Groups[1].Value;
    }
    public static string GetParameter(string command)
    {
      if (!IsCommand(command)) return null;
      return Regex.Match(command, _commandRegex).Groups[2].Value;
    }

    public static bool IsCommand(string command)
    {
      return Regex.IsMatch(command, _commandRegex);
    }
  }
}