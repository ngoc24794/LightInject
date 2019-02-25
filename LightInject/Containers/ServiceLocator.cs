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
    /// Định nghĩa lớp định vị dịch vụ
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        private readonly IContainer _container;
        public ServiceLocator(IContainer container)
        {
            _container = container;
            Current = this;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static IServiceLocator Current { get; private set; }
    }
}
