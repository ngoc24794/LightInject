// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  IServiceLocator.cs
**
** Description: Định nghĩa Interface cho lớp định vị dịch vụ
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa Interface cho lớp định vị dịch vụ
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Tạo thể hiện của kiểu dữ liệu được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để tạo thể hiện</typeparam>
        /// <returns>Thể hiện được tạo</returns>
        T Resolve<T>();
        object Resolve(Type type);
    }
}
