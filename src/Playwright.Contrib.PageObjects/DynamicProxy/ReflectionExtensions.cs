using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Playwright.Contrib.PageObjects.DynamicProxy
{
    internal static class ReflectionExtensions
    {
        public static bool IsGetterPropertyWithAttribute<T>(this MethodInfo methodInfo)
            where T : Attribute
        {
            if (!methodInfo.IsGetter())
            {
                return false;
            }

            var property = methodInfo.DeclaringType.GetProperty(methodInfo);

            return property.HasAttribute<T>();
        }

        public static bool IsGetter(this MethodInfo methodInfo) =>
            methodInfo.IsSpecialName &&
            methodInfo.IsCompilerGenerated() &&
            methodInfo.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase);

        public static bool HasAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute =>
            propertyInfo.GetCustomAttributes<T>().Any();

        public static T GetAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute =>
            propertyInfo.GetCustomAttribute<T>();

        public static PropertyInfo GetProperty(this Type rootType, MethodInfo methodInfo) =>
            rootType.GetProperty(methodInfo.Name.Substring(4), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        public static bool IsReturningAsyncResult(this MethodInfo methodInfo) =>
            methodInfo.ReturnType.IsGenericType &&
            methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);

        internal static bool IsCompilerGenerated(this MemberInfo memberInfo) =>
            memberInfo.GetCustomAttributes<CompilerGeneratedAttribute>().Any();
    }
}
