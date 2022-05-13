using BitCoinManager.Models;
using BitCoinManager.Services;
using BitCoinManagerModels;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(GlobalizationHandler global, ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(global, logger, repository, session) { }

        public IActionResult Index()
        {
            BitCoinUser user = null;
            if (_session.GetUser(out user))
            {

            }

            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
