using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Flugplatzfest_Terminal.MVVM.Model.Menu
{
    public class Menu
    {
        [XmlArray("MenuItemList"), XmlArrayItem(typeof(MenuItem), ElementName = "MenuItem")]
        private readonly List<MenuItem> menu;

        public Menu(string xmlString)
        {
            using (StringReader reader = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<MenuItem>));
                try
                {
                    menu = serializer.Deserialize(reader) as List<MenuItem>;
                }
                catch (Exception)
                {
                    menu = new List<MenuItem>();
                }
            }
        }

        public List<MenuItem> GetMenu()
        {
            return menu;
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            menu.Add(menuItem);
        }

        public void ClearMenu()
        {
            menu.Clear();
        }

        public override string ToString()
        { //TODO fix formatting
            StringBuilder builder = new StringBuilder();
            int index = 1;
            builder.AppendLine("Speisekarte");
            builder.AppendLine("");
            foreach (MenuItem menuItem in menu)
            {
                if (menuItem.Type == MenuItemType.Food)
                {
                    builder.AppendLine(string.Format("{0,3}. {1,-30}{2,5:C}", index, menuItem.Content, menuItem.Price));
                    index++;
                }
            }
            if (menu.FirstOrDefault(x => x.Type == MenuItemType.Drink) != null)
            {
                builder.AppendLine("");
                builder.AppendLine("Getränkekarte");
                builder.AppendLine("");
                foreach (MenuItem menuItem in menu)
                {
                    if (menuItem.Type == MenuItemType.Drink)
                    {
                        builder.AppendLine(string.Format("{0,3}. {1,-30}{2,5:C}", index, menuItem.Content, menuItem.Price));
                        index++;
                    }
                }
            }

            return builder.ToString();
        }

        public string GetXmlString()
        {
            using (TextWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MenuItem[]));
                serializer.Serialize(writer, menu.ToArray());
                return writer.ToString();
            }
        }
    }
}