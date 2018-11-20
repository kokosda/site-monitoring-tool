using Microsoft.EntityFrameworkCore;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Services
{
    public class StartUpService : IStartUpService
    {
        private readonly SiteMonitoringToolDbContext dbContext;
        private readonly IScheduleService scheduleService;
        private readonly IWebSiteCrawlService webSiteCrawlService;

        public StartUpService(SiteMonitoringToolDbContext dbContext, IScheduleService scheduleService, IWebSiteCrawlService webSiteCrawlService)
        {
            this.dbContext = dbContext;
            this.scheduleService = scheduleService;
            this.webSiteCrawlService = webSiteCrawlService;
        }

        public async void ScheduleWebSitesCrawling()
        {
            await dbContext.WebSiteStatuses.ForEachAsync(wss => 
            {
                var actionId = scheduleService.Schedule(() => webSiteCrawlService.Crawl(dbContext, wss), wss.Interval);
                wss.LastActionId = actionId;
                dbContext.WebSiteStatuses.Update(wss);
            });

            await dbContext.SaveChangesAsync();
        }
    }
}