using Flugplatzfest_Terminal.Model.Messages;
using System;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private Chat chat;

        public bool Incoming => chat.GetLastMessage().GetMessageDirection() == MessageDirection.incoming;
        public string LastMessageText => chat.GetLastMessage().GetMessage().Contains(Environment.NewLine) ? chat.GetLastMessage().GetMessage().Substring(0, chat.GetLastMessage().GetMessage().IndexOf(Environment.NewLine)) + " ..." : chat.GetLastMessage().GetMessage();
        public InterfaceType InterfaceType =>chat.GetChatId().GetInterfaceType();
        public string DateTime => chat.GetLastMessage().GetDateTime().ToString("HH:mm");

        public ChatViewModel(Chat chat)
        {
            UpdateChat(chat);
            OnPropertyChanged(nameof(ChatViewModel));
        }

        public void UpdateChat(Chat chat)
        {
            this.chat = chat;
        }

        public Chat GetChat()
        { return chat; }
    }
}