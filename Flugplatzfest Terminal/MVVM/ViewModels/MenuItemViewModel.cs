using Flugplatzfest_Terminal.MVVM.Model;

namespace Flugplatzfest_Terminal.MVVM.ViewModels
{
    public class MenuItemViewModel
    {
        private readonly MenuItem menuItem;

        public string ItemName => menuItem.Content;

        public double Price => menuItem.Price;

        public MenuItemViewModel(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }
    }
}