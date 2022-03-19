using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Flugplatzfest_Terminal.MVVM.Model
{
    public class Menu
    {
        private List<MenuItem> menu;

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
        {
            StringBuilder builder = new StringBuilder();
            int index = 1;
            builder.AppendLine("Speisekarte");
            builder.AppendLine("");
            foreach (MenuItem menuItem in menu)
            {
                if (menuItem.Type == MenuItemType.Food)
                {
                    builder.AppendLine(string.Format("{0,0}. {1,-20}{2,50}€", index, menuItem.Content, menuItem.Price));
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
                        builder.AppendLine(string.Format("{0,2}. {1,-10}{2,5}€", index, menuItem.Content, menuItem.Price));
                        index++;
                    }
                }
            }

            return builder.ToString();
        }

        public string GetXmlString()
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<MenuItem>));
                serializer.Serialize(writer, menu);
                return writer.ToString();
            }
        }
    }
}