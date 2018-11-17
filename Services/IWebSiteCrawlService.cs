using System.Threading.Tasks;

namespace SiteMonitoringTool.Services
{
    public interface IWebSiteCrawlService
    {
        Task Crawl();
    }
}