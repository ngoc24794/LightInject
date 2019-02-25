// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  Event.cs
**
** Description: Định nghĩa một lớp cơ sở để Publish và Subscribe các sự kiện.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa một lớp cơ sở để Publish và Subscribe các sự kiện.
    /// </summary>
    public abstract class Event
    {
        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        /// <summary>
        /// Trả về danh sách <see cref="IEventSubscription"/> hiện có.
        /// </summary>
        /// <value>Các <see cref="IEventSubscription"/> hiện có</value>
        protected ICollection<IEventSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        public SynchronizationContext SynchronizationContext { get; set; }

        protected void InternalSubscribe<T>(Action<T> subscriber, ThreadOption threadOption)
        {
            EventSubscription<T> subscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    subscription = new EventSubscription<T>(subscriber);
                    break;
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundEventSubscription<T>(subscriber);
                    break;
                case ThreadOption.UIThread:
                    subscription = new DispatcherEventSubscription<T>(subscriber, SynchronizationContext);
                    break;
                default:
                    subscription = new EventSubscription<T>(subscriber);
                    break;
            }

            lock (Subscriptions)
            {
                Prune();
                Subscriptions.Add(subscription);
            }
        }

        protected void Prune()
        {
            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    if (_subscriptions[i].GetExecutionStrategy() == null)
                    {
                        _subscriptions.RemoveAt(i);
                    }
                }
            }
        }

        protected void InternalPublish(params object[] arguments)
        {
            List<Action<object[]>> executionStrategies = PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            lock (Subscriptions)
            {
                Prune();
                return _subscriptions.Select(x => x.GetExecutionStrategy()).ToList();
            }
        }
    }
}
