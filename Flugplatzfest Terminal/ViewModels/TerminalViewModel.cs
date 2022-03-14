using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Services;
using System;
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

        public IEnumerable<MessageViewModel> Messages => messages;
        public IEnumerable<ChatViewModel> Chats => chats;

        public ICommand SendCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public TerminalViewModel(App app, NavigationService settingsViewNavigationService)
        {
            chats = new ObservableCollection<ChatViewModel>();
            messages = new ObservableCollection<MessageViewModel>();
            SendCommand = new SendMessageCommand(this, app.GetInterface());
            NavigateSettingsCommand = new NavigateCommand(settingsViewNavigationService);

            app.GetEvents().MessageSent += UpdateChats;
            app.GetEvents().MessageReceived += UpdateChats;
            this.app = app;

            GenerateChats();
        }

        private void UpdateChats(TextMessage message)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                ChatViewModel chatViewModel = chats.FirstOrDefault(x =>
                {
                    return x.GetChat().GetChatId().GetChatID() == message.GetChatID().GetChatID() && x.GetChat().GetChatId().GetInterfaceType() == message.GetChatID().GetInterfaceType();
                });
                bool isSelected = selectedChatViewmodel == chatViewModel;
                if (chatViewModel != null)
                {
                    chatViewModel.UpdateChat(app.GetChatList().GetChat(message.GetChatID()));
                }
                else
                {
                    chats.Add(new ChatViewModel(app.GetChatList().GetChat(message.GetChatID())));
                }
                UpdateMessages();
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