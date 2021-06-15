# Simple Chat Bot

A simple live chat application with a chat bot

## Built with

* [.NET 5](https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/)
* [Identity Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio)
* [SignalR](https://github.com/SignalR/SignalR)
* [Docker](https://docker.com) 
* [RabbitMQ](https://www.rabbitmq.com/)

## Getting Started

### Requirements

* [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Microsoft SQL Server Express LocalDb](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)
* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/) or [VSCode](https://code.visualstudio.com/download) with C# Support (As [OmniSharp](https://www.omnisharp.net/))
* RabbitMQ

### Optional

* Docker

## Installation

* Clone the repository by using any git client you prefer or downloading the zip package.
* Start your RabbitMQ Server. (If you don't have it, you can follow this [guide](https://blog.devgenius.io/rabbitmq-with-docker-on-windows-in-30-minutes-172e88bb0808) to install it with Docker).
* Update appSettings in Simple.Chat.Bot.App/appSettings.json and Simple.Chat.Bot.CommandWorker/appSettings.json to work with your RabbitMQ Server settings. By default, both points to the default RabbitMQ settings.
* Open the root folder of the project with Visual Studio 2019 or VSCode with .Net 5 SDK installed.
* Run Entity Framework Core migrations to restore de database. (SQL Server Express localdb is required)
* With RabbitMQ running, execute both App and CommandWorker projects from Visual Studio 2019 IDE or following the next commands:
  * Open a terminal in the root folder and execute:

  ```shell
  cd Simple.Chat.Bot.CommandWorker
  dotnet run
  ```

  * Open another terminal in the root folder and execute:

  ```shell
  cd Simple.Chat.Bot.App
  dotnet run
  ```

* After having running both projects, open your browser in http://localhost:5000 or https://localhost:5001
* Test the app

## Notes

* If you have any dependency problem, try running the following command in the root folder:

  ```shell
  dotnet restore
  ```
