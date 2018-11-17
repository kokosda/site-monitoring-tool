using System.ComponentModel.DataAnnotations;

namespace SiteMonitoringTool.Models
{
    public class WebSiteStatus
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string SiteName { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}