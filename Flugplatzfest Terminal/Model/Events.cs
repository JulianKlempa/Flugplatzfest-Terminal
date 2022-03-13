using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.Model
{
    public delegate void NotifyMessageReceived(TextMessage message);

    public delegate void NotifyMessageSent(TextMessage message);

    public class Events
    {
        public event NotifyMessageReceived MessageReceived;

        public event NotifyMessageSent MessageSent;

        public virtual void OnMessageReceived(TextMessage message)
        {
            MessageReceived?.Invoke(message);
        }

        public virtual void OnMessageSent(TextMessage message)
        {
            MessageSent?.Invoke(message);
        }
    }
}