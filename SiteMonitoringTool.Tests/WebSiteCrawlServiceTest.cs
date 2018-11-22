using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiteMonitoringTool.Models;
using SiteMonitoringTool.Persistence;
using SiteMonitoringTool.Services;

namespace SiteMonitoringTool.Tests
{
    [TestClass]
    public class WebSiteCrawlServiceTest
    {
        private MockRepository mockRepository;
        private WebSiteCrawlService webSiteCrawlService;

        public WebSiteCrawlServiceTest()
        {
            mockRepository = new MockRepository(MockBehavior.Default);
        }

        //TODO: write mo tests cases
        [TestMethod]
        public async Task CrawlTest()
        {
            // Arrange
            var logger = mockRepository.Create<ILogger<WebSiteCrawlService>>();
            var serviceCollection = new ServiceCollection();
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
            webSiteCrawlService = new WebSiteCrawlService(logger.Object, serviceProvider);

            // Act
            await webSiteCrawlService.Crawl(webSiteStatus);

            // Assert
            Assert.AreEqual(true, webSiteStatus.IsActive);
        }
    }
}