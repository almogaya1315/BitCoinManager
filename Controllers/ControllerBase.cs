using BitCoinManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly ILogger<Controller> _logger;
        protected readonly BitCoinRepository _repository;
        protected readonly ISessionHandler _session;

        public ControllerBase(ILogger<Controller> logger, BitCoinRepository repository, ISessionHandler session)
        {
            _logger = logger;
            _repository = repository;
            _session = session;
        }
    }
}
