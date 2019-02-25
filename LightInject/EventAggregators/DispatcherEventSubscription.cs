// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  DispatcherEventSubscription.cs
**
** Description: Định nghĩa một lớp cho phép đăng ký một sự kiện đồng bộ.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;
using System.Threading;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa một lớp cho phép đăng ký một sự kiện đồng bộ.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối số sự kiện</typeparam>
    public class DispatcherEventSubscription<T> : EventSubscription<T>
    {
        private SynchronizationContext _synchronizationContext;

        public DispatcherEventSubscription(Action<T> action) : base(action)
        {
        }

        public DispatcherEventSubscription(Action<T> subscriber, SynchronizationContext synchronizationContext) : base(subscriber)
        {
            _synchronizationContext = synchronizationContext;
        }

        protected override void InvokeAction(Action<T> action, T argument)
        {
            _synchronizationContext.Post((o) => action((T)o), argument);
        }
    }
}