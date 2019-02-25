// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  ThreadOption.cs
**
** Description: 
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

namespace LightInject
{
    /// <summary>
    /// Xác định Thread mà ở đó một Subscriber của <see cref="PubSubEvent{T}"/> được gọi.
    /// </summary>
    public enum ThreadOption
    {
        /// <summary>
        /// Lời gọi được thực hiện trên cùng Thread với Publisher của <see cref="PubSubEvent{T}"/>.
        /// </summary>
        PublisherThread,

        /// <summary>
        /// Lời gọi được thực hiện trên UI Thread.
        /// </summary>
        UIThread,

        /// <summary>
        /// Lời gọi bất đồng bộ được thực hiện dưới nền.
        /// </summary>
        BackgroundThread
    }
}
