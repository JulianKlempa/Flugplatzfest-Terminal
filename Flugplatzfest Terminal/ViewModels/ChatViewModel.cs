using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private Chat chat;

        public bool Incoming => chat.GetLastMessage().GetMessageDirection() == MessageDirection.incoming;
        public string LastMessageText => chat.GetLastMessage().GetMessage();
        public string DateTime => chat.GetLastMessage().GetDateTime().ToString("HH:mm:ss");

        public ChatViewModel(Chat chat)
        {
            UpdateChat(chat);
        }

        public void UpdateChat(Chat chat)
        {
            this.chat = chat;
        }

        public Chat GetChat()
        { return chat; }
    }
}