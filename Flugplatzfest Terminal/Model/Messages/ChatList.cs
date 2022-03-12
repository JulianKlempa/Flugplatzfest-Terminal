using System.Collections.Generic;

namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatList
    {
        private Dictionary<ChatId, Queue<string>> chatList;

        public ChatList()
        {
            chatList = new Dictionary<ChatId, Queue<string>>();
        }

        public bool AddMessage(TextMessage message)
        {
            //TODO fix evaluation
            bool exists = chatList.TryGetValue(message.GetChatID(), out Queue<string> queue);
            if (!exists)
            {
                queue = new Queue<string>();
            }
            queue.Enqueue(message.GetMessage());
            chatList[message.GetChatID()] = queue;
            return !exists;
        }
    }
}