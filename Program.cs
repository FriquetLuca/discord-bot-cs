using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DiscordBot.Log;
using Discord.Commands;
using DiscordBot.Services;
namespace DiscordBot
{
    public class program
    {
        private DiscordSocketClient? _client;
        public static Task Main(string[] args) => new program().MainAsync();
        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(path: "config.json")
                .Build();
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) => services
                    // Add the configuration to the registered services
                    .AddSingleton(config)
                    // Add the DiscordSocketClient, along with specifying the GatewayIntents and user caching
                    .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                    {
                        GatewayIntents = Discord.GatewayIntents.All,
                        LogGatewayIntentWarnings = false,
                        AlwaysDownloadUsers = true,
                        LogLevel = LogSeverity.Debug
                    }))
                    // Adding console logging
                    .AddTransient<ConsoleLogger>()
                    // Used for slash commands and their registration with Discord
                    .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                    // Required to subscribe to the various client events used in conjunction with Interactions
                    .AddSingleton<InteractionHandler>()
                    // Adding the prefix Command Service
                    .AddSingleton(x => new CommandService(new CommandServiceConfig
                    {
                        LogLevel = LogSeverity.Debug,
                        DefaultRunMode = Discord.Commands.RunMode.Async,

                    }))
                    // Adding the prefix command handler
                    .AddSingleton<PrefixHandler>())
                .Build();

            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var prefixCommands = provider.GetRequiredService<PrefixHandler>();
            prefixCommands.AddModule<DiscordBot.Modules.Prefixed.PrefixModule>();
            await prefixCommands.InitializeAsync();

            // Subscribe to client log events
            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            // Subscribe to slash command log events
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            _client.Ready += async () => {
                // If running the bot with DEBUG flag, register all commands to guild specified in config
                if (IsDebug()) {
                    // Id of the test guild can be provided from the Configuration object
                    Console.WriteLine("Debug mode !");
                    await commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["testGuild"]), true);
                } else {
                    Console.WriteLine("Production mode !");
                    // If not debug, register commands globally
                    await commands.RegisterCommandsGloballyAsync(true);
                }
            };

            await _client.LoginAsync(Discord.TokenType.Bot, config["token"]);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}