using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class TerminalViewModel : ViewModelBase
    {
        private string sendMessageText;

        private Chat chat;

        private ChatViewModel selectedChatViewmodel;

        private readonly ObservableCollection<ChatViewModel> chats;
        public IEnumerable<ChatViewModel> Chats => chats;

        private readonly ObservableCollection<MessageViewModel> messages;

        public IEnumerable<MessageViewModel> Messages => messages;

        public ICommand SendCommand { get; }

        public TerminalViewModel(ChatList chatList, Interface inter)
        {
            chats = new ObservableCollection<ChatViewModel>();
            messages = new ObservableCollection<MessageViewModel>();
            SendCommand = new SendMessageCommand(inter, ref sendMessageText, ref chat);

            TextMessage message = new TextMessage("Test", new ChatId(InterfaceType.Telegram, 187), MessageDirection.incoming);
            chatList.AddMessage(message);
            chats.Add(new ChatViewModel(message, chatList));
            TextMessage message2 = new TextMessage("Test", new ChatId(InterfaceType.Telegram, 259), MessageDirection.outgoing);
            chatList.AddMessage(message2);
            chats.Add(new ChatViewModel(message2, chatList));
        }

        public string SendMessageText
        {
            get => sendMessageText;
            set
            {
                sendMessageText = value;
                OnPropertyChanged(nameof(SendMessageText));
            }
        }

        public ChatViewModel SelectedChatViewModel
        {
            set
            {
                selectedChatViewmodel = value;
                chat = selectedChatViewmodel.chat;
                OnPropertyChanged(nameof(SelectedChatViewModel));
            }
        }
    }
}