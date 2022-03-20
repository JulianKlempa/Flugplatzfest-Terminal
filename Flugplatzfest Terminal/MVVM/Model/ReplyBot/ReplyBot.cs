using Flugplatzfest_Terminal.MVVM.Model.Interfaces;
using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System;
using System.Linq;

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
            if (app.GetBotActive())
            {
                if (chatList.GetChat(message.GetChatID())?.GetAllMessages().Count <= 1 || message.GetMessage().ToLower().Contains("karte"))
                {
                    inter.SendMessage(message.Reply(app.GetMenu().ToString()));
                    inter.SendMessage(new TextMessage("Bitte Nummer zurückschreiben", message.GetChatID(), MessageDirection.outgoing));
                }
                else
                {
                    string messageString = message.GetMessage();
                    foreach (string orderItemString in messageString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        string resultString = new string(orderItemString.Where(char.IsDigit).ToArray());
                    }
                }
            }
        }
    }
}