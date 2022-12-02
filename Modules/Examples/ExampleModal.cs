using Discord;
using Discord.Interactions;
namespace DiscordBot.Modules.Examples {
    public class HelloModalModule : InteractionModuleBase<SocketInteractionContext> {
        public InteractionService? Commands { get; set; }

        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("modal", "Test modal inputs")]
        public async Task ModalInput() {
            await Context.Interaction.RespondWithModalAsync<HelloModal>("modal_input_demo");
        }

        [ModalInteraction("modal_input_demo")]
        public async Task ModalResponse(HelloModal modal) {
            // Build the message to send.
            string message = $"{modal.Greeting}";

            // Specify the AllowedMentions so we don't actually ping everyone.
            AllowedMentions mentions = new();
            // Filter for the presense of role or everyone pings
            mentions.AllowedTypes = AllowedMentionTypes.Users;
            
            // Respond to the modal.
            await RespondAsync(message, allowedMentions: mentions, ephemeral: true);
        }
    }

    // Defines the modal that will be sent.
    public class HelloModal : IModal {
        // Modal form label
        public string Title => "Hello Modal Inputs";
        // Text box title
        [InputLabel("Send a greeting!")]
        // Strings with the ModalTextInput attribute will automatically become components.
        [ModalTextInput("greeting_input", TextInputStyle.Paragraph, placeholder: "Be nice...", maxLength: 200)]
        // string to hold the user input text
        public string Greeting { get; set; } = "";
    }
}