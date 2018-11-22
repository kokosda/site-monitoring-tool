using System;
using System.Threading.Tasks;

namespace SiteMonitoringTool.Services
{
    public interface IScheduleService : IDisposable
    {
        ///<summary>
        /// returns Id of scheduled action.
        ///</summary>
        long Schedule(Func<Task> action, TimeSpan interval);

        void Unschedule(long id);
    }
}