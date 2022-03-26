using Flugplatzfest_Terminal.MVVM.Model.Menu;
using Flugplatzfest_Terminal.MVVM.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flugplatzfest_Terminal.MVVM.Model.Order
{
    public class Order
    {
        private readonly ChatId chatId;
        private readonly App app;
        private readonly List<OrderItem> orderItems;

        public Order(ChatId chatId, App app)
        {
            this.chatId = chatId;
            this.app = app;
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
            app.GetEvents().OnOrderChanged(this);
        }

        public ChatId GetChatId()
        {
            return chatId;
        }

        public override string ToString()
        {
            double price = 0.0;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ihre aktuelle Bestellung:");
            foreach (OrderItem item in GetOrderItems())
            {
                price += item.GetMenuItem().Price * item.GetAmount();
                stringBuilder.AppendLine(string.Format("{0,3}x{1,-30}{2,5:C}", item.GetAmount(), item.GetMenuItem().Content, item.GetMenuItem().Price * item.GetAmount()));
            }
            stringBuilder.AppendLine(string.Format("Gesamtkosten: {0:C}", price));
            return stringBuilder.ToString();
        }
    }
}