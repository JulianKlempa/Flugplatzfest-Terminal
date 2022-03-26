using Flugplatzfest_Terminal.MVVM.Model.Menu;
using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Flugplatzfest_Terminal.MVVM.Model.Order
{
    public class Order
    {
        private readonly ChatId chatId;
        private List<OrderItem> orderItems;

        public Order(ChatId chatId)
        {
            this.chatId = chatId;
            orderItems = new List<OrderItem>();
        }

        public List<OrderItem> GetOrderItems()
        {
            return orderItems;
        }

        public OrderItem GetOrderItem(MenuItem menuItem)
        {
            return orderItems.Where(x => x.GetMenuItem().Equals(menuItem)).FirstOrDefault();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            orderItems.Add(orderItem);
        }

        public ChatId GetChatId()
        {
            return chatId;
        }
    }
}