using Discord;
using Discord.Commands;
using Discord.Interactions;
namespace DiscordBot.Modules.Examples {
    public class ContextCommandModule : InteractionModuleBase<SocketInteractionContext> {
        public InteractionService? Commands { get; set; }

        [UserCommand("mention")]
        public async Task MentionUser(IUser user) {
            // Respond with user ping
            await RespondAsync($"User to ping: <@{user.Id}>");
        }

        [MessageCommand("authorName")]
        public async Task MessageAuthorName(IMessage message) {
            // Respond with user ping
            await RespondAsync($"Message author: <@{message.Author.Id}>");
        }

        [Command("author")]
        public async Task Author(string message) {
            // Respond with user ping
            await RespondAsync($"Message author: <@{message}>");
        }
    }
}