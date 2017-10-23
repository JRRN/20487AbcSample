using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AbcSample.DAL.Storages.Blob
{
    public static class EnumExtension
    {
        public static string ToDescription(this Enum value)
        {
            DescriptionAttribute attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            Type type = value.GetType();
            MemberInfo[] memberInfo = type.GetMember(value.ToString());
            IEnumerable<T> attributes = memberInfo[0].GetCustomAttributes<T>(false);
            return attributes.FirstOrDefault();
        }
    }
}
