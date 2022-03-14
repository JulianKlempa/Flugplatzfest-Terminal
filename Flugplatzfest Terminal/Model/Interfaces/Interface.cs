using Flugplatzfest_Terminal.Model.Messages;
using System;

namespace Flugplatzfest_Terminal.Model.Interfaces
{
    public class Interface
    {
        public InterfaceType interfaceType;
        private Telegram telegram;
        private readonly Events events;
        private ChatList chatList;

        public Interface(string telegramToken, Events events, ChatList chatList)
        {
            telegram = new Telegram(telegramToken, events);
            this.events = events;
            this.chatList = chatList;
        }

        public void SendMessage(TextMessage message)
        {
            switch (message.GetChatID().GetInterfaceType())
            {
                case InterfaceType.Telegram:
                    telegram.SendMessage(message);
                    break;

                case InterfaceType.Signal:
                    throw new NotImplementedException();

                case InterfaceType.WhatsApp:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }
            events.OnMessageSent(message);
        }
    }
}