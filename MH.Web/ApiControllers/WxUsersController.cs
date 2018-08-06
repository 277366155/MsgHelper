using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MH.Web.ApiControllers
{
    [Produces("application/json")]
    [Route("WxUsers")]
    public class WxUsersController : BaseController
    {
        public WxUsersController(IHttpContextAccessor _accessor) : base(_accessor)
        {
        }

        //[Route("")]
        //public IActionResult GetUserInfo()
        //{
        //    wxApi.
        //}
    }
}