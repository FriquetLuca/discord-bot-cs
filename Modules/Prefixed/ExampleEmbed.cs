using Discord;
using Discord.Commands;
namespace DiscordBot.Modules.Prefixed {
    public partial class PrefixModule {
        [Command("embed")]
        public async Task SendRichEmbedAsync() {
            var embed = new EmbedBuilder {
                // Embed property can be set within object initializer
                Title = "Hello world!",
                Description = "I am a description set by initializer."
            };
            // Or with methods
            embed.AddField("Field title", "Field value. I also support [hyperlink markdown](https://example.com)!")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer => footer.Text = "I am a footer.")
                .WithColor(Color.Blue)
                .WithTitle("I overwrote \"Hello world!\"")
                .WithDescription("I am a description.")
                .WithUrl("https://example.com")
                .WithCurrentTimestamp();

            //Your embed needs to be built before it is able to be sent
            await ReplyAsync(embed: embed.Build());
        }
    }
}