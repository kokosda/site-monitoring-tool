using Microsoft.EntityFrameworkCore;

namespace SiteMonitoringTool.Persistence
{
    public class SiteMonitoringToolDbContext : DbContext
    {
        public SiteMonitoringToolDbContext(DbContextOptions<SiteMonitoringToolDbContext> options)
            : base(options)
        {
            
        }
    }
}