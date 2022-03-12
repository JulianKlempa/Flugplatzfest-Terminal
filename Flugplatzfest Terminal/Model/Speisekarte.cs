namespace Flugplatzfest_Terminal.Model
{
    internal class Speisekarte
    {
        private string speisekarte;

        public Speisekarte(string speisekarte)
        {
            SetSpeisekarte(speisekarte);
        }

        public string GetSpeisekarte()
        {
            return speisekarte;
        }

        public void SetSpeisekarte(string speisekarte)
        {
            this.speisekarte = speisekarte;
        }
    }
}