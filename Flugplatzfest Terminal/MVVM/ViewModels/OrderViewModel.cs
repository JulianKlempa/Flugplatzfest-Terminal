using Flugplatzfest_Terminal.MVVM.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flugplatzfest_Terminal.MVVM.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private Order order;
        public string OrderContent => order.ToString();

        public OrderViewModel(Order order)
        {
            this.order = order;
        }

        public Order GetOrder()
        {
            return order;
        }

        public void UpdateOrder(Order order)
        {
            this.order = order;
        }
    }
}