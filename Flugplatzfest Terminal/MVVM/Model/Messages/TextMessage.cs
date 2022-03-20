using System;

namespace Flugplatzfest_Terminal.MVVM.Model.Messages
{
    public class TextMessage
    {
        private string message;
        private readonly ChatId chatID;
        private MessageDirection messageDirection;
        private DateTime dateTime;

        public TextMessage(string message, ChatId chatID, MessageDirection messageDirection)
        {
            this.message = message;
            this.chatID = chatID;
            this.messageDirection = messageDirection;
            dateTime = DateTime.Now;
        }

        public TextMessage Reply(string text)
        {
            message = text;
            messageDirection = MessageDirection.outgoing;
            dateTime = DateTime.Now;
            return this;
        }

        public string GetMessage()
        {
            return message;
        }

        public ChatId GetChatID()
        {
            return chatID;
        }

        public MessageDirection GetMessageDirection()
        {
            return messageDirection;
        }

        public DateTime GetDateTime()
        {
            return dateTime;
        }
    }
}