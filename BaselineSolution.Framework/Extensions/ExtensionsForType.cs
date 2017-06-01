using System;
using System.Collections.Generic;
using System.Linq;

namespace BaselineSolution.Framework.Extensions
{
    public static class ExtensionsForType
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            if (type.BaseType == null)
                yield break;
            yield return type.BaseType;

            foreach (var baseType in GetBaseTypes(type.BaseType))
                yield return baseType;
        }


        public static bool HasAttribute(this Type @this, Type attributeType)
        {
            return Attribute.GetCustomAttributes(@this, attributeType).Any();
        }
    }
}
