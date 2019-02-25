// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  InjectionConstructor.cs
**
** Description: Định nghĩa cách tiêm các thể hiện thuộc kho IContainer vào hàm tạo 
** trong lúc tạo thể hiện của một kiểu dữ liệu.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa cách tiêm các thể hiện thuộc kho <see cref="IContainer"/> vào hàm tạo
    /// trong lúc tạo thể hiện của một kiểu dữ liệu.
    /// </summary>
    internal class InjectionConstructor
    {
        public Type[] ParameterTypes { get; }

        /// <summary>
        /// Lấy hoặc đặt Khả năng có thể được Resolve bởi <see cref="IContainer"/>
        /// </summary>

        public InjectionConstructor()
        {
        }

        /// <summary>
        /// Hàm khởi tạo thể hiện của InjectionConstructor
        /// </summary>
        public InjectionConstructor(params Type[] parameters)
        {
            ParameterTypes = parameters;
        }


        /// <summary>
        /// Xác định hàm tạo có thể được gọi bởi kho chứa được chỉ định hay không
        /// </summary>
        /// <param name="container">Kho chứa để kiểm tra</param>
        /// <returns>Trả về true nếu hàm tạo có thể được gọi bởi kho chứa. Ngược lại, false.</returns>
        public bool Resolvable(IContainer container)
        {
            if (ParameterTypes?.Length > 0)
            {
                foreach (var parameterType in ParameterTypes)
                {
                    if (!container.IsTypeRegistered(parameterType))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
