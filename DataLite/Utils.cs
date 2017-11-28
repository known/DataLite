using System;

namespace DataLite
{
    /// <summary>
    /// 效用类。
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 判断对象值是否为空或空字符串。
        /// </summary>
        /// <param name="value">对象值。</param>
        /// <returns>为空或空字符串返回true，否则返回false。</returns>
        public static bool IsNullOrEmpty(object value)
        {
            return value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}
