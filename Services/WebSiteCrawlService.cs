using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SiteMonitoringTool.Models;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Services
{
    public class WebSiteCrawlService : IWebSiteCrawlService
    {
        private readonly SiteMonitoringToolDbContext dbContext;
        private readonly ILogger<WebSiteCrawlService> logger;
        private readonly HttpClient httpClient;

        public WebSiteCrawlService(ILogger<WebSiteCrawlService> logger)
        {
            this.logger = logger;
            this.httpClient = new HttpClient();
        }

        public Task Crawl(SiteMonitoringToolDbContext dbContext, WebSiteStatus webSiteStatus)
        {
            var result = httpClient.GetAsync(webSiteStatus.Url)
                .ContinueWith(async hr => 
                {
                    try
                    {
                        logger.LogInformation($"{webSiteStatus.Url} was queried.");
                        var httpResponse = hr.Result;

                        if (!httpResponse.IsSuccessStatusCode)
                            webSiteStatus.IsActive = false;
                        else
                            webSiteStatus.IsActive = true;
                        httpResponse.Dispose();
                    }
                    catch (Exception ex)
                    {
                        webSiteStatus.IsActive = false;
                        logger.LogError(ex, $"Error querying {webSiteStatus.Url}.");
                    }
                    finally
                    {
                        dbContext.WebSiteStatuses.Update(webSiteStatus);
                        await dbContext.SaveChangesAsync();
                    }
                });
            return result;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}