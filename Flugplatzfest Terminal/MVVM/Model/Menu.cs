namespace Flugplatzfest_Terminal.MVVM.Model
{
    internal class Menu
    {
        private string menu;

        public Menu(string speisekarte)
        {
            SetMenu(speisekarte);
        }

        public string GetMenu()
        {
            return menu;
        }

        public void SetMenu(string speisekarte)
        {
            this.menu = speisekarte;
        }
    }
}