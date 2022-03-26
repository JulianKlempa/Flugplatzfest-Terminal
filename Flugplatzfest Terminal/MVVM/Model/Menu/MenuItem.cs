using System.Xml.Serialization;

namespace Flugplatzfest_Terminal.MVVM.Model.Menu
{
    [XmlRoot("Menu")]
    public class MenuItem
    {
        public MenuItem(double price, string content, MenuItemType type)
        {
            Price = price;
            Content = content;
            Type = type;
        }

        public MenuItem() : this(0, "", MenuItemType.Food)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is MenuItem other && other.Content == this.Content && other.Type == this.Type && other.Price == this.Price;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Price.GetHashCode();
            hash = (hash * 7) + Content.GetHashCode();
            return hash;
        }

        public string Content
        { get; set; }

        public double Price
        { get; set; }

        public MenuItemType Type
        { get; set; }

        public int Index
        { get; set; }
    }

    public enum MenuItemType
    {
        Food,
        Drink
    }
}