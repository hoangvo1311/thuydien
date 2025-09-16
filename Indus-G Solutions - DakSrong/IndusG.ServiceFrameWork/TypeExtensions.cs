using System;
using System.Collections.Generic;
using System.Linq;

namespace IndusG.ServiceFrameWork
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class TypeExtensions
    {
        public static T GetAttribute<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            return GetAttributes<T>(typeWithAttributes).FirstOrDefault();
        }

        public static IEnumerable<T> GetAttributes<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            //Try to find the configuration attribute for the default logger if it exists
            object[] configAttributes = Attribute.GetCustomAttributes(typeWithAttributes,
                typeof(T), false);

            //Get just the first one
            if (configAttributes != null && configAttributes.Length > 0)
            {
                foreach (T attribute in configAttributes)
                {
                    yield return attribute;
                }
            }
        }
    }
}