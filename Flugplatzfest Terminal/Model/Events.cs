using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.Model
{
    public delegate void NotifyMessageReceived(TextMessage message);

    public class Events
    {
        public event NotifyMessageReceived MessageReceived;

        public virtual void OnMessageReceived(TextMessage message)
        {
            MessageReceived?.Invoke(message);
        }
    }
}