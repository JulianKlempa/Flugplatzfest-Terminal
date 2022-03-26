using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Flugplatzfest_Terminal.MVVM.Model.Order
{
    public class OrdersList
    {
        private List<Order> orders;

        public OrdersList()
        {
            orders = new List<Order>();
        }

        public Order GetOrder(ChatId chatID)
        {
            return orders.Where(x => x.GetChatId().Equals(chatID)).FirstOrDefault();
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
    }
}