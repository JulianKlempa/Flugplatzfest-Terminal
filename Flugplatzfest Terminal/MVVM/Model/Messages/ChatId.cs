namespace Flugplatzfest_Terminal.MVVM.Model.Messages
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

        public override bool Equals(object obj)
        {
            ChatId other = obj as ChatId;
            return other != null && other.chatID == chatID && other.interfaceType == interfaceType;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + interfaceType.GetHashCode();
            hash = (hash * 7) + chatID.GetHashCode();
            return hash;
        }
    }
}