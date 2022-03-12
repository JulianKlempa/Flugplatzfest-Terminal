using Flugplatzfest_Terminal.Model;
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
        Model.Interfaces.Telegram telegram;
        Speisekarte speisekarte;
        public MainWindow()
        {
            Events events = new Events();
            events.MessageReceived += Events_MessageReceived;
            speisekarte = new Speisekarte("Das ist eine Speisekarte \n1. \n2.");
            string token = "5271526292:AAH0KJH2ULkRSWMmBZPoGeeLzpwyW0TOn1k";
            ChatList chatList = new ChatList();
            telegram = new Model.Interfaces.Telegram(token, events, chatList);
            InitializeComponent();
        }

        private void Events_MessageReceived(TextMessage message)
        {
            Console.WriteLine(message.GetMessage());
            if (message.GetChatID().GetFirstChat() || message.GetMessage().ToLower().Contains("karte"))
            {
                telegram.SendMessage(new TextMessage(speisekarte.GetSpeisekarte(), message.GetChatID()));
            }
            else
            {
                telegram.SendMessage(new TextMessage("okay", message.GetChatID()));
            }
        }
    }
}
