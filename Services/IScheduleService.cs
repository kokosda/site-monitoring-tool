using System;

namespace SiteMonitoringTool.Services
{
    public interface IScheduleService
    {
        void Schedule(Action action);
    }
}