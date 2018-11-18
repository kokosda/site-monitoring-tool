using System.ComponentModel.DataAnnotations;

namespace SiteMonitoringTool.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(31)]
        public string Username { get; set; }

        //TODO: change to hash
        [Required]
        [StringLength(63)]
        public string Password { get; set; }
    }
}