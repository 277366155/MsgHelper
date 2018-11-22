using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MH.Web.Components
{
    public class NoticeViewComponent: ViewComponent
    {
        /// <summary>
        /// 公告栏组件
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
