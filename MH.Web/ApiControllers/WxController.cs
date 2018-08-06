using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MH.Common;

namespace MH.Web.ApiControllers
{
    [Produces("application/json")]
    [Route("Wx")]
    public class WxController : BaseController
    {
        public WxController(IHttpContextAccessor _accessor) : base(_accessor)
        {
        }

        [Route("")]
        public ActionResult Index()
        {
            return Content(accessor.CheckWX());
        }
        [HttpPost]
        [Route("menu/create")]
        public IActionResult CreateMenu()
        {
            var data = new ButtonParam() { button = new List<BaseButton>() };
            data.button.Add(new ViewButton() { name = "url按钮", type = ButtonType.view.ToString(), url = "http://277366155.cn:10821" });
            var subBtns=new SubButtonList() { name="test二级菜单", sub_button= new List<Button>() };
            subBtns.sub_button.Add(new ViewButton() { name="二级链接测试", type = ButtonType.view.ToString(), url = "http://277366155.cn:10821" });
            data.button.Add(subBtns);
           return Content(WxApi.CreateMenu(data));
        }

        [HttpPost]
        [Route("menu/delete")]
        public IActionResult DeleteMenu()
        {
            return Content(WxApi.DeleteMenu());
        }

        [HttpPost]
        [Route("menu/select")]
        public IActionResult SelectMenu()
        {
            return Content(WxApi.SelectMenu());
        }
    }
}