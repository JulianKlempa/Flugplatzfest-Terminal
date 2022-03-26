namespace Flugplatzfest_Terminal.MVVM.Model.Order
{
    using Flugplatzfest_Terminal.MVVM.Model.Menu;

    public class OrderItem
    {
        private MenuItem menuItem;
        private int amount;

        public OrderItem(MenuItem menuItem, int amount = 1)
        {
            this.menuItem = menuItem;
            this.amount = amount != 0 ? amount : 1;
        }

        public MenuItem GetMenuItem()
        {
            return menuItem;
        }
    }
}