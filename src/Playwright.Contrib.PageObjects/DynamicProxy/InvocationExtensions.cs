using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Microsoft.Playwright.Contrib.PageObjects.DynamicProxy
{
    internal static class InvocationExtensions
    {
        public static async Task<object> GetReturnValueAsync(this IInvocation invocation)
        {
            if (!invocation.IsGetterPropertyWithAttribute<SelectorAttribute>())
            {
                return null;
            }

            return invocation.InvocationTarget switch
            {
                PageObject pageObject => await invocation.GetReturnValueAsync(pageObject).ConfigureAwait(false),
                ElementObject elementObject => await invocation.GetReturnValueAsync(elementObject).ConfigureAwait(false),
                _ => null,
            };
        }

        internal static async Task<object> GetReturnValueAsync(this IInvocation invocation, PageObject pageObject)
        {
            var page = pageObject.Page;

            if (page == null)
            {
                return null;
            }

            var attribute = invocation.GetAttribute<SelectorAttribute>();

            if (invocation.IsReturning<IElementHandle>())
            {
                return await page.QuerySelectorAsync(attribute.Selector).ConfigureAwait(false);
            }

            if (invocation.IsReturning<IReadOnlyList<IElementHandle>>())
            {
                return await page.QuerySelectorAllAsync(attribute.Selector).ConfigureAwait(false);
            }

            if (invocation.IsReturningElementObject())
            {
                var proxyType = invocation.Method.ReturnType.GetGenericArguments()[0];
                var elementHandle = await page.QuerySelectorAsync(attribute.Selector).ConfigureAwait(false);

                return ProxyFactory.ElementObject(proxyType, elementHandle);
            }

            if (invocation.IsReturningElementObjectList())
            {
                var listType = invocation.Method.ReturnType.GetGenericArguments()[0];
                var proxyType = listType.GetGenericArguments()[0];
                var elementHandles = await page.QuerySelectorAllAsync(attribute.Selector).ConfigureAwait(false);

                return ProxyFactory.ElementObjectList(proxyType, elementHandles);
            }

            return null;
        }

        internal static async Task<object> GetReturnValueAsync(this IInvocation invocation, ElementObject elementObject)
        {
            var element = elementObject.Element;

            if (element == null)
            {
                return null;
            }

            var attribute = invocation.GetAttribute<SelectorAttribute>();

            if (invocation.IsReturning<IElementHandle>())
            {
                return await element.QuerySelectorAsync(attribute.Selector).ConfigureAwait(false);
            }

            if (invocation.IsReturning<IReadOnlyList<IElementHandle>>())
            {
                return await element.QuerySelectorAllAsync(attribute.Selector).ConfigureAwait(false);
            }

            if (invocation.IsReturningElementObject())
            {
                var proxyType = invocation.Method.ReturnType.GetGenericArguments()[0];
                var elementHandle = await element.QuerySelectorAsync(attribute.Selector).ConfigureAwait(false);

                return ProxyFactory.ElementObject(proxyType, elementHandle);
            }

            if (invocation.IsReturningElementObjectList())
            {
                var listType = invocation.Method.ReturnType.GetGenericArguments()[0];
                var proxyType = listType.GetGenericArguments()[0];
                var elementHandles = await element.QuerySelectorAllAsync(attribute.Selector).ConfigureAwait(false);

                return ProxyFactory.ElementObjectList(proxyType, elementHandles);
            }

            return null;
        }

        internal static bool HasValidReturnType(this IInvocation invocation)
        {
            var returnType = invocation.Method.ReturnType;

            return typeof(Task).IsAssignableFrom(returnType) && returnType.IsGenericType;
        }

        internal static bool IsGetterPropertyWithAttribute<T>(this IInvocation invocation)
            where T : Attribute
        {
            if (!invocation.Method.IsGetter())
            {
                return false;
            }

            var property = invocation.TargetType.GetProperty(invocation.Method);

            return property.HasAttribute<T>();
        }

        internal static T GetAttribute<T>(this IInvocation invocation)
            where T : Attribute
        {
            var property = invocation.TargetType.GetProperty(invocation.Method);

            return property.GetAttribute<T>();
        }

        internal static bool IsReturning<T>(this IInvocation invocation) =>
            invocation.Method.ReturnType == typeof(Task<T>);

        internal static bool IsReturningElementObject(this IInvocation invocation) =>
            invocation.Method.ReturnType.IsGenericType &&
            invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>) &&
            typeof(ElementObject).IsAssignableFrom(invocation.Method.ReturnType.GetGenericArguments()[0]);

        internal static bool IsReturningElementObjectList(this IInvocation invocation) =>
            invocation.Method.ReturnType.IsGenericType &&
            invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>) &&
            typeof(IReadOnlyList<ElementObject>).IsAssignableFrom(invocation.Method.ReturnType.GetGenericArguments()[0]);
    }
}
