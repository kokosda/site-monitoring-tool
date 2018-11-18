using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMonitoringTool.Controllers.Resources;
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
        public async Task<IActionResult> Get()
        {
            var webSiteStatuses = await dbContext.WebSiteStatuses.ToListAsync();
            var data = webSiteStatuses.ToResourceCollection();
            var result = Ok(data);
            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WebSiteStatusResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(resource);
        }
    }
};