namespace Flugplatzfest_Terminal.Model.Messages
{
    public class TextMessage
    {
        private string message;
        private ChatId chatID;

        public TextMessage(string message, ChatId chatID)
        {
            this.message = message;
            this.chatID = chatID;
        }

        public string GetMessage()
        {
            return message;
        }

        public ChatId GetChatID()
        {
            return chatID;
        }
    }
}