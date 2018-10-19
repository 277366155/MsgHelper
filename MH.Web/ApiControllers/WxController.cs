using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MH.Common;
using MH.Context;

namespace MH.Web.ApiControllers
{
    [Produces("application/json")]
    [Route("Wx")]
    public class WxController : BaseController
    {
        public WxController() 
        {
        }

        [Route("")]
        public ActionResult Index()
        {
            //var msg=accessor.GetUserMsg();
            new WxUserMessageContext().GetXmlDataAndInsert();
            return Content(CurrentAccessor.HttpContext.CheckWX());
        }
        [HttpPost]
        [Route("menu/create")]
        public IActionResult CreateMenu()
        {
            var data = new ButtonParam() { button = new List<BaseButton>() };
            //添加一个一级菜单
            data.button.Add(new ViewButton() { name = "首页", type = ButtonType.view.ToString(), url = "http://277366155.cn" });

            //添加带有二级菜单的按钮
            var menuBtns = new SubButtonList() { name = "菜单管理", sub_button = new List<Button>() };
            menuBtns.sub_button.Add(new ViewButton() { name = "重建菜单", type = ButtonType.view.ToString(), url = "http://277366155.cn/wx/menu/create" });
            menuBtns.sub_button.Add(new ViewButton() { name = "查看菜单", type = ButtonType.view.ToString(), url = "http://277366155.cn/wx/menu/select" });
            data.button.Add(menuBtns);

            var subBtns=new SubButtonList() { name="其他", sub_button= new List<Button>() };
            subBtns.sub_button.Add(new ViewButton() { name="个人信息", type = ButtonType.view.ToString(), url = "http://277366155.cn/home/info" });
            data.button.Add(subBtns);
           return Content(WxApi.CreateMenu(data));
        }

        [HttpPost]
        [Route("menu/delete")]
        public IActionResult DeleteMenu()
        {
            return Content(WxApi.DeleteMenu());
        }

        [HttpGet]
        [Route("menu/select")]
        public IActionResult SelectMenu()
        {
            return Content(WxApi.SelectMenu());
        }
    }
}