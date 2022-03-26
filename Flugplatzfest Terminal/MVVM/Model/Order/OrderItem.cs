namespace Flugplatzfest_Terminal.MVVM.Model.Order
{
    using Flugplatzfest_Terminal.MVVM.Model.Menu;

    public class OrderItem
    {
        private readonly MenuItem menuItem;
        private int amount;

        public OrderItem(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }

        public void SetAmount(int amount)
        {
            this.amount = amount;
        }

        public int GetAmount()
        {
            return amount;
        }

        public MenuItem GetMenuItem()
        {
            return menuItem;
        }
    }
}