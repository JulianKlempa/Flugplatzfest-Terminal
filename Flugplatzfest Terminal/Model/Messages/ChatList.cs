using System.Collections.Generic;

namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatList
    {
        private Dictionary<ChatId, Chat> chatList;
        private readonly App app;

        public ChatList(App app)
        {
            chatList = new Dictionary<ChatId, Chat>();
            app.GetEvents().MessageSent += AddMessage;
            app.GetEvents().MessageReceived += AddMessage;
            this.app = app;
        }

        public void AddMessage(TextMessage message)
        {
            bool exists = chatList.TryGetValue(message.GetChatID(), out Chat queue);
            if (!exists)
            {
                queue = new Chat();
            }
            queue.AddMessage(message);
            chatList[message.GetChatID()] = queue;
            if (message.GetMessageDirection() == MessageDirection.incoming) app.MessageReceived(message);
        }

        public Chat GetChat(ChatId chatId)
        {
            chatList.TryGetValue(chatId, out Chat queue);
            return queue;
        }

        public List<Chat> GetAllChats()
        {
            List<Chat> list = new List<Chat>();
            foreach (ChatId key in chatList.Keys)
            {
                list.Add(chatList[key]);
            }
            return list;
        }
    }
}