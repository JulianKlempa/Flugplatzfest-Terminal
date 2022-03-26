using Flugplatzfest_Terminal.MVVM.Model.Interfaces;
using Flugplatzfest_Terminal.MVVM.Model.Messages;
using Flugplatzfest_Terminal.MVVM.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;

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
                            Order.Order order = app.GetOrdersList().GetOrder(message.GetChatID());
                            if (order == null)
                            {
                                order = new Order.Order(message.GetChatID());
                                app.GetOrdersList().AddOrder(order);
                            }
                            int amount = numberList.Count > index + 1 ? numberList[index] : 0;
                            OrderItem orderItem = new OrderItem(app.GetMenu().GetMenuItem(numberList[index]), amount);
                            if (orderItem.GetMenuItem() != null)
                            {
                                order.AddOrderItem(orderItem);
                                //TODO add to existing
                            }
                            else
                            {
                                //TODO notify
                            }
                            index += 2;
                        }
                    }
                }
            }
        }
    }
}