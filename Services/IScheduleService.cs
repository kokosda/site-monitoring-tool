using System;
using System.Threading.Tasks;

namespace SiteMonitoringTool.Services
{
    public interface IScheduleService
    {
        void Schedule(Func<Task> action);
    }
}