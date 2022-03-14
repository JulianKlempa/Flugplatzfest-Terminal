﻿using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class TerminalViewModel : ViewModelBase
    {
        private readonly App app;
        private string sendMessageText;
        private Chat chat;
        private ChatViewModel selectedChatViewmodel;

        public ObservableCollection<ChatViewModel> Chats { get; set; }
        public ObservableCollection<MessageViewModel> Messages { get; set; }

        private readonly List<ChatViewModel> chats;

        public ICommand SendCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public TerminalViewModel(App app, NavigationService settingsViewNavigationService)
        {
            this.app = app;
            chats = new List<ChatViewModel>();
            GenerateChats();

            Chats = new ObservableCollection<ChatViewModel>(chats);
            Messages = new ObservableCollection<MessageViewModel>();

            SendCommand = new SendMessageCommand(this, app.GetInterface());
            NavigateSettingsCommand = new NavigateCommand(settingsViewNavigationService);

            app.GetEvents().MessageReceived += UpdateChats;
            app.GetEvents().MessageSent += UpdateChats;
        }

        private void UpdateChats(TextMessage message)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                Chat chat = app.GetChatList().GetChat(message.GetChatID());
                ChatViewModel chatViewModel = Chats.FirstOrDefault(x => x.GetChat().GetChatId().Equals(message.GetChatID()));
                if (chatViewModel != null)
                {
                    chatViewModel.UpdateChat(chat);
                }
                else
                {
                    Chats.Add(new ChatViewModel(chat));
                }
                CollectionViewSource.GetDefaultView(Chats).Refresh();
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
            Messages.Clear();

            Queue<TextMessage> textMessages = chat?.GetAllMessages();
            if (textMessages != null)
            {
                foreach (TextMessage message in textMessages)
                {
                    Messages.Add(new MessageViewModel(message));
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