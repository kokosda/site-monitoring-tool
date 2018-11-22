using System;
using System.Collections.Generic;
using System.Linq;
using SiteMonitoringTool.Controllers.Resources;

namespace SiteMonitoringTool.Models.Extensions
{
    public static class WebSiteStatusExtensions
    {
        public static WebSiteStatusResource ToResource(this WebSiteStatus webSiteStatus)
        {
            if (webSiteStatus == null)
                throw new ArgumentNullException(nameof(webSiteStatus));

            var result = new WebSiteStatusResource
            {
                Id = webSiteStatus.Id,
                SiteName = webSiteStatus.SiteName,
                Url = webSiteStatus.Url,
                IsActive = webSiteStatus.IsActive,
                LastUpdated = webSiteStatus.LastUpdated,
                Interval = webSiteStatus.Interval
            };
            return result;
        }

        public static ICollection<WebSiteStatusResource> ToResourceCollection(this ICollection<WebSiteStatus> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            var result = collection.Select(wss => wss.ToResource()).ToList();
            return result;
        }

        public static WebSiteStatus ToWebSiteStatus(this WebSiteStatusResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            var result = new WebSiteStatus
            {
                Interval = resource.Interval,
                LastUpdated = DateTime.UtcNow,
                SiteName = resource.SiteName,
                Url = resource.Url
            };

            return result;
        }

        public static void UpdateFromResource(this WebSiteStatus webSiteStatus, WebSiteStatusResource resource)
        {
            if (webSiteStatus == null)
                throw new ArgumentNullException(nameof(webSiteStatus));

            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            webSiteStatus.Interval = resource.Interval;
            webSiteStatus.SiteName = resource.SiteName;
            webSiteStatus.Url = resource.Url;
            webSiteStatus.LastUpdated = DateTime.UtcNow;
        }
    }
}