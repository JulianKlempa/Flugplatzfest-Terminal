namespace Flugplatzfest_Terminal.MVVM.Model
{
    public class MenuItem
    {
        private string content;
        private double price;
        private MenuItemType type;

        public MenuItem(double price, string content)
        {
            this.price = price;
            this.content = content;
        }

        public MenuItem() : this(0, "")
        {
        }

        public string Content
        { get { return content; } set { content = value; } }

        public double Price
        { get { return price; } set { price = value; } }

        public MenuItemType Type
        { get { return type; } set { type = value; } }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public enum MenuItemType
    {
        Food,
        Drink
    }
}