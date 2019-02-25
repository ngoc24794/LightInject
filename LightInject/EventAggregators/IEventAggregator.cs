// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  IEventAggregator.cs
**
** Description: Định nghĩa Interface để lấy các thể hiện của một kiểu sự kiện.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

namespace LightInject
{
    /// <summary>
    /// Định nghĩa Interface để lấy các thể hiện của một kiểu sự kiện.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Lấy một sự kiện có kiểu đã cho.
        /// </summary>
        /// <typeparam name="TEventType">Kiểu sự kiện để lấy</typeparam>
        /// <returns>Một thể hiện của kiểu <typeparamref name="TEventType"/></returns>
        TEventType GetEvent<TEventType>() where TEventType : Event, new();
    }
}
