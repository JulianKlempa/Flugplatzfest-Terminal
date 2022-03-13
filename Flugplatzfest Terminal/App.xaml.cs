using Flugplatzfest_Terminal.Model;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Services;
using Flugplatzfest_Terminal.Stores;
using Flugplatzfest_Terminal.ViewModels;
using System;
using System.Configuration;
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

            chatList = new ChatList();
            navigationStore = new NavigationStore();

            menu = new Menu(ConfigurationManager.AppSettings.Get("Menu"));
            string telegramToken = ConfigurationManager.AppSettings.Get("TelegramToken");

            inter = new Interface(telegramToken, events, chatList);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = CreateTerminalViewModel();

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

        public void SaveMenu(string menuString)
        {
            ConfigurationManager.AppSettings.Set("Menu", menuString);
            menu.SetMenu(menuString);
        }

        public string GetMenu()
        {
            return menu.GetMenu();
        }

        private TerminalViewModel CreateTerminalViewModel()
        {
            return new TerminalViewModel(chatList, inter, new NavigationService(navigationStore, CreateSettingsViewModel));
        }

        private SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel(this, new NavigationService(navigationStore, CreateTerminalViewModel));
        }
    }
}