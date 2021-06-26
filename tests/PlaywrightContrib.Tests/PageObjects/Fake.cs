using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Playwright;
using Microsoft.Playwright.Contrib.PageObjects;

namespace PlaywrightContrib.Tests.PageObjects
{
    public static class Fake
    {
        public static string Html => "<html><body><div class='tweet'><div class='like'>100</div><div class='retweets'>10</div></div></body></html>";
    }

    public class FakePageObject : PageObject
    {
        [Selector(".tweet")]
        public virtual Task<IElementHandle> SelectorForElementHandle { get; }

        [Selector("div")]
        public virtual Task<IReadOnlyList<IElementHandle>> SelectorForElementHandleList { get; }

        [Selector(".retweets")]
        public virtual Task<FakeElementObject> SelectorForElementObject { get; }

        [Selector("div")]
        public virtual Task<IReadOnlyList<FakeElementObject>> SelectorForElementObjectList { get; }

        // Fail

        [Selector(".tweet")]
        public virtual Task<object> SelectorForWrongReturnType { get; }

        [Selector(".tweet")]
        public virtual IElementHandle SelectorForElementHandleWithNonTaskReturnType { get; }

        [Selector("div")]
        public virtual IReadOnlyList<IElementHandle> SelectorForElementHandleListWithNonTaskReturnType { get; }

        [Selector(".retweets")]
        public virtual FakeElementObject SelectorForElementObjectWithNonTaskReturnType { get; }

        [Selector("div")]
        public virtual IReadOnlyList<FakeElementObject> SelectorForElementObjectListWithNonTaskReturnType { get; }
    }

    public class FakeElementObject : ElementObject
    {
        [Selector(".tweet")]
        public virtual Task<IElementHandle> SelectorForElementHandle { get; }

        [Selector("div")]
        public virtual Task<IReadOnlyList<IElementHandle>> SelectorForElementHandleList { get; }

        [Selector(".retweets")]
        public virtual Task<FakeElementObject> SelectorForElementObject { get; }

        [Selector("div")]
        public virtual Task<IReadOnlyList<FakeElementObject>> SelectorForElementObjectList { get; }

        // Fail

        [Selector(".tweet")]
        public virtual Task<object> SelectorForWrongReturnType { get; }

        [Selector(".tweet")]
        public virtual IElementHandle SelectorForElementHandleWithNonTaskReturnType { get; }

        [Selector("div")]
        public virtual IReadOnlyList<IElementHandle> SelectorForElementHandleListWithNonTaskReturnType { get; }

        [Selector(".retweets")]
        public virtual FakeElementObject SelectorForElementObjectWithNonTaskReturnType { get; }

        [Selector("div")]
        public virtual IReadOnlyList<FakeElementObject> SelectorForElementObjectListWithNonTaskReturnType { get; }
    }

    public class FakeObjectWithNoBaseClass
    {
        [Selector(".tweet")]
        public virtual Task<IElementHandle> SelectorForElementHandle { get; }
    }

    public class FakeInvocation : AbstractInvocation
    {
        public FakeInvocation(MethodInfo proxiedMethod, object proxy = null) : base(proxy, null, proxiedMethod, null)
        {
            TargetType = proxiedMethod.DeclaringType;
            InvocationTarget = proxy;
        }

        protected override void InvokeMethodOnTarget() => throw new NotImplementedException();

        public override object InvocationTarget { get; }
        public override Type TargetType { get; }
        public override MethodInfo MethodInvocationTarget { get; }
    }
}
