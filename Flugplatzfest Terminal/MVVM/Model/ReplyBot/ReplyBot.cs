using Flugplatzfest_Terminal.MVVM.Model.Interfaces;
using Flugplatzfest_Terminal.MVVM.Model.Messages;
using Flugplatzfest_Terminal.MVVM.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flugplatzfest_Terminal.MVVM.Model.ReplyBot
{
    public class ReplyBot
    {
        private readonly ChatList chatList;
        private readonly Interface inter;
        private readonly App app;

        public ReplyBot(ChatList chatList, Interface inter, App app)
        {
            this.chatList = chatList;
            this.inter = inter;
            this.app = app;
        }

        public void Reply(TextMessage message)
        {
            if (app.GetBotActive())
            {
                if (chatList.GetChat(message.GetChatID())?.GetAllMessages().Count <= 1 || message.GetMessage().ToLower().Contains("karte"))
                {
                    inter.SendMessage(message.Reply(app.GetMenu().ToString()));
                    inter.SendMessage(new TextMessage("Bitte Nummer zurückschreiben", message.GetChatID(), MessageDirection.outgoing));
                    //TODO Message config
                }
                else
                {
                    string messageString = message.GetMessage();
                    Order.Order order = app.GetOrdersList().GetOrder(message.GetChatID());
                    if (order == null)
                    {
                        order = new Order.Order(message.GetChatID());
                        app.GetOrdersList().AddOrder(order);
                    }
                    foreach (string orderItemString in messageString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        List<int> numberList = new List<int>();
                        int currentNumber = 0;
                        foreach (char orderItemChar in orderItemString.ToArray())
                        {
                            if (char.IsDigit(orderItemChar))
                            {
                                currentNumber = currentNumber * 10 + orderItemChar - '0';
                            }
                            else if (currentNumber != 0)
                            {
                                numberList.Add(currentNumber);
                                currentNumber = 0;
                            }
                        }
                        int index = 0;
                        while (numberList.Count > index)
                        {
                            int amount = numberList.Count > index + 1 ? numberList[index + 1] : 1;
                            OrderItem orderItem = order.GetOrderItem(app.GetMenu().GetMenuItem(numberList[index]));
                            if (orderItem == null)
                            {
                                orderItem = new OrderItem(app.GetMenu().GetMenuItem(numberList[index]));
                                if (orderItem.GetMenuItem() != null)
                                {
                                    order.AddOrderItem(orderItem);
                                }
                                else
                                {
                                    //TODO notify
                                }
                            }
                            orderItem.SetAmount(orderItem.GetAmount() + amount);
                            index += 2;
                        }
                    }
                    double price = 0.0;
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Ihre aktuelle Bestellung:");
                    foreach (OrderItem item in order.GetOrderItems())
                    {
                        price += item.GetMenuItem().Price * item.GetAmount();
                        stringBuilder.AppendLine(string.Format("{0,3}x{1,-30}{2,5:C}", item.GetAmount(), item.GetMenuItem().Content, item.GetMenuItem().Price * item.GetAmount()));
                    }
                    stringBuilder.AppendLine(String.Format("Gesamtkosten: {0:C}", price));
                    inter.SendMessage(message.Reply(stringBuilder.ToString()));
                }
            }
        }
    }
}