using Flugplatzfest_Terminal.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flugplatzfest_Terminal.Model
{
    public delegate void NotifyMessageReceived(TextMessage message);
    public class Events
    {
        public event NotifyMessageReceived MessageReceived;

        public virtual void OnMessageReceived(TextMessage message) //protected virtual method
        {
            //if ProcessCompleted is not null then call delegate
            MessageReceived?.Invoke(message);
        }
    }
}
