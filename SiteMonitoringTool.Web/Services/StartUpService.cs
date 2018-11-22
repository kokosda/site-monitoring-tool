using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Services
{
    public class StartUpService : IStartUpService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IScheduleService scheduleService;
        private readonly IWebSiteCrawlService webSiteCrawlService;

        public StartUpService(IServiceProvider serviceProvider, IScheduleService scheduleService, IWebSiteCrawlService webSiteCrawlService)
        {
            this.serviceProvider = serviceProvider;
            this.scheduleService = scheduleService;
            this.webSiteCrawlService = webSiteCrawlService;
        }

        public void ScheduleWebSitesCrawling()
        {
            using(var scope = serviceProvider.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<SiteMonitoringToolDbContext>())
            {
                var list = dbContext.WebSiteStatuses.ToList();
                
                foreach (var wss in list)
                {
                    var actionId = scheduleService.Schedule(() => webSiteCrawlService.Crawl(wss), wss.Interval);
                    wss.LastActionId = actionId;
                };

                dbContext.SaveChanges();
            }
        }
    }
}