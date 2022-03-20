using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System.Collections.Generic;

namespace Flugplatzfest_Terminal.MVVM.Model.Order
{
    public class Order
    {
        private readonly ChatId chatId;
        private List<OrderItem> orderItems;

        public Order(ChatId chatId)
        {
            this.chatId = chatId;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            orderItems.Add(orderItem);
        }
    }
}