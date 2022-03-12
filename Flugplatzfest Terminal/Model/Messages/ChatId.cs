namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatId
    {
        private InterfaceType interfaceType;
        private long chatID;

        public ChatId(InterfaceType interfaceType, long chatID)
        {
            this.interfaceType = interfaceType;
            this.chatID = chatID;
        }

        public InterfaceType GetInterfaceType()
        {
            return interfaceType;
        }

        public long GetChatID()
        {
            return chatID;
        }
    }
}