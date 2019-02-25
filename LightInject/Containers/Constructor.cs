// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  Constructor.cs
**
** Description: Định nghĩa một lớp chỉ định hàm tạo thể hiện được thực hiện bởi kho chứa
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

namespace LightInject
{
    /// <summary>
    /// Định nghĩa một lớp chỉ định hàm tạo thể hiện được thực hiện bởi <see cref="IContainer"/>
    /// </summary>
    public class Constructor
    {
        /// <summary>
        /// Hàm khởi tạo thể hiện của Constructor
        /// </summary>
        /// <param name="paramters">Các tham số đầu vào cho hàm tạo thể hiện</param>
        public Constructor(params object[] paramters)
        {
            Paramters = paramters;
        }

        /// <summary>
        /// Các tham số đầu vào cho hàm tạo thể hiện
        /// </summary>
        public object[] Paramters { get; set; }
    }
}
