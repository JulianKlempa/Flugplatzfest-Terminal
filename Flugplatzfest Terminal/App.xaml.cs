using Flugplatzfest_Terminal.Model;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Stores;
using Flugplatzfest_Terminal.ViewModels;
using System;
using System.Windows;

namespace Flugplatzfest_Terminal
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private readonly Interface inter;
        private Menu menu;
        private readonly ChatList chatList;
        private readonly NavigationStore navigationStore;

        public App()
        {
            Events events = new Events();
            events.MessageReceived += Events_MessageReceived;
            menu = new Menu("Das ist eine Speisekarte \n1. \n2.");
            string telegramToken = "5271526292:AAH0KJH2ULkRSWMmBZPoGeeLzpwyW0TOn1k";
            chatList = new ChatList();
            inter = new Interface(telegramToken, events, chatList);
            navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = new TerminalViewModel(chatList, inter, navigationStore, this);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private void Events_MessageReceived(TextMessage message)
        {
            if (chatList.AddMessage(message) || message.GetMessage().ToLower().Contains("karte"))
            {
                inter.SendMessage(new TextMessage(menu.GetMenu(), message.GetChatID(), MessageDirection.outgoing));
            }
            else
            {
                inter.SendMessage(message);
            }
            Console.WriteLine(message.GetMessage());
        }

        public void SetMenu(string menuString)
        {
            menu.SetMenu(menuString);
        }

        public string GetMenu()
        {
            return menu.GetMenu();
        }
    }
}