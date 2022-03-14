using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model;
using Flugplatzfest_Terminal.Model.Interfaces;
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
        private string sendMessageText;

        private Chat chat;

        private ChatViewModel selectedChatViewmodel;

        private readonly ObservableCollection<ChatViewModel> chats;
        public IEnumerable<ChatViewModel> Chats => chats;

        private readonly ObservableCollection<MessageViewModel> messages;
        private readonly App app;

        public IEnumerable<MessageViewModel> Messages => messages;

        public ICommand SendCommand { get; }

        public ICommand NavigateSettingsCommand { get; }

        public TerminalViewModel(Events events, App app, Interface inter, NavigationService settingsViewNavigationService)
        {
            chats = new ObservableCollection<ChatViewModel>();
            messages = new ObservableCollection<MessageViewModel>();
            SendCommand = new SendMessageCommand(this, inter);
            NavigateSettingsCommand = new NavigateCommand(settingsViewNavigationService);

            events.MessageSent += UpdateMessages;
            events.MessageReceived += UpdateMessages;
            this.app = app;

            GenerateChats();
        }

        private void UpdateMessages(TextMessage message)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                ChatViewModel chatViewModel = chats.FirstOrDefault(x =>
                {
                    return x.GetChat().GetChatId().GetChatID() == message.GetChatID().GetChatID() && x.GetChat().GetChatId().GetInterfaceType() == message.GetChatID().GetInterfaceType();
                });
                if (chatViewModel != null)
                {
                    chatViewModel.UpdateChat(app.GetChatList().GetChat(message.GetChatID()));
                }
                else
                {
                    chats.Add(new ChatViewModel(app.GetChatList().GetChat(message.GetChatID())));
                }
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