using Flugplatzfest_Terminal.MVVM.Model.Interfaces;
using Flugplatzfest_Terminal.MVVM.Model.Messages;

namespace Flugplatzfest_Terminal.MVVM.Model.ReplyBot
{
    public class ReplyBot
    {
        private readonly ChatList chatList;
        private readonly Interface inter;
        private readonly App app;

        public ReplyBot(ChatList chatList, Interface inter, App app)
        {
            this.chatList = chatList;
            this.inter = inter;
            this.app = app;
        }

        public void Reply(TextMessage message)
        {
            if (chatList.GetChat(message.GetChatID())?.GetAllMessages().Count <= 1 || message.GetMessage().ToLower().Contains("karte"))
            {
                inter.SendMessage(message.Reply(app.GetMenu().ToString()));
            }
            else
            {//TODO implement interaction
                inter.SendMessage(new TextMessage(message.GetMessage(), message.GetChatID(), MessageDirection.outgoing));
            }
        }
    }
}