using System;
using System.Linq;

namespace DependencyInjectionContainer.Extensions
{
    public static class TypeExtensions
    {
        public static string ToGenericTypeString(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;
            
            var genericTypeName = type.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName[..genericTypeName.IndexOf('`')];
            var genericArgs = string.Join(", ",
                type.GetGenericArguments()
                    .Select(ToGenericTypeString).ToArray());
            return genericTypeName + "<" + genericArgs + ">";
        }
    }
}