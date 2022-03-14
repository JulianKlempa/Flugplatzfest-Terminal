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
        private readonly Events events;

        public App()
        {
            events = new Events();

            chatList = new ChatList(this);
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

        public void MessageReceived(TextMessage message)
        {
            if (chatList.GetChat(message.GetChatID()).GetAllMessages().Count <= 1 || message.GetMessage().ToLower().Contains("karte"))
            {
                inter.SendMessage(new TextMessage(menu.GetMenu(), message.GetChatID(), MessageDirection.outgoing));
            }
            else
            {
                inter.SendMessage(new TextMessage(message.GetMessage(), message.GetChatID(), MessageDirection.outgoing));
            }
            Console.WriteLine(message.GetMessage());
        }

        public void SaveMenu(string menuString)
        {
            menu.SetMenu(menuString);

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings["Menu"].Value = menuString;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        public string GetMenu()
        {
            return menu.GetMenu();
        }

        public ChatList GetChatList()
        {
            return chatList;
        }

        public Interface GetInterface()
        {
            return inter;
        }

        public Events GetEvents()
        {
            return events;
        }

        private TerminalViewModel CreateTerminalViewModel()
        {
            return new TerminalViewModel(this, new NavigationService(navigationStore, CreateSettingsViewModel));
        }

        private SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel(this, new NavigationService(navigationStore, CreateTerminalViewModel));
        }
    }
}