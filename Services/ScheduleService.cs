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
        private readonly Timer timer;
        private ICollection<ActionState> actions;
        private readonly object @lock;

        public ScheduleService()
        {
            @lock = new object();
            actions = new Collection<ActionState>();
            timer = new Timer(InitTimer, null, dueTime: TimeSpan.Zero, period: new TimeSpan(0, 0, 10));
        }

        public void Schedule(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock(@lock)
            {
                actions.Add(new ActionState
                {
                    Action = action,
                    IsExecuting = false
                });
            }
        }

        private void InitTimer(object o)
        {
            var arrayOfActions = new ActionState[0];

            lock(@lock)
            {
                arrayOfActions = actions.ToArray();
            }

            foreach (var action in arrayOfActions)
            {
                var tempAction = action;

                if (tempAction.IsExecuting)
                    continue;

                tempAction.IsExecuting = true;

                Task.Factory.StartNew(state => 
                {
                    var @as = state as ActionState;
                    @as.Action();
                }, tempAction)
                .ContinueWith((t, state) => 
                {
                    var @as = state as ActionState;
                    @as.IsExecuting = false;
                }, tempAction);
            }
        }

        private class ActionState
        {
            private volatile bool isExecuting;

            public Action Action { get; set; }
            public bool IsExecuting
            {
                get { return isExecuting; }
                set { isExecuting = value; }
            }
        }
    }
}