using BitCoinManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Models
{
    public class UserViewModel
    {
        private User _user;
        private bool _init;

        public UserViewModel()
            : this(new User()) { }

        public UserViewModel(User user)
        {
            _user = user;
        }

        public string Email
        {
            get { return _user.Email; }
            set { _user.Email = value; }
        }

        public string Password
        {
            get { return _user.Password; }
            set { _user.Password = value; }
        }

        public List<OrderViewModel> Orders
        {
            get { return _user.Orders.Select(o => new OrderViewModel(o)).ToList(); }
            set { _user.Orders = value.Select(o => o.Model).ToList(); }
        }

        public User Model { get { return _user; } }
        public bool Init
        {
            get
            {
                _init = string.IsNullOrWhiteSpace(_user.Email) && string.IsNullOrWhiteSpace(_user.Password);
                return _init;
            }
        }
    }
}
