using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMonitoringTool.Controllers.Resources;
using SiteMonitoringTool.Models.Extensions;
using SiteMonitoringTool.Persistence;
using SiteMonitoringTool.Services;

namespace SiteMonitoringTool.Controllers
{
    [Route("/api/websitestatuses")]
    public class WebSiteStatusesController : Controller
    {
        private readonly SiteMonitoringToolDbContext dbContext;
        private readonly IScheduleService scheduleService;
        private readonly IWebSiteCrawlService webSiteCrawlService;

        public WebSiteStatusesController(SiteMonitoringToolDbContext dbContext, IScheduleService scheduleService, IWebSiteCrawlService webSiteCrawlService)
        {
            this.dbContext = dbContext;
            this.scheduleService = scheduleService;
            this.webSiteCrawlService = webSiteCrawlService;
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

            var webSiteStatus = resource.ToWebSiteStatus();
            webSiteStatus.LastActionId = scheduleService.Schedule(() => webSiteCrawlService.Crawl(webSiteStatus), webSiteStatus.Interval);
            dbContext.WebSiteStatuses.Add(webSiteStatus);
            await dbContext.SaveChangesAsync();

            var result = Ok(webSiteStatus.ToResource());
            return result;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WebSiteStatusResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var webSiteStatus = await dbContext.WebSiteStatuses.FirstOrDefaultAsync(wss => wss.Id == id);

            if (webSiteStatus == null)
            {
                ModelState.AddModelError(nameof(webSiteStatus), $"{nameof(webSiteStatus)} was not found.");
                return BadRequest(ModelState);
            }

            webSiteStatus.UpdateFromResource(resource);

            if (webSiteStatus.LastActionId.HasValue)
                scheduleService.Unschedule(webSiteStatus.LastActionId.Value);
            
            webSiteStatus.LastActionId = scheduleService.Schedule(() => webSiteCrawlService.Crawl(webSiteStatus), webSiteStatus.Interval);
            dbContext.WebSiteStatuses.Update(webSiteStatus);
            await dbContext.SaveChangesAsync();

            var result = Ok(webSiteStatus.ToResource());
            return result;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var webSiteStatus = await dbContext.WebSiteStatuses.FirstOrDefaultAsync(wss => wss.Id == id);

            if (webSiteStatus == null)
            {
                ModelState.AddModelError(nameof(webSiteStatus), $"{nameof(webSiteStatus)} was not found.");
                return BadRequest(ModelState);
            }

            if (webSiteStatus.LastActionId.HasValue)
                scheduleService.Unschedule(webSiteStatus.LastActionId.Value);
            
            dbContext.WebSiteStatuses.Remove(webSiteStatus);
            await dbContext.SaveChangesAsync();

            var result = Ok();
            return result;
        }
    }
};