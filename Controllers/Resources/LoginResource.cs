using System.ComponentModel.DataAnnotations;

namespace SiteMonitoringTool.Controllers.Resources
{
    public class LoginResource
    {
        [Required]
        [StringLength(31)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }
    }
}