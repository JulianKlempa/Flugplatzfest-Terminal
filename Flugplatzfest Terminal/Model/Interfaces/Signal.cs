namespace Flugplatzfest_Terminal.Model.Interfaces
{
    public class Signal
    {
        private readonly Events events;

        public Signal(Events events)
        {
            this.events = events;
        }
    }
}