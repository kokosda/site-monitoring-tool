using System;
using System.Threading.Tasks;
using SiteMonitoringTool.Models;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Services
{
    public interface IWebSiteCrawlService : IDisposable
    {
        Task Crawl(WebSiteStatus WebSiteStatus);
    }
}