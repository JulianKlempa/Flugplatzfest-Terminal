using System.Collections.Generic;

namespace Flugplatzfest_Terminal.MVVM.Model.Messages
{
    public class Chat
    {
        private readonly Queue<TextMessage> textMessages;
        private TextMessage lastMessage;
        private readonly ChatId chatId;

        public Chat(TextMessage textMessage)
        {
            chatId = textMessage.GetChatID();
            AddMessage(textMessage);
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
            textMessages.Enqueue(message);
            lastMessage = message;
        }

        public ChatId GetChatId()
        {
            return chatId;
        }
    }
}