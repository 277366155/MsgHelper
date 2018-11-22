using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MH.Web.Components
{
    public class SearchViewComponent : ViewComponent
    {
        /// <summary>
        /// 搜索框组件
        /// </summary>
        /// <returns></returns>
        public  async Task<IViewComponentResult> InvokeAsync() 
        {
            return  View();
        }
    }
}