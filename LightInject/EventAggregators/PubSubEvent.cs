// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  PubSubEvent.cs
**
** Description: Định nghĩa một lớp để quản lý việc Publish và Subscribe các sự kiện.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa một lớp để quản lý việc Publish và Subscribe các sự kiện.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PubSubEvent<T> : Event
    {
        public void Publish(T eventArg)
        {
            InternalPublish(eventArg);
        }

        /// <summary>
        /// Đăng ký một đơn vị xử lý sự kiện
        /// </summary>
        /// <param name="subscriber">Đơn vị xử lí sự kiện được đăng ký</param>
        public void Subscribe(Action<T> subscriber)
        {
            Subscribe(subscriber, ThreadOption.PublisherThread);
        }

        /// <summary>
        /// Đăng ký một đơn vị xử lý sự kiện
        /// </summary>
        /// <param name="subscriber">Đơn vị xử lí sự kiện được đăng ký</param>
        /// <param name="threadOption">Tùy chọn Thread ở đó thực hiện thao tác đăng ký</param>
        public void Subscribe(Action<T> subscriber, ThreadOption threadOption)
        {
            InternalSubscribe(subscriber, threadOption);
        }
    }
}
