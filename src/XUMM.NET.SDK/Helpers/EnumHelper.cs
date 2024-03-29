﻿using System;
using System.ComponentModel.DataAnnotations;

namespace XUMM.NET.SDK.Helpers;

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
                if (field.GetValue(null) is T value)
                {
                    return value;
                }
            }
        }

        throw new ArgumentOutOfRangeException(nameof(name), name, "Specified argument was out of the range of valid values.");
    }
}
