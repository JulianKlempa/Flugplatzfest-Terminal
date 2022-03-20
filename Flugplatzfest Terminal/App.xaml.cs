using Flugplatzfest_Terminal.MVVM.Model;
using Flugplatzfest_Terminal.MVVM.Model.Interfaces;
using Flugplatzfest_Terminal.MVVM.Model.Messages;
using Flugplatzfest_Terminal.MVVM.Services;
using Flugplatzfest_Terminal.MVVM.Stores;
using Flugplatzfest_Terminal.MVVM.ViewModels;
using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Flugplatzfest_Terminal
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private readonly Interface inter;
        private readonly Menu menu;
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

            inter = new Interface(telegramToken, events);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

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
            if (chatList.GetChat(message.GetChatID())?.GetAllMessages().Count <= 1 || message.GetMessage().ToLower().Contains("karte"))
            {
                inter.SendMessage(new TextMessage(menu.ToString(), message.GetChatID(), MessageDirection.outgoing));
            }
            else
            {
                inter.SendMessage(new TextMessage(message.GetMessage(), message.GetChatID(), MessageDirection.outgoing));
            }
            Console.WriteLine(message.GetMessage());
        }

        public Menu GetMenu()
        {
            return menu;
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

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings["Menu"].Value = menu.GetXmlString();
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}