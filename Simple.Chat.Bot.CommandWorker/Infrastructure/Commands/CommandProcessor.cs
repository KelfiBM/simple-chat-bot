using System;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Simple.Chat.Bot.CommandWorker.Helpers;

namespace Simple.Chat.Bot.CommandWorker.Infrastructure.Commands
{
  public interface ICommandProcessor
  {
    string GetCommandResponse(string command);
  }
  public class CommandProcessor : ICommandProcessor
  {
    private readonly IConfiguration _configuration;
    public CommandProcessor(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public string GetCommandResponse(string command)
    {
      var parameter = CommandParser.GetParameter(command);
      var commandHead = CommandParser.GetCommand(command);
      var url = $"{_configuration.GetSection("Commands:Stock").GetValue<string>("Api")}?s={parameter.ToLower()}&f=sd2t2ohlcv&h&e=csv";
      try
      {
        var stockResponses = CSVParser.ParseFromUrl<StockResponse>(url);
        var stock = stockResponses.FirstOrDefault(x => x.Symbol.ToLower() == parameter.ToLower());
        if (stock == null || !double.TryParse(stock.Close, out _))
        {
          return $"The command \"{command}\" is invalid";
        }
        return $"{stock.Symbol} quote is ${stock.Close} per share";
      }
      catch (Exception)
      {
        return $"The command \"{command}\" is invalid";
      }
    }

  }
}