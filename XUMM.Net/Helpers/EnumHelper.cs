using System;
using System.ComponentModel.DataAnnotations;

namespace XUMM.Net.Helpers
{
    public static class EnumHelper
    {
        public static T GetValueFromName<T>(string name) where T : Enum
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
                if (attribute?.Name == name || field.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(name));
        }
    }
}
