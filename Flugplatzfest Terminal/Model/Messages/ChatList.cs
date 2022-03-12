﻿using System.Collections.Generic;
using System.Linq;

namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatList
    {
        private List<ChatId> chatIds;

        public ChatList()
        {
            chatIds = new List<ChatId>();
        }

        public bool AddChatId(ChatId chatId)
        {
            ChatId chatID = chatIds.FirstOrDefault(x => (x.GetInterfaceType() == chatId.GetInterfaceType() && x.GetChatID() == chatId.GetChatID()));
            if (chatID == null)
            {
                chatIds.Add(chatId);
            }
            return chatID == null;
        }
    }
}