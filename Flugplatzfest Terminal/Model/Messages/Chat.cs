using System.Collections.Generic;

namespace Flugplatzfest_Terminal.Model.Messages
{
    public class Chat
    {
        private Queue<TextMessage> textMessages;
        private TextMessage lastMessage;

        public Chat()
        {
            textMessages = new Queue<TextMessage>();
        }

        public TextMessage GetLastMessage()
        {
            return lastMessage;
        }

        public void AddMessage(TextMessage message)
        {
            textMessages.Enqueue(message);
            lastMessage = message;
        }
    }
}