using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HttpPcl.Utilities
{
    public static class Extensions
    {
        public static bool HasProperty(this Type obj, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return false;
            return obj.GetRuntimeProperty(propertyName) != null;
        }
    }
}
