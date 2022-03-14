using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class TerminalViewModel : ViewModelBase
    {
        private readonly App app;
        private string sendMessageText;
        private Chat chat;
        private ChatViewModel selectedChatViewmodel;

        private ObservableCollection<ChatViewModel> chats;
        private ObservableCollection<MessageViewModel> messages;

        public IEnumerable<ChatViewModel> Chats => chats;
        public IEnumerable<MessageViewModel> Messages => messages;

        public ICommand SendCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public TerminalViewModel(App app, NavigationService settingsViewNavigationService)
        {
            chats = new ObservableCollection<ChatViewModel>();
            messages = new ObservableCollection<MessageViewModel>();
            SendCommand = new SendMessageCommand(this, app.GetInterface());
            NavigateSettingsCommand = new NavigateCommand(settingsViewNavigationService);

            app.GetEvents().ChatCreated += ChatCreated;
            app.GetEvents().MessageReceived += UpdateChats;
            app.GetEvents().MessageSent += UpdateChats;
            this.app = app;

            GenerateChats();
        }

        private void UpdateChats(TextMessage message)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                Chat chat = app.GetChatList().GetChat(message.GetChatID());
                ChatViewModel chatViewModel = chats.FirstOrDefault(x => x.GetChat().GetChatId().Equals(message.GetChatID()));
                chatViewModel.UpdateChat(chat);
                OnPropertyChanged(nameof(chats));
                UpdateMessages();
            });
        }

        private void ChatCreated(Chat chat)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                chats.Add(new ChatViewModel(chat));
            });
        }

        private void GenerateChats()
        {
            chats.Clear();

            foreach (Chat item in app.GetChatList().GetAllChats())
            {
                ChatViewModel chatViewModel = new ChatViewModel(item);
                chats.Add(chatViewModel);
            }
        }

        private void UpdateMessages()
        {
            messages.Clear();

            Queue<TextMessage> textMessages = chat?.GetAllMessages();
            if (textMessages != null)
            {
                foreach (TextMessage message in textMessages)
                {
                    messages.Add(new MessageViewModel(message));
                }
            }
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
                chat = selectedChatViewmodel?.GetChat();
                UpdateMessages();
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