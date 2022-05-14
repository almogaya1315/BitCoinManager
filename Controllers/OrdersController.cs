using BitCoinManager.Models;
using BitCoinManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Controllers
{
    public class OrdersController : ControllerBase
    {
        public OrdersController(GlobalizationHandler global, ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(global, logger, repository, session) { }

        public IActionResult MainMenu(UserViewModel userVm)
        {
            return View(userVm);
        }
    }
}
