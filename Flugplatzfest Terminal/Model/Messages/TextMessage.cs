namespace Flugplatzfest_Terminal.Model.Messages
{
    public class TextMessage
    {
        private string message;
        private ChatId chatID;
        private MessageDirection messageDirection;

        public TextMessage(string message, ChatId chatID, MessageDirection messageDirection)
        {
            this.message = message;
            this.chatID = chatID;
            this.messageDirection = messageDirection;
        }

        public string GetMessage()
        {
            return message;
        }

        public ChatId GetChatID()
        {
            return chatID;
        }

        public MessageDirection GetMessageDirection()
        {
            return messageDirection;
        }

        public void SwitchMessageDirection()
        {
            switch (messageDirection)
            {
                case MessageDirection.incoming:
                    messageDirection = MessageDirection.outgoing;
                    break;

                case MessageDirection.outgoing:
                    messageDirection = MessageDirection.incoming;
                    break;
            }
        }
    }
}