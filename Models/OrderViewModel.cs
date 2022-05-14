using BitCoinManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Models
{
    public class OrderViewModel
    {
        private Order _order;

        public OrderViewModel() 
            : this(new Order()) { }

        public OrderViewModel(Order order)
        {
            _order = order;
        }

        public double Amount 
        {
            get { return _order.Amount; }
            set { _order.Amount = value; }
        }
        public double Price 
        {
            get { return _order.Price; }
            set { _order.Price = value; }
        }
        public OrderOperation Operation
        {
            get { return _order.Operation; }
            set { _order.Operation = value; }
        }

        public Order Model { get { return _order; } }
    }
}
