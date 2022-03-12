using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Flugplatzfest_Terminal.Model;
using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.Model.Interfaces
{
    class Telegram
    {
        private Events events;

        TelegramBotClient botClient;
        CancellationToken ct;
        ReceiverOptions receiverOptions;
        ChatList chatList;

        public Telegram(string token, Events events, ChatList chatList)
        {
            this.events = events;

            botClient = new TelegramBotClient(token);

            CancellationTokenSource cts = new CancellationTokenSource();

            ct = cts.Token;

            this.chatList = chatList;

            receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            StartReceiving();
        }

        private async Task StartReceiving()
        {
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: ct);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message.Type != MessageType.Text)
                return;

            long chatId = update.Message.Chat.Id;
            string messageText = update.Message.Text;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            events.OnMessageReceived(new TextMessage(messageText, new Messages.ChatId(InterfaceType.Telegram, chatId, chatList)));
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            return Task.CompletedTask;
        }

        public async void SendMessage(TextMessage message)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: message.GetChatID().GetChatID(),
                text: message.GetMessage(),
                cancellationToken: ct);
        }
    }
}
