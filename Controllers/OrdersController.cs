using BitCoinManager.Models;
using BitCoinManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Controllers
{
    public class OrdersController : ControllerBase
    {
        public OrdersController(ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(logger, repository, session) { }

        public IActionResult MainMenu(UserViewModel userVm)
        {
            return View(userVm);
        }


        public IActionResult CreateOrder(OrderViewModel orderVm)
        {
            _session.GetUserFromCookies(out UserViewModel userVm);

            try
            {
                orderVm.Model.Id = _repository.CreateOrder(userVm.Model.Id, orderVm.Model);
                userVm.Orders.Add(orderVm);
                _session.SetUserInCookies(JsonConvert.SerializeObject(orderVm.Model));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in 'CreateOrder'. {e.Message}");
                ViewData.Add("Mesasge_Login", "Error in order creation.");
            }

            return View("MainMenu", userVm);
        }
    }
}
