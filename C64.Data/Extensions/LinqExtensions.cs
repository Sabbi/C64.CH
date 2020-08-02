using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace C64.Data.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Thank you thank you thank you
        /// https://stackoverflow.com/questions/39685787/how-to-make-a-dynamic-order-in-entity-framework
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="ordering"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, bool ascending = true)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            PropertyInfo property;
            Expression propertyAccess;
            if (ordering.Contains('.'))
            {
                // support to be sorted on child fields.
                String[] childProperties = ordering.Split('.');
                property = type.GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(T).GetProperty(ordering);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                                                             ascending ? "OrderBy" : "OrderByDescending",
                                                             new[] { type, property.PropertyType }, source.Expression,
                                                             Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}