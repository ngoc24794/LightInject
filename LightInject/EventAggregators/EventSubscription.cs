// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  EventSubscription.cs
**
** Description: Định nghĩa Interface để lấy các thể hiện của một kiểu sự kiện.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa Interface để lấy các thể hiện của một kiểu sự kiện.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối số sự kiện</typeparam>
    public class EventSubscription<T> : IEventSubscription
    {
        public EventSubscription(Action<T> action)
        {
            Action = action;
        }

        public Action<T> Action { get; private set; }
        public Action<object[]> GetExecutionStrategy()
        {
            Action<T> action = Action;
            if (action != null)
            {
                return arguments =>
                {
                    T argument = default(T);
                    if (arguments?.Length > 0 && arguments[0] != null)
                    {
                        argument = (T)arguments[0];
                    }
                    InvokeAction(action, argument);
                };
            }
            return null;
        }

        protected virtual void InvokeAction(Action<T> action, T argument)
        {
            action(argument);
        }
    }
}
