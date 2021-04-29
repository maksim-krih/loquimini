using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Loquimini.Common.Extentions
{
    public static class EnumExtensions
    {
        public static string GetName<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var displayAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DisplayAttribute), false)
                            .FirstOrDefault() as DisplayAttribute;

                        if (displayAttribute != null)
                        {
                            return displayAttribute.Name;
                        }
                    }
                }
            }

            return string.Empty;
        }
        
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var displayAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DisplayAttribute), false)
                            .FirstOrDefault() as DisplayAttribute;

                        if (displayAttribute != null)
                        {
                            return displayAttribute.Description;
                        }
                    }
                }
            }

            return string.Empty;
        }
        
        public static string GetGroupName<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var displayAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DisplayAttribute), false)
                            .FirstOrDefault() as DisplayAttribute;

                        if (displayAttribute != null)
                        {
                            return displayAttribute.GroupName;
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}