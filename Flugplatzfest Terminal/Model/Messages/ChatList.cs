﻿using System.Collections.Generic;

namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatList
    {
        private Dictionary<ChatId, Chat> chatList;

        public ChatList()
        {
            chatList = new Dictionary<ChatId, Chat>();
        }

        public bool AddMessage(TextMessage message)
        {
            bool exists = chatList.TryGetValue(message.GetChatID(), out Chat queue);
            if (!exists)
            {
                queue = new Chat();
            }
            queue.AddMessage(message);
            chatList[message.GetChatID()] = queue;
            return !exists;
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