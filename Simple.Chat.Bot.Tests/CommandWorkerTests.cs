using Xunit;
using Simple.Chat.Bot.CommandWorker.Helpers;

namespace Simple.Chat.Bot.Tests
{
  public class CommandWorkerTests
  {
    [Fact]
    public void CommandParser_IsCommand_ValidCommand_ReturnsTrue()
    {
      //Given
      string validCommand = "/stock=a";
      //When
      bool isCommand = CommandParser.IsCommand(validCommand);
      //Then
      Assert.True(isCommand);
    }
    [Fact]
    public void CommandParser_IsCommand_InvalidCommand_ReturnsFalse()
    {
      //Given
      string invalidCommand = "/stock=";
      //When
      bool isCommand = CommandParser.IsCommand(invalidCommand);
      //Then
      Assert.False(isCommand);
    }
    [Fact]
    public void CommandParser_GetCommand_ValidCommand_ReturnsCommandSubstring()
    {
      //Given
      string validCommand = "/stock=a";
      //When
      string commandSubstring = CommandParser.GetCommand(validCommand);
      //Then
      Assert.Equal("/stock=", commandSubstring);
    }
    [Fact]
    public void CommandParser_GetCommand_InvalidCommand_ReturnsNull()
    {
      //Given
      string validCommand = "/stock=";
      //When
      string commandSubstring = CommandParser.GetCommand(validCommand);
      //Then
      Assert.Null(commandSubstring);
    }

    [Fact]
    public void CommandParser_GetParameter_ValidCommand_ReturnsParameterSubstring()
    {
      //Given
      string validCommand = "/stock=a";
      //When
      string commandSubstring = CommandParser.GetParameter(validCommand);
      //Then
      Assert.Equal("a", commandSubstring);
    }
    [Fact]
    public void CommandParser_GetParameter_InvalidCommand_ReturnsNull()
    {
      //Given
      string validCommand = "/stock=";
      //When
      string commandSubstring = CommandParser.GetParameter(validCommand);
      //Then
      Assert.Null(commandSubstring);
    }
  }
}