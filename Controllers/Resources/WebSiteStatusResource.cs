using System.ComponentModel.DataAnnotations;

namespace SiteMonitoringTool.Controllers.Resources
{
    public class WebSiteStatusResource
    {
        [Required]
        [StringLength(255)]
        public string SiteName { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }
    }
}