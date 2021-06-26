using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace PlaywrightContrib.PageObjects.DynamicProxy
{
    [Serializable]
    internal class ProxyGenerationHook : IProxyGenerationHook
    {
        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo) =>
            methodInfo.IsGetterPropertyWithAttribute<SelectorAttribute>() &&
            methodInfo.IsReturningAsyncResult();

        public override bool Equals(object? obj) =>
            obj != null &&
            obj.GetType() == typeof(ProxyGenerationHook);

        public override int GetHashCode() =>
            GetType().GetHashCode();
    }
}
