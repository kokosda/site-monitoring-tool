using System;
using System.ComponentModel.DataAnnotations;

namespace SiteMonitoringTool.Controllers.Resources
{
    public class WebSiteStatusResource
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string SiteName { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public TimeSpan Interval { get; set; }

        public bool IsActive { get; set; }
    }
}