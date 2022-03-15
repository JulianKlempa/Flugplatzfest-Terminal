using System.Collections.Generic;

namespace Flugplatzfest_Terminal.MVVM.Model.Messages
{
    public class Chat
    {
        private Queue<TextMessage> textMessages;
        private TextMessage lastMessage;
        private ChatId chatId;

        public Chat()
        {
            textMessages = new Queue<TextMessage>();
        }

        public TextMessage GetLastMessage()
        {
            return lastMessage;
        }

        public Queue<TextMessage> GetAllMessages()
        {
            return textMessages;
        }

        public void AddMessage(TextMessage message)
        {
            chatId = message.GetChatID();
            textMessages.Enqueue(message);
            lastMessage = message;
        }

        public ChatId GetChatId()
        {
            return chatId;
        }
    }
}