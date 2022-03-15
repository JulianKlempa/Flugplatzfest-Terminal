using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System;

namespace Flugplatzfest_Terminal.MVVM.Model.Interfaces
{
    public class Interface
    {
        public InterfaceType interfaceType;
        private readonly Telegram telegram;
        private readonly Events events;

        public Interface(string telegramToken, Events events)
        {
            telegram = new Telegram(telegramToken, events);
            this.events = events;
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