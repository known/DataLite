using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLite
{
    /// <summary>
    /// 扩展方法类。
    /// </summary>
    public static class Extension
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();

        /// <summary>
        /// 获取字符串字节长度。
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>字符串字节长度。</returns>
        public static int ByteLength(this string value)
        {
            return Encoding.Default.GetBytes(value).Length;
        }

        /// <summary>
        /// 将对象序列化成JSON格式字符串。
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>JSON格式字符串。</returns>
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 根据格式将字符串转换成可空日期。
        /// </summary>
        /// <param name="input">日期字符串。</param>
        /// <param name="format">日期格式。</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string input, string format)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out DateTime result))
                return result;

            return null;
        }

        /// <summary>
        /// 获取类型成员的指定类型的特性。
        /// </summary>
        /// <typeparam name="T">特性类型。</typeparam>
        /// <param name="member">类型成员。</param>
        /// <param name="inherit">是否继承查找。</param>
        /// <returns>指定类型的特性。</returns>
        public static T GetAttribute<T>(this MemberInfo member, bool inherit = true)
        {
            if (member == null)
                return default(T);

            foreach (var attr in member.GetCustomAttributes(inherit))
            {
                if (attr is T)
                {
                    return (T)attr;
                }
            }

            return default(T);
        }

        /// <summary>
        /// 获取类型的所有数据实体表栏位属性对象集合，默认约定可读写且非虚拟的Public属性为数据实体表栏位属性。
        /// </summary>
        /// <param name="type">实体类型。</param>
        /// <returns>所有数据实体表栏位属性对象集合。</returns>
        public static List<PropertyInfo> GetColumnProperties(this Type type)
        {
            if (type == null)
                return null;

            if (TypeProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pis))
                return pis.ToList();

            var properties = type.GetProperties()
                                 .Where(p => p.CanRead && p.CanWrite && !(p.SetMethod.IsVirtual && !p.SetMethod.IsFinal))
                                 .ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }
    }
}
