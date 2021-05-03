using System;
using System.Collections.Generic;
using System.Reflection;

namespace Loquimini.ModelDTO.GridDTO
{
    public class GridFilterDTO : IGridFilter
    {
        public string Operator { get; set; }

        public string Field { get; set; }

        public string Value { get; set; }

        public string Logic { get; set; }

        protected string ToLinqOperator()
        {
            switch (Operator.ToLower())
            {
                case "eq":
                    return " == ";
                case "neq":
                    return " != ";
                case "gte":
                    return " >= ";
                case "gt":
                    return " > ";
                case "lte":
                    return " <= ";
                case "lt":
                    return " < ";
                case "or":
                    return " || ";
                case "and":
                    return " && ";
                default:
                    return null;
            }
        }

        private string GetSimpleCondition(Type propertyType, int index)
        {
            switch (Operator.ToLower())
            {
                case "eq":
                case "neq":
                case "gte":
                case "gt":
                case "lte":
                case "lt":
                    if (typeof(DateTime).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(DateTime?).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(int).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(int?).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(decimal).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(decimal?).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(double).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(double?).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(bool).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(string).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(bool).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(Guid).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    if (typeof(Enum).IsAssignableFrom(propertyType))
                    {
                        return string.Format("{0}{1}@{2}",
                            Field,
                            ToLinqOperator(),
                            index);
                    }
                    return string.Format("{0}{1}@{2}",
                        Field,
                        ToLinqOperator(),
                        index);
                case "startswith":
                    return string.Format("({0} != null and {0}.StartsWith(@{1}))",
                        Field,
                        index);
                case "endswith":
                    return string.Format("({0} != null and {0}.EndsWith(@{1}))",
                        Field,
                        index);
                case "contains":
                    return string.Format("({0} != null and {0}.ToLower().Contains(@{1}))",
                        Field,
                        index);
                case "arraycontains":
                    return string.Format("({0} != null and {0}.Any(x=>x.Id==@{1}))",
                        Field,
                        index);
                case "doesnotcontain":
                    return string.Format("({0} != null and !{0}.ToLower().Contains(@{1}))",
                        Field,
                        index);
                default:
                    throw new ArgumentException("This operator is not yet supported for this Grid", Operator);
            }
        }

        private static string UppercaseFirst(string str)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        private static DateTime? ConvertJsDate(string jsDate)
        {
            const string formatString = "ddd MMM d yyyy HH:mm:ss";

            var gmtIndex = jsDate.IndexOf(" GMT", StringComparison.Ordinal);
            if (gmtIndex > -1)
            {
                jsDate = jsDate.Remove(gmtIndex);
                return DateTime.ParseExact(jsDate, formatString, null);
            }
            return DateTime.Parse(jsDate).AddDays(-1);

        }

        private void SetUpParams(Type entityType, PropertyInfo property, List<object> parameters)
        {

            switch (Operator.ToLower())
            {
                case "eq":
                case "neq":
                case "gte":
                case "gt":
                case "lte":
                case "lt":
                    if (typeof(DateTime).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(ConvertJsDate(Value));
                        break;
                    }
                    if (typeof(DateTime?).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(ConvertJsDate(Value));
                        break;
                    }
                    if (typeof(int).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(int.Parse(Value));
                        break;
                    }
                    if (typeof(int?).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(int.Parse(Value));
                        break;
                    }
                    if (typeof(decimal).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(int.Parse(Value));
                        break;
                    }
                    if (typeof(decimal?).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(int.Parse(Value));
                        break;
                    }
                    if (typeof(double).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(double.Parse(Value));
                        break;
                    }
                    if (typeof(double?).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(double.Parse(Value));
                        break;
                    }
                    if (typeof(bool).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(bool.Parse(Value));
                        break;
                    }
                    if (typeof(string).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(Value.ToString());
                        break;
                    }
                    if (typeof(bool).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(bool.Parse(Value));
                        break;
                    }
                    if (typeof(Guid).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(Guid.Parse(Value));
                        break;
                    }
                    if (typeof(Enum).IsAssignableFrom(property.PropertyType))
                    {
                        int number;
                        if (Int32.TryParse(Value, out number))
                        {
                            parameters.Add(Enum.ToObject(property.PropertyType, int.Parse(Value)));
                        }
                        break;
                    }
                    parameters.Add(Value);
                    break;
                case "startswith":
                    parameters.Add(Value);
                    break;
                case "endswith":
                    parameters.Add(Value);
                    break;
                case "contains":
                    parameters.Add(Value);
                    break;
                case "doesnotcontain":
                    parameters.Add(Value);
                    break;
                case "arraycontains":
                    parameters.Add(Value);
                    break;
                default:
                    throw new ArgumentException("This operator is not yet supported for this Grid", Operator);
            }
        }

        public string GetFilterCondition(Type entityType, List<object> parameters, int index, int recursionCounter = 0)
        {
            if (Value == null)
            {
                return null;
            }

            var property = entityType.GetProperty(UppercaseFirst(Field));

            SetUpParams(entityType, property, parameters);
            var getCondition = GetSimpleCondition(entityType.BaseType, index);

            return getCondition;
        }
    }
}
