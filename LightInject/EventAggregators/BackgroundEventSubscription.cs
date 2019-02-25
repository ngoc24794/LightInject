// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  BackgroundEventSubscription.cs
**
** Description: Định nghĩa một lớp cho phép đăng ký sự kiện dưới nền.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;
using System.Threading.Tasks;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa một lớp cho phép đăng ký sự kiện dưới nền.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối số sự kiện</typeparam>
    public class BackgroundEventSubscription<T> : EventSubscription<T>
    {
        public BackgroundEventSubscription(Action<T> action) : base(action)
        {
        }

        protected override void InvokeAction(Action<T> action, T argument)
        {
            Task.Run(() => action(argument));
        }
    }
}