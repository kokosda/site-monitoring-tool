using Microsoft.EntityFrameworkCore;
using SiteMonitoringTool.Models;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Persistence
{
    public class SiteMonitoringToolDbContext : DbContext
    {
        public SiteMonitoringToolDbContext(DbContextOptions<SiteMonitoringToolDbContext> options)
            : base(options)
        {            
        }

        public DbSet<WebSiteStatus> WebSiteStatuses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}