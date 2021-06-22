using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Microsoft.Playwright.Contrib.PageObjects.DynamicProxy
{
    [Serializable]
    internal class InterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) =>
            method.IsGetterPropertyWithAttribute<SelectorAttribute>() ?
                interceptors.Where(x => x is SelectorInterceptor).ToArray() :
                interceptors.Where(x => !(x is SelectorInterceptor)).ToArray();
    }
}
