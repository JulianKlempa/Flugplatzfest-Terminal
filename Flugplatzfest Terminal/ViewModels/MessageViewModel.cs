﻿using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class MessageViewModel
    {
        private readonly TextMessage textMessage;

        public string Message => textMessage.GetMessage();
        public bool Incoming => textMessage.GetMessageDirection() == MessageDirection.incoming;
        public string Time => textMessage.GetDateTime().ToString("HH:mm");

        public MessageViewModel(TextMessage textMessage)
        {
            this.textMessage = textMessage;
        }
    }
}