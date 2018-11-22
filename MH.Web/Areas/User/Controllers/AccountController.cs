using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MH.Web.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}