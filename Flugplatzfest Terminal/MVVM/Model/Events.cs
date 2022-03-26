using Flugplatzfest_Terminal.MVVM.Model.Messages;

namespace Flugplatzfest_Terminal.MVVM.Model
{
    public delegate void NotifyMessageReceived(TextMessage message);

    public delegate void NotifyMessageSent(TextMessage message);

    public delegate void NotifyChatUpdated(Chat chat);

    public delegate void NotifyOrderChanged(Order.Order order);

    public class Events
    {
        public event NotifyMessageReceived MessageReceived;

        public event NotifyMessageSent MessageSent;

        public event NotifyChatUpdated ChatUpdated;

        public event NotifyOrderChanged OrderChanged;

        public virtual void OnMessageReceived(TextMessage message)
        {
            MessageReceived?.Invoke(message);
        }

        public virtual void OnMessageSent(TextMessage message)
        {
            MessageSent?.Invoke(message);
        }

        public virtual void OnChatUpdated(Chat chat)
        {
            ChatUpdated?.Invoke(chat);
        }

        public virtual void OnOrderChanged(Order.Order order)
        {
            OrderChanged?.Invoke(order);
        }
    }
}