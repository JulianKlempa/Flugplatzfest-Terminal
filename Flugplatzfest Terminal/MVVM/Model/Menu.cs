using System.Collections.Generic;
using System.IO;
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
                menu = serializer.Deserialize(reader) as List<MenuItem>;
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
            foreach (MenuItem menuItem in menu)
            {
                builder.Append("Speisekarte \n");
                builder.Append(menuItem.ToString());
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