using NUnit.Framework;
using PlaywrightContrib.PageObjects.DynamicProxy;

namespace PlaywrightContrib.Tests.PageObjects.DynamicProxy
{
    public class ProxyGenerationHookTests
    {
        [Test]
        public void ShouldInterceptMethod_returns_true_for_properties_marked_with_Selector_attribute()
        {
            var subject = new ProxyGenerationHook();

            // PageObject

            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandleList)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObject)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectList)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            // ElementObject

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementHandle)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementHandleList)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementObject)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementObjectList)).GetMethod;
            Assert.True(subject.ShouldInterceptMethod(null, methodInfo));
        }

        [Test]
        public void ShouldInterceptMethod_returns_false_for_properties_not_marked_with_Selector_attribute()
        {
            var subject = new ProxyGenerationHook();

            var methodInfo = typeof(string).GetProperty(nameof(string.Length)).GetMethod;
            Assert.False(subject.ShouldInterceptMethod(null, methodInfo));
        }

        [Test]
        public void ShouldInterceptMethod_returns_false_for_invalid_properties_marked_with_Selector_attribute()
        {
            var subject = new ProxyGenerationHook();

            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            Assert.False(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandleListWithNonTaskReturnType)).GetMethod;
            Assert.False(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectWithNonTaskReturnType)).GetMethod;
            Assert.False(subject.ShouldInterceptMethod(null, methodInfo));

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectListWithNonTaskReturnType)).GetMethod;
            Assert.False(subject.ShouldInterceptMethod(null, methodInfo));
        }

        [Test]
        public void caching_should_work()
        {
            var subject = new ProxyGenerationHook();

            Assert.True(subject.Equals(new ProxyGenerationHook()));

            var hashCode = subject.GetHashCode();

            Assert.AreEqual(new ProxyGenerationHook().GetHashCode(), hashCode);
        }
    }
}
