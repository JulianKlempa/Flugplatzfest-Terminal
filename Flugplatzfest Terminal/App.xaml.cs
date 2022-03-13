using Flugplatzfest_Terminal.Model;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
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
        private Interface interfaces;
        private Speisekarte speisekarte;
        private ChatList chatList;

        protected override void OnStartup(StartupEventArgs e)
        {
            Events events = new Events();
            events.MessageReceived += Events_MessageReceived;
            speisekarte = new Speisekarte("Das ist eine Speisekarte \n1. \n2.");
            string telegramToken = "5271526292:AAH0KJH2ULkRSWMmBZPoGeeLzpwyW0TOn1k";
            chatList = new ChatList();
            interfaces = new Interface(telegramToken, events, chatList);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(chatList, interfaces)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private void Events_MessageReceived(TextMessage message)
        {
            if (chatList.AddMessage(message))
            {
                interfaces.SendMessage(new TextMessage(speisekarte.GetSpeisekarte(), message.GetChatID(), MessageDirection.outgoing));
            }
            else
            {
                interfaces.SendMessage(message);
            }
            Console.WriteLine(message.GetMessage());
        }
    }
}