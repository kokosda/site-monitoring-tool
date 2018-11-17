namespace SiteMonitoringTool.Controllers.Resources
{
    public class WebSiteStatusResource
    {
        public string SiteName { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }        
    }
}