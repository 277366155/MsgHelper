using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MH.Web.Components
{
    public class BannerViewComponent : ViewComponent
    {
        /// <summary>
        /// banner图
        /// </summary>
        /// <returns></returns>
        public  async Task<IViewComponentResult> InvokeAsync() 
        {
            return  View();
        }
    }
}