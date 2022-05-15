using BitCoinManager.Models;
using BitCoinManager.Services;
using BitCoinManagerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(logger, repository, session) { }

        public IActionResult Index()
         {
            var operations = Enum.GetValues(typeof(OrderOperation)).Cast<OrderOperation>().Select(e => new SelectListItem(e.ToString(), ((int)e).ToString()));
            _session.Set(SessionKey.operations, operations);

            UserViewModel userVm = null;
            if (_session.GetUserFromCookies(out userVm))
            {
                return RedirectToAction("MainMenu", "Orders", userVm);
            }

            return RedirectToAction("Login", "UserIdentity", userVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
