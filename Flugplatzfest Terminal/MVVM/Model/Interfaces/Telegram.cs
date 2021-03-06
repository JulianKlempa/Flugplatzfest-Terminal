using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flugplatzfest_Terminal.MVVM.Model.Interfaces
{
    internal class Telegram
    {
        private readonly TelegramBotClient botClient;
        private CancellationToken ct;
        private readonly ReceiverOptions receiverOptions;
        private readonly Events events;

        public Telegram(string telegramToken, Events events)
        {
            botClient = new TelegramBotClient(telegramToken);

            CancellationTokenSource cts = new CancellationTokenSource();

            ct = cts.Token;

            receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            this.events = events;

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

            events.OnMessageReceived(new TextMessage(messageText, new Messages.ChatId(InterfaceType.Telegram, chatId), MessageDirection.incoming));
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            return Task.CompletedTask;
        }

        public async void SendMessage(TextMessage message)
        {
            await botClient.SendTextMessageAsync(
                chatId: message.GetChatID().GetChatID(),
                text: message.GetMessage(),
                cancellationToken: ct);
        }
    }
}