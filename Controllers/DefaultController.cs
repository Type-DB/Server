using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TypeDB.Server.Controllers
{
    public class DefaultController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("authentication")]
        public IActionResult Authentication()
        {
            return View();
        }

        [Route("triggers")]
        public IActionResult Triggers()
        {
            return View();
        }

        [Route("migration")]
        public IActionResult Migration()
        {
            return View();
        }

        [Route("scripts")]
        public IActionResult Scripts()
        {
            return View();
        }

        [Route("settings")]
        public IActionResult Settings()
        {
            return View();
        }

        [Route("account")]
        public IActionResult Account()
        {
            return View();
        }

        [Route("lock")]
        public IActionResult Lock()
        {
            return View();
        }
    }
}
