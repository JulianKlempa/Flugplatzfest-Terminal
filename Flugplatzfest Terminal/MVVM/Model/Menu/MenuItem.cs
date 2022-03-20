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

        public string Content
        { get; set; }

        public double Price
        { get; set; }

        public MenuItemType Type
        { get; set; }
    }

    public enum MenuItemType
    {
        Food,
        Drink
    }
}