using System.Collections.Generic;

namespace DataLite.Mapping
{
    /// <summary>
    /// 实体基类。
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public EntityBase()
        {
            IsNew = true;
        }

        internal bool IsNew { get; set; }

        /// <summary>
        /// 验证实体数据。
        /// </summary>
        /// <returns>验证器。</returns>
        public List<string> Validate()
        {
            var errors = new List<string>();
            var properties = GetType().GetColumnProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(this, null);
                var attr = property.GetAttribute<ColumnAttribute>();
                if (attr != null)
                    attr.Validate(value, errors);
            }
            return errors;
        }
    }
}
