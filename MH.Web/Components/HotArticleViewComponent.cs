using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MH.Web.Components
{
    public class HotArticleViewComponent : ViewComponent
    {
        /// <summary>
        /// 热门文章列表
        /// </summary>
        /// <returns></returns>
        public  async Task<IViewComponentResult> InvokeAsync() 
        {
            return  View();
        }
    }
}