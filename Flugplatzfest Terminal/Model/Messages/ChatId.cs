namespace Flugplatzfest_Terminal.Model.Messages
{
    public class ChatId
    {
        private InterfaceType interfaceType;
        private long chatID;
        private bool firstChat;

        public ChatId(InterfaceType interfaceType, long chatID, ChatList chatList)
        {
            this.interfaceType = interfaceType;
            this.chatID = chatID;
            firstChat = chatList.AddChatId(this);
        }

        public InterfaceType GetInterfaceType()
        {
            return interfaceType;
        }

        public long GetChatID()
        {
            return chatID;
        }

        public bool GetFirstChat()
        {
            return firstChat;
        }
    }
}