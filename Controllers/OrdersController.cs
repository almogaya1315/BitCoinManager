using BitCoinManager.Models;
using BitCoinManager.Services;
using BitCoinManagerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BitCoinManager.Controllers
{
    public class OrdersController : ControllerBase
    {
        public OrdersController(ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(logger, repository, session) { }

        public IActionResult MainMenu(UserViewModel userVm)
        {
            if (userVm.Model.Id == 0 || userVm.Model.Orders.Count == 0)
                _session.GetUserFromCookies(out userVm);

            ViewData.Add("Operations", _session.Get<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>>(SessionKey.operations));

            return View(userVm);
        }


        public IActionResult CreateOrder(OrderViewModel orderVm)
        {
            _session.GetUserFromCookies(out UserViewModel userVm);

            try
            {
                //throw new Exception();

                orderVm.Model.Id = _repository.CreateOrder(userVm.Model.Id, orderVm.Model);
                userVm.Model.Orders.Add(orderVm.Model);
                _session.SetUserInCookies(JsonConvert.SerializeObject(userVm.Model));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in 'CreateOrder'. {e.Message}");
                ViewData.Add("Mesasge_Login", "Error in order creation.");


                return Json(new { success = false, responseText = e.Message });
            }

            return View("MainMenu", userVm);
        }
    }
}
