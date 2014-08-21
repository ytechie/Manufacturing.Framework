using System;
using System.Collections.Generic;
using System.Linq;

namespace Manufacturing.Framework.Utility
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> ExcludeTypes(this IEnumerable<Type> applyToTypes, IEnumerable<Type> excludeTypes)
        {
            return applyToTypes.Where(x => !excludeTypes.Contains(x));
        }
    }
}
