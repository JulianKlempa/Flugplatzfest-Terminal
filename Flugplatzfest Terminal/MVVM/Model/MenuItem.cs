namespace Flugplatzfest_Terminal.MVVM.Model
{
    public class MenuItem
    {
        private string content;
        private double price;
        private MenuItemType type;

        public MenuItem(double price, string content, MenuItemType type)
        {
            this.price = price;
            this.content = content;
            this.type = type;
        }

        public MenuItem() : this(0, "", MenuItemType.Food)
        {
        }

        public string Content
        { get { return content; } }

        public double Price
        { get { return price; } }

        public MenuItemType Type
        { get { return type; } }
    }

    public enum MenuItemType
    {
        Food,
        Drink
    }
}