using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flugplatzfest_Terminal.Model
{
    class Speisekarte
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
