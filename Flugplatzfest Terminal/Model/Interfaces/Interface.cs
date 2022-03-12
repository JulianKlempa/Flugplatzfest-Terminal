using Flugplatzfest_Terminal.Model.Messages;
using System;

namespace Flugplatzfest_Terminal.Model.Interfaces
{
    public class Interface
    {
        public InterfaceType interfaceType;
        private Telegram telegram;
        private ChatList chatList;

        public Interface(string telegramToken, Events events, ChatList chatList)
        {
            telegram = new Telegram(telegramToken, events);
            this.chatList = chatList;
        }

        public void SendMessage(TextMessage message)
        {
            if (message.GetMessageDirection() == MessageDirection.incoming) message.SwitchMessageDirection();
            switch (message.GetChatID().GetInterfaceType())
            {
                case InterfaceType.Telegram:
                    telegram.SendMessage(message);
                    break;

                case InterfaceType.Signal:
                    throw new NotImplementedException();
                    break;

                case InterfaceType.WhatsApp:
                    throw new NotImplementedException();
                    break;

                default:
                    throw new NotImplementedException();
            }
            chatList.AddMessage(message);
        }
    }
}