// ==++==
// 
//   Copyright (c) Huong Viet.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  MappingKey.cs
**
** Description: Định nghĩa một khóa ánh xạ
** 
** Author: Nguyen Van Ngoc
** 
** Hitory: 
===========================================================*/

using System;

namespace LightInject
{
    /// <summary>
    /// Định nghĩa một khóa ánh xạ
    /// </summary>
    internal class MappingKey
    {
        /// <summary>
        /// Kiểu dữ liệu phụ thuộc
        /// </summary>
        public Type Type { get; protected set; }

        /// <summary>
        /// Tên của thể hiện
        /// </summary>
        public string InstanceName { get; protected set; }


        /// <summary>
        /// Tạo một thể hiện mới của <see cref="MappingKey"/>
        /// </summary>
        /// <param name="type">Kiểu phụ thuộc</param>
        /// <param name="instanceName">Tên của thể hiện</param>
        public MappingKey(Type type, string instanceName)
        {
            Type = type;
            InstanceName = instanceName;
        }

        /// <summary>
        /// Trả về mã băm của đối tượng này
        /// </summary>
        /// <returns>Mã băm của đối tượng này</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + Type.GetHashCode();
                hash = hash * multiplier + (InstanceName == null ? 0 : InstanceName.GetHashCode());

                return hash;
            }
        }


        /// <summary>
        /// Xác định đối tượng được chỉ định có bằng đối tượng này hay không
        /// </summary>
        /// <param name="obj">Đối tượng để so sánh với đối tượng này</param>
        /// <returns>
        /// <c>true</c> nếu đối tượng được chỉ định bằng đối tượng này; ngược lại, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            MappingKey compareTo = obj as MappingKey;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (compareTo == null)
                return false;

            return Type.Equals(compareTo.Type) &&
                string.Equals(InstanceName, compareTo.InstanceName, StringComparison.InvariantCultureIgnoreCase);
        }        
    }
}
