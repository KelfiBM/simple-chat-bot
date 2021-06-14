namespace Simple.Chat.Bot.CommandWorker.Helpers
{
  public class ConnectionFactorySettings
  {
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public string MessageQueue { get; set; }
    public string CommandQueue { get; set; }
  }
}