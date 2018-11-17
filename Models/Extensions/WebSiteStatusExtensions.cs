using System;
using System.Collections.Generic;
using System.Linq;
using SiteMonitoringTool.Controllers.Resources;

namespace SiteMonitoringTool.Models.Extensions
{
    public static class WebSiteStatusExtensions
    {
        public static WebSiteStatusResource ToResource(this WebSiteStatus model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var result = new WebSiteStatusResource
            {
                SiteName = model.SiteName,
                Url = model.Url,
                IsActive = model.IsActive  
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
    }
}