using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.Commands
{
    public class SendMessageCommand : CommandBase
    {
        private Interface inter;

        private string message;

        private Chat chat;

        public SendMessageCommand(Interface inter, ref string message, ref Chat chat)
        {
            this.inter = inter;
            this.message = message;
            this.chat = chat;
        }

        public override void Execute(object parameter)
        {
            inter.SendMessage(new TextMessage(message, chat.GetLastMessage().GetChatID(), MessageDirection.outgoing));
        }
    }
}