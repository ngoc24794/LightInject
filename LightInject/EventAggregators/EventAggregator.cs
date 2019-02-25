// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  EventAggregator.cs
**
** Description: Định nghĩa Interface để lấy các thể hiện của một kiểu sự kiện.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa Interface để lấy các thể hiện của một kiểu sự kiện.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, Event> _events = new Dictionary<Type, Event>();
        private readonly SynchronizationContext syncContext = SynchronizationContext.Current;

        /// <summary>
        /// Lấy một thể hiện của một kiểu sự kiện.
        /// </summary>
        /// <typeparam name="TEventType">Kiểu sự kiện để lấy</typeparam>
        /// <returns>Một thể hiện của kiểu <typeparamref name="TEventType"/></returns>
        public TEventType GetEvent<TEventType>() where TEventType : Event, new()
        {

            //---------------------------------------------------------------------------
            // Duyệt trong Dictionary tìm sự kiện theo kiễu dữ liệu được chỉ định.
            // Nếu đã tồn tại sự kiện có kiểu đã cho thì trả về sự kiện đó.
            // Ngược lại, tạo một thể hiện mới của sự kiện có kiểu đã cho và trả về nó.
            //---------------------------------------------------------------------------
            lock (_events)
            {
                if (!_events.TryGetValue(typeof(TEventType), out Event existingEvent))
                {
                    TEventType newEvent = new TEventType
                    {
                        SynchronizationContext = syncContext
                    };
                    _events[typeof(TEventType)] = newEvent;

                    return newEvent;
                }
                else
                {
                    return (TEventType)existingEvent;
                }
            }
        }
    }
}
