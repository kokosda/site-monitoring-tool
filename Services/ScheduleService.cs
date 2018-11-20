using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SiteMonitoringTool.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ICollection<ActionState> actions;
        private readonly object @lock;
        private readonly Random random;

        public ScheduleService()
        {
            @lock = new object();
            actions = new Collection<ActionState>();
            random = new Random();
        }

        public void Dispose()
        {
            lock(@lock)
            {
                var temp = actions.ToArray();

                foreach(var actionState in temp)
                {
                    actionState.Timer.Dispose();
                    actionState.Timer = null;
                    actions.Remove(actionState);
                }
            }
        }

        public long Schedule(Func<Task> action, TimeSpan interval)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var actionState = new ActionState
            {
                Id = DateTime.UtcNow.Ticks + random.Next(1, 1000000),
                Action = action,
                Interval = interval,
                IsExecuting = false
            };
            actionState.Timer = new Timer(InitTimer, actionState, dueTime: TimeSpan.Zero, period: interval);

            lock(@lock)
            {                
                actions.Add(actionState);
            }

            var result = actionState.Id;
            return result;
        }

        public void Unschedule(long id)
        {
            var actionState = actions.FirstOrDefault(a => a.Id == id);
            
            if (actionState == null)
                return;

            actionState.Timer.Dispose();
            actionState.Timer = null;

            lock(@lock)
            {
                actions.Remove(actionState);
            }
        }

        private void InitTimer(object @object)
        {
            var actionState = @object as ActionState;

            if (actionState.IsExecuting)
                return;

            actionState.IsExecuting = true;

            Task.Factory.StartNew(async state => 
            {
                var @as = state as ActionState;
                await @as.Action();
                @as.IsExecuting = false;
            }, actionState);
        }

        private class ActionState
        {
            private volatile bool isExecuting;

            public long Id { get; set; }
            public Func<Task> Action { get; set; }
            public TimeSpan Interval { get; set; }
            public Timer Timer { get; set; }
            public bool IsExecuting
            {
                get { return isExecuting; }
                set { isExecuting = value; }
            }
        }
    }
}