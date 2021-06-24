using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Microsoft.Playwright.Contrib.PageObjects.DynamicProxy
{
    internal static class ProxyFactory
    {
        private static readonly ProxyGenerator _proxyGenerator = new();
        private static readonly ProxyGenerationOptions _options = new(new ProxyGenerationHook()) { Selector = new InterceptorSelector() };

        public static T? PageObject<T>(IPage? page, IResponse? response)
            where T : PageObject
        {
            if (page == null)
            {
                return default;
            }

            var proxy = _proxyGenerator.CreateClassProxy<T>(_options, new SelectorInterceptor());
            proxy.Initialize(page, response);

            return proxy;
        }

        public static T? ElementObject<T>(IElementHandle? elementHandle)
            where T : ElementObject
        {
            if (elementHandle == null)
            {
                return default;
            }

            var proxy = _proxyGenerator.CreateClassProxy<T>(_options, new SelectorInterceptor());
            proxy.Initialize(elementHandle);

            return proxy;
        }

        public static ElementObject? ElementObject(Type proxyType, IElementHandle? elementHandle)
        {
            if (elementHandle == null)
            {
                return default;
            }

            var proxy = _proxyGenerator.CreateClassProxy(proxyType, _options, new SelectorInterceptor()) as ElementObject;
            proxy?.Initialize(elementHandle);

            return proxy;
        }

        public static object ElementObjectList(Type proxyType, IReadOnlyList<IElementHandle> elementHandles)
        {
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(proxyType))!;
            var listType = list.GetType();
            var methodInfo = listType.GetMethod("Add")!;

            foreach (var elementHandle in elementHandles)
            {
                methodInfo.Invoke(list, new object[] { ElementObject(proxyType, elementHandle)! });
            }

            methodInfo = listType.GetMethod("AsReadOnly")!;

            return methodInfo.Invoke(list, Array.Empty<object>())!;
        }
    }
}
