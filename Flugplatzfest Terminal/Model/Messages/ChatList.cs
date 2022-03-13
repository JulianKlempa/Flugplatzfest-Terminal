using System.Collections.Generic;

namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatList
    {
        private Dictionary<ChatId, Queue<TextMessage>> chatList;

        public ChatList()
        {
            chatList = new Dictionary<ChatId, Queue<TextMessage>>();
        }

        public bool AddMessage(TextMessage message)
        {
            bool exists = chatList.TryGetValue(message.GetChatID(), out Queue<TextMessage> queue);
            if (!exists)
            {
                queue = new Queue<TextMessage>();
            }
            queue.Enqueue(message);
            chatList[message.GetChatID()] = queue;
            return !exists;
        }
    }
}