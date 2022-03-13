using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Services;
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

        public ICommand NavigateSettingsCommand { get; }

        public TerminalViewModel(ChatList chatList, Interface inter, NavigationService settingsViewNavigationService)
        {
            chats = new ObservableCollection<ChatViewModel>();
            messages = new ObservableCollection<MessageViewModel>();
            SendCommand = new SendMessageCommand(this, inter);
            NavigateSettingsCommand = new NavigateCommand(settingsViewNavigationService);

            UpdateChats();
        }

        private void UpdateChats()
        {
            chats.Clear();

            //foreach ()
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
                chat = selectedChatViewmodel?.chat;
                OnPropertyChanged(nameof(SelectedChatViewModel));
            }
        }

        public Chat GetCurrentChat()
        {
            return chat;
        }

        public string GetSendMessage()
        {
            return sendMessageText;
        }
    }
}