using Discord.Interactions;
namespace DiscordBot.Modules.Examples {
    // interation modules must be public and inherit from an IInterationModuleBase
    public class ExampleSlashCommands : InteractionModuleBase<SocketInteractionContext> {
        public InteractionService? Commands { get; set; }
        [SlashCommand("agenda", "find your answer!")]
        public async Task Agenda(DateTime time, string evenment) {
            await RespondAsync($"It's noted ! Your event {evenment} is on {time}."); // reply with the answer
        }
    }
}