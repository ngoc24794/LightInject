// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  IEventSubscription.cs
**
** Description: Định nghĩa Interface cho đơn vị xử lý sự kiện được dùng bởi Event
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa Interface cho đơn vị xử lý sự kiện được dùng bởi <see cref="Event"/>
    /// </summary>
    public interface IEventSubscription
    {
        /// <summary>
        /// Trả về một hành động thực hiện chức năng đăng ký sự kiện
        /// </summary>
        /// <returns></returns>
        Action<object[]> GetExecutionStrategy();
    }
}
