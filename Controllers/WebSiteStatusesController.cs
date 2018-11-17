using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMonitoringTool.Models.Extensions;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Controllers
{
    [Route("/api/websitestatuses")]
    public class WebSiteStatusesController : Controller
    {
        private readonly SiteMonitoringToolDbContext dbContext;
        public WebSiteStatusesController(SiteMonitoringToolDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var webSiteStatuses = await dbContext.WebSiteStatuses.ToListAsync();
            var data = webSiteStatuses.ToResourceCollection();
            var result = Ok(data);
            return result;
        }
    }
}