# Discord.NET bot

> A Discord.NET bot implementation with admin right based on [drobbins329](https://github.com/drobbins329/Discord.Net-V3-Bot-Tutorial)'s discord bot project.

It's just a tweak from [drobbins329](https://github.com/drobbins329/Discord.Net-V3-Bot-Tutorial)'s discord bot project. It will probably be modified and given a bit more thoughts on implementation as time goes on.

## Installation

Install all the NuGet packages:
```terminal
dotnet add package Discord.NET --version 3.8.1
dotnet add package Microsoft.Extensions.Configuration --version 6.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 6.0.0
dotnet add package Microsoft.Extensions.DependencyInjection --version 7.0.0
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions --version 7.0.0
dotnet add package Microsoft.Extensions.Hosting --version 6.0.0
```

## Configuration

To configure your bot, copy `config.json.example` and paste it renamed as `config.json`.
Setup your config from there.
If you're going to build the bot and use it as an `.exe`, then put the `config.json` in the `./bin/Debug/` or `./bin/Release/`.
```terminal
dotnet build
dotnet build --configuration Release
```

## Create modules

You can create modules based on how some example are showned.
Note: Any prefix command modules (not the slash command one) should be inside a `public partial class PrefixModule` to make it work.