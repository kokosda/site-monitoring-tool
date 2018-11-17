using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Services
{
    public class WebSiteCrawlService : IWebSiteCrawlService
    {
        private readonly SiteMonitoringToolDbContext dbContext;
        private readonly ILogger<WebSiteCrawlService> logger;

        public WebSiteCrawlService(SiteMonitoringToolDbContext dbContext, ILogger<WebSiteCrawlService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task Crawl()
        {
            var httpClient = new HttpClient();
            var httpResponseTasks = new Collection<Task>();
            
            await dbContext.WebSiteStatuses.ForEachAsync(wss =>
                {
                    try
                    {
                        var httpResponseTask = httpClient.GetAsync(wss.Url)
                            .ContinueWith(hr => 
                            {
                                var httpResponse = hr.Result;

                                if (!httpResponse.IsSuccessStatusCode)
                                    wss.IsActive = false;
                                else
                                    wss.IsActive = true;

                                httpResponse.Dispose();
                                dbContext.WebSiteStatuses.Update(wss);
                            });

                        httpResponseTasks.Add(httpResponseTask);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error while querying {wss.Url}.");
                    }
                });

            await Task.WhenAll(httpResponseTasks)
                .ContinueWith(async t => 
                {
                    httpClient.Dispose();
                    await dbContext.SaveChangesAsync();
                });
        }
    }
}