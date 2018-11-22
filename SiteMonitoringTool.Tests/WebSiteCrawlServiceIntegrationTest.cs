

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteMonitoringTool.Models;
using SiteMonitoringTool.Persistence;
using SiteMonitoringTool.Services;

namespace SiteMonitoringTool.Tests
{
    [TestClass]
    public class WebSiteCrawlServiceIntegrationTest
    {
        // TODO: create integration test with ScheduleService
        [TestMethod]
        public async Task CrawlTest()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddSingleton<IWebSiteCrawlService, WebSiteCrawlService>();

            var webSiteStatus = new WebSiteStatus { Id = 1, Url = "http://ya.ru" };

            serviceCollection.AddScoped(typeof(SiteMonitoringToolDbContext), sp =>
            {
                var options = new DbContextOptionsBuilder<SiteMonitoringToolDbContext>()
                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                    .Options;
                var funcResult = new SiteMonitoringToolDbContext(options);
                funcResult.WebSiteStatuses.Add(webSiteStatus);
                return funcResult;
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var webSiteCrawlService = serviceProvider.GetService<IWebSiteCrawlService>();

            // Act
            await webSiteCrawlService.Crawl(webSiteStatus);

            // Assert
            Assert.AreEqual(true, webSiteStatus.IsActive);
        }
    }
}