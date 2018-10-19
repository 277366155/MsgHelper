using MH.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MH.Core;
namespace MH.Web
{
    public class BaseController: Controller
    {
        protected static IHttpContextAccessor CurrentAccessor;
        protected string UserOpenid = "";
        public BaseController()
        {
            CurrentAccessor = Current.CurrentAccessor;
            UserOpenid = CurrentAccessor.GetCookie();
        }
    }
}
