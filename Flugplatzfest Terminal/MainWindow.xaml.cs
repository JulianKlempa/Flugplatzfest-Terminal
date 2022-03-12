using Flugplatzfest_Terminal.Model;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using System;
using System.Windows;

namespace Flugplatzfest_Terminal
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Interface interfaces;
        private Speisekarte speisekarte;
        private ChatList chatList;

        public MainWindow()
        {
            Events events = new Events();
            events.MessageReceived += Events_MessageReceived;
            speisekarte = new Speisekarte("Das ist eine Speisekarte \n1. \n2.");
            string telegramToken = "5271526292:AAH0KJH2ULkRSWMmBZPoGeeLzpwyW0TOn1k";
            chatList = new ChatList();
            interfaces = new Interface(telegramToken, events, chatList);
            InitializeComponent();
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