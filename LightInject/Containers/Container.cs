// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  Container.cs
**
** Description: Kho chứa các kiểu dữ liệu, các đối tượng dùng chung
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace LightInject
{
    /// <summary>
    /// Kho chứa các kiểu dữ liệu, các đối tượng dùng chung
    /// Kho cung cấp các chức năng:
    /// + Đăng ký một kiểu dữ liệu, một thể hiện đã có lên kho
    /// + Tạo thể hiện theo kiểu dữ liệu, theo hàm tạo được chỉ định
    /// + Cho phép tạo các kho chứa con
    /// </summary>
    public class Container : IContainer
    {
        private readonly Dictionary<MappingKey, Func<object>> _mappings;
        public IContainer Parent { get; private set; }
        public Container()
        {
            _mappings = new Dictionary<MappingKey, Func<object>>();
            if (!IsRegistered(typeof(IContainer)))
            {
                RegisterInstance(typeof(IContainer), this);
            }
        }

        #region Check Registered
        /// <summary>
        /// Xác định một kiểu đã được đăng ký với tên chỉ định hay chưa.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu để kiểm tra</param>
        /// <param name="name">Tên</param>
        /// <returns>Trả về true nếu kiểu đã được đăng ký. Ngược lại, false.</returns>
        public bool IsTypeRegistered(Type type, string name = null)
        {
            return IsRegistered(type, name);
        }
        #endregion

        #region Register

        #region Register Type

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <param name="from">Kiểu Interface</param>
        /// <param name="to">Kiểu dữ liệu thi hành Interface</param>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        public void Register(Type from, Type to, string name = null)
        {
            Func<object> createInstanceDelegate = () => CreateInstance(to);
            Register(from, createInstanceDelegate, name);
        }

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu để đăng ký</param>
        /// <param name="name">Tên</param>
        public void Register(Type type, string name = null)
        {
            Register(type, type, name);
        }

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="name">Tên</param>
        public void Register<T>(string name = null)
        {
            Register(typeof(T), name);
        }

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name">Tên</param>
        public void Register<TFrom, TTo>(string name = null) where TTo : TFrom
        {
            Register(typeof(TFrom), typeof(TTo), name);
        }

        /// <summary>
        /// Đăng ký một kiểu với hàm tạo được chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu để đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        public void Register(Type type, Constructor constructor)
        {
            if (constructor.Paramters?.Length > 0)
            {
                Register(type, constructor);
            }
            Register(type);
        }

        /// <summary>
        /// Đăng ký một kiểu với tên và hàm khởi tạo được chỉ định.
        /// </summary>
        /// <param name="from">Kiểu Interface</param>
        /// <param name="to">Kiểu dữ liệu thi hành Interface</param>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        public void Register(Type from, Type to, Constructor constructor, string name = null)
        {
            if (constructor.Paramters?.Length > 0)
            {
                Func<object> createInstanceDelegate = () => Activator.CreateInstance(to, constructor.Paramters);
                Register(from, createInstanceDelegate, name);
                return;
            }
            Register(from, to, name);
        }

        /// <summary>
        /// Đăng ký một kiểu với tên và hàm khởi tạo được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        public void Register<TFrom, TTo>(Constructor constructor, string name = null) where TTo : TFrom
        {
            Register(typeof(TFrom), typeof(TTo), constructor, name);
        }

        /// <summary>
        /// Đăng ký một kiểu với hàm tạo được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        public void Register<T>(Constructor constructor)
        {
            Register(typeof(T), typeof(T), constructor);
        }
        #endregion

        #region Register Instance
        /// <summary>
        /// Đăng ký một đối tượng có kiểu chỉ định lên kho.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu của đối tượng được đăng ký</param>
        /// <param name="instance">Đối tượng được đăng ký</param>
        public void RegisterInstance(Type type, object instance)
        {
            Func<object> createInstanceDelegate = () => instance;
            Register(type, createInstanceDelegate);
        }

        /// <summary>
        /// Đăng ký một đối tượng lên kho.
        /// </summary>
        /// <typeparam name="TInterface">Interface mà đối tượng thi hành</typeparam>
        /// <param name="instance">Đối tượng được đăng ký</param>
        public void RegisterInstance<T>(T instance)
        {
            Func<object> createInstanceDelegate = () => instance;
            Register(typeof(T), createInstanceDelegate);
        }
        #endregion

        #region Register Singleton
        /// <summary>
        /// Đăng ký một kiểu Singleton.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        public void RegisterSingleton<T>()
        {
            RegisterSingleton<T, T>();
        }

        /// <summary>
        /// Đăng ký một kiểu Singleton với tên được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="name">Tên</param>
        public void RegisterSingleton<T>(string name)
        {
            RegisterSingleton<T, T>(name);
        }

        /// <summary>
        /// Đăng ký một kiểu Singleton.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        public void RegisterSingleton<TFrom, TTo>() where TTo : TFrom
        {
            RegisterSingleton<TFrom, TTo>(null);
        }

        /// <summary>
        /// Đăng ký một kiểu Singleton với tên được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name"></param>
        public void RegisterSingleton<TFrom, TTo>(string name) where TTo : TFrom
        {
            Lazy<TTo> lazy = new Lazy<TTo>(() => (TTo)CreateInstance(typeof(TTo)));
            Func<object> createInstanceDelegate = () => lazy.Value;
            Register(typeof(TFrom), createInstanceDelegate, name);
        }

        /// <summary>
        /// Đăng ký một kiểu với tên và hàm khởi tạo được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        public void RegisterSingleton<TFrom, TTo>(Constructor constructor, string name = null) where TTo : TFrom
        {
            if (constructor.Paramters?.Length > 0)
            {
                Lazy<TTo> lazy = new Lazy<TTo>(() => (TTo)Activator.CreateInstance(typeof(TTo), constructor.Paramters));
                Func<object> createInstanceDelegate = () => lazy.Value;
                Register(typeof(TFrom), createInstanceDelegate, name);
                return;
            }
            RegisterSingleton<TFrom, TTo>(name);
        }

        /// <summary>
        /// Đăng ký một kiểu với hàm tạo được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        public void RegisterSingleton<T>(Constructor constructor)
        {
            RegisterSingleton<T, T>(constructor);
        }
        #endregion

        #endregion

        #region Resolve
        /// <summary>
        /// Tạo thể hiện có kiểu chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu của đối tượng được tạo</param>
        /// <returns>Đối tượng được tạo</returns>
        public object Resolve(Type type)
        {
            return Resolve(type, null);
        }

        /// <summary>
        /// Tạo thể hiện có kiểu chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của đối tượng được tạo</typeparam>
        /// <returns>Đối tượng được tạo</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T), null);
        }

        /// <summary>
        /// Tạo thể hiện có kiểu và tên được chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu của đối tượng được tạo</param>
        /// <param name="name">Tên của đối tượng được tạo</param>
        /// <returns>Đối tượng được tạo</returns>
        public object Resolve(Type type, string name)
        {
            var key = new MappingKey(type, name);
            Func<object> createInstance;

            if (_mappings.TryGetValue(key, out createInstance))
            {
                var instance = createInstance();
                return instance;
            }

            return CreateInstance(type);
        }

        /// <summary>
        /// Tạo thể hiện có kiểu và tên được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của đối tượng được tạo</typeparam>
        /// <param name="name">Tên của đối tượng được tạo</param>
        /// <returns>Đối tượng được tạo</returns>
        public T Resolve<T>(string name)
        {
            return (T)Resolve(typeof(T), name);
        }
        #endregion

        #region Prvivate Methods
        private void Register(Type type, Func<object> createInstanceDelegate, string instanceName = null)
        {
            MappingKey key = new MappingKey(type, instanceName);
            if (_mappings.ContainsKey(key))
            {
                _mappings.Remove(key);
            }
            _mappings.Add(key, createInstanceDelegate);
        }

        private bool IsRegistered(Type type, string instanceName = null)
        {
            var key = new MappingKey(type, instanceName);
            return _mappings.ContainsKey(key);
        }

        private object CreateInstance(Type to)
        {
            var constructorInfos = to.GetConstructors();
            if (constructorInfos?.Length > 0)
            {
                InjectionConstructor resolvableConstructor = ResolvableConstructor(constructorInfos);
                if (resolvableConstructor != null)
                {
                    if (resolvableConstructor.ParameterTypes != null)
                    {
                        object[] parameters = Resolve(resolvableConstructor.ParameterTypes);
                        return Activator.CreateInstance(to, parameters);
                    }
                    else
                    {
                        return Activator.CreateInstance(to);
                    }
                }
            }
            return null;
        }

        private object[] Resolve(Type[] types)
        {
            object[] result = new object[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                result[i] = Resolve(type);
            }
            return result;
        }

        private InjectionConstructor ResolvableConstructor(ConstructorInfo[] constructorInfos)
        {
            InjectionConstructor[] injectionConstructors = new InjectionConstructor[constructorInfos.Length];
            int validIndex = -1, max = -1;
            for (int i = 0; i < constructorInfos.Length; i++)
            {
                ConstructorInfo constructorInfo = constructorInfos[i];
                InjectionConstructor injectionConstructor = MappingConstructor(constructorInfo);
                if (injectionConstructor.Resolvable(this))
                {
                    injectionConstructors[i] = injectionConstructor;
                    int parameterCount = constructorInfo.GetParameters()?.Length ?? 0;
                    if (parameterCount > max)
                    {
                        max = parameterCount;
                        validIndex = i;
                    }
                }
            }
            return validIndex > -1 ? injectionConstructors[validIndex] : null;
        }

        private InjectionConstructor MappingConstructor(ConstructorInfo constructorInfo)
        {
            var parameterInfos = constructorInfo.GetParameters();
            if (parameterInfos?.Length > 0)
            {
                Type[] parameterTypes = new Type[parameterInfos.Length];
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    ParameterInfo parameterInfo = parameterInfos[i];
                    Type parameterType = parameterInfo.ParameterType;
                    parameterTypes[i] = parameterType;
                }
                return new InjectionConstructor(parameterTypes);
            }
            return new InjectionConstructor();
        }

        public IContainer CreateChildContainer()
        {
            IContainer childContainer = new Container()
            {
                Parent = this
            };
            return childContainer;
        }
        #endregion
    }
}
