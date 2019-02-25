// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  IContainer.cs
**
** Description: Định nghĩa Interface cho kho chứa các đối tượng dùng chung.
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa Interface cho kho chứa các đối tượng dùng chung.
    /// </summary>
    public interface IContainer
    {
        /*============================================================
        **
        **                          RESOLVE 
        **
        ===========================================================*/

        /// <summary>
        /// Tạo thể hiện có kiểu chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu của đối tượng được tạo</param>
        /// <returns>Đối tượng được tạo</returns>
        object Resolve(Type type);

        /// <summary>
        /// Tạo thể hiện có kiểu và tên được chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu của đối tượng được tạo</param>
        /// <param name="name">Tên của đối tượng được tạo</param>
        /// <returns>Đối tượng được tạo</returns>
        object Resolve(Type type, string name);

        /// <summary>
        /// Tạo thể hiện có kiểu chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của đối tượng được tạo</typeparam>
        /// <returns>Đối tượng được tạo</returns>
        T Resolve<T>();

        /// <summary>
        /// Tạo thể hiện có kiểu và tên được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của đối tượng được tạo</typeparam>
        /// <param name="name">Tên của đối tượng được tạo</param>
        /// <returns>Đối tượng được tạo</returns>
        T Resolve<T>(string name);

        /*============================================================
        **
        **                          REGISTER 
        **
        ===========================================================*/

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu để đăng ký</param>
        /// <param name="name">Tên</param>
        void Register(Type type, string name = null);

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <param name="from">Kiểu Interface</param>
        /// <param name="to">Kiểu dữ liệu thi hành Interface</param>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        void Register(Type from, Type to, string name = null);

        /// <summary>
        /// Đăng ký một kiểu với tên và hàm khởi tạo được chỉ định.
        /// </summary>
        /// <param name="from">Kiểu Interface</param>
        /// <param name="to">Kiểu dữ liệu thi hành Interface</param>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        void Register(Type from, Type to, Constructor constructor, string name = null);

        /// <summary>
        /// Đăng ký một kiểu với hàm tạo được chỉ định.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu để đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        void Register(Type type, Constructor constructor);

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="name">Tên</param>
        void Register<T>(string name = null);

        /// <summary>
        /// Đăng ký một kiểu có tên được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name">Tên</param>
        void Register<TFrom, TTo>(string name = null) where TTo : TFrom;

        /// <summary>
        /// Đăng ký một kiểu với tên và hàm khởi tạo được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        void Register<TFrom, TTo>(Constructor constructor, string name = null) where TTo : TFrom;

        /// <summary>
        /// Đăng ký một kiểu với hàm tạo được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        void Register<T>(Constructor constructor);

        /// <summary>
        /// Đăng ký một đối tượng có kiểu chỉ định lên kho.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu của đối tượng được đăng ký</param>
        /// <param name="instance">Đối tượng được đăng ký</param>
        void RegisterInstance(Type type, object instance);

        /// <summary>
        /// Đăng ký một đối tượng lên kho.
        /// </summary>
        /// <typeparam name="T">Interface mà đối tượng thi hành</typeparam>
        /// <param name="instance">Đối tượng được đăng ký</param>
        void RegisterInstance<T>(T instance);

        /// <summary>
        /// Đăng ký một kiểu Singleton.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        void RegisterSingleton<T>();

        /// <summary>
        /// Đăng ký một kiểu Singleton với tên được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="name">Tên</param>
        void RegisterSingleton<T>(string name);

        /// <summary>
        /// Đăng ký một kiểu Singleton.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        void RegisterSingleton<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        /// Đăng ký một kiểu Singleton với tên được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name"></param>
        void RegisterSingleton<TFrom, TTo>(string name) where TTo : TFrom;

        /// <summary>
        /// Đăng ký một kiểu với tên và hàm khởi tạo được chỉ định.
        /// </summary>
        /// <typeparam name="TFrom">Kiểu Interface</typeparam>
        /// <typeparam name="TTo">Kiểu dữ liệu thi hành Interface</typeparam>
        /// <param name="name">Tên của kiểu được đăng ký</param>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        void RegisterSingleton<TFrom, TTo>(Constructor constructor, string name = null) where TTo : TFrom;

        /// <summary>
        /// Đăng ký một kiểu với hàm tạo được chỉ định.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu để đăng ký</typeparam>
        /// <param name="constructor">Hàm khởi tạo được dùng để khởi tạo thể hiện</param>
        void RegisterSingleton<T>(Constructor constructor);

        /// <summary>
        /// Xác định một kiểu đã được đăng ký với tên chỉ định hay chưa.
        /// </summary>
        /// <param name="type">Kiểu dữ liệu để kiểm tra</param>
        /// <param name="name">Tên</param>
        /// <returns>Trả về true nếu kiểu đã được đăng ký. Ngược lại, false.</returns>
        bool IsTypeRegistered(Type type, string name = null);

        /// <summary>
        /// Lấy Kho chứa cha
        /// </summary>
        IContainer Parent { get; }

        /// <summary>
        /// Tạo một kho chứa con
        /// </summary>
        /// <returns>Kho chứa con</returns>
        IContainer CreateChildContainer();
    }
}
