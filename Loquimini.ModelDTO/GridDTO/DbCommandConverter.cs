using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.GridDTO
{
    public static class DbCommandConverter
    {
        public static string BuildWhereClause<TEntity>(int index, IGridFilter filter,
    List<object> parameters)
        {
            var entityType = (typeof(TEntity));

            return filter.GetFilterCondition(entityType, parameters, index);
        }

        public static string BuildSelectClause<TEntity>(string field)
        {
            var entityType = (typeof(TEntity));
            var property = entityType.GetProperty(UppercaseFirst(field));

            try
            {
                if (property == null)
                {
                    string[] navigationProp = field.Split('.');

                    entityType = entityType.GetProperty(UppercaseFirst(navigationProp[0])).PropertyType;

                    property = entityType.GetProperty(UppercaseFirst(navigationProp[1]));
                }
            }
            catch
            {

                throw;
            }

            return field;
        }

        public static DateTime? ConvertJsDate(string jsDate)
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

        public static string ToLinqOperator(string @operator)
        {
            switch (@operator.ToLower())
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
                    return " && "; // &amp;&amp; todo
                default:
                    return null;
            }
        }

        public static string UppercaseFirst(string str)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
