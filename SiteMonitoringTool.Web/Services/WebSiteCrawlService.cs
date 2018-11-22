using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SiteMonitoringTool.Models;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Services
{
    public class WebSiteCrawlService : IWebSiteCrawlService
    {
        private readonly ILogger<WebSiteCrawlService> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly HttpClient httpClient;

        public WebSiteCrawlService(ILogger<WebSiteCrawlService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.httpClient = new HttpClient();
        }

        public Task Crawl(WebSiteStatus webSiteStatus)
        {
            var result = httpClient.GetAsync(webSiteStatus.Url)
                .ContinueWith(hr => 
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

                    using (var scope = serviceProvider.CreateScope())
                    using (var dbContext = scope.ServiceProvider.GetRequiredService<SiteMonitoringToolDbContext>())
                    {
                        var wss = dbContext.WebSiteStatuses.FirstOrDefault(x => x.Id == webSiteStatus.Id);

                        if (wss == null)
                            return;

                        wss.IsActive = webSiteStatus.IsActive;
                        dbContext.SaveChanges();
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
