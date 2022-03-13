using Flugplatzfest_Terminal.Model.Messages;
using System;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly TextMessage textMessage;
        private readonly ChatList chatList;

        public bool Incoming => chatList.GetTextMessages(textMessage.GetChatID()).GetLastMessage().GetMessageDirection() == MessageDirection.incoming;
        public string LastMessageText => chatList.GetTextMessages(textMessage.GetChatID()).GetLastMessage().GetMessage();
        public DateTime DateTime => chatList.GetTextMessages(textMessage.GetChatID()).GetLastMessage().GetDateTime();

        public ChatViewModel(TextMessage textMessage, ChatList chatList)
        {
            this.textMessage = textMessage;
            this.chatList = chatList;
        }
    }
}