namespace Flugplatzfest_Terminal.MVVM.Model.Messages
{
    public class ChatId
    {
        private readonly InterfaceType interfaceType;
        private readonly long chatID;

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
            return obj is ChatId other && other.chatID == chatID && other.interfaceType == interfaceType;
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