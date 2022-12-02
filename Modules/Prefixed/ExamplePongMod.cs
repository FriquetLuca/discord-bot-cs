using Discord.Commands;
using Discord;
namespace DiscordBot.Modules.Prefixed {
    public partial class PrefixModule {
        [Command("ping", true)]
        public async Task Pong() {
            // Reply to the user's message with the response
            await Context.Message.ReplyAsync("Pong!");
        }
    }
}