using System.Linq;
using Castle.DynamicProxy;
using Microsoft.Playwright.Contrib.PageObjects.DynamicProxy;
using NUnit.Framework;

namespace PlaywrightContrib.Tests.PageObjects.DynamicProxy
{
    public class InterceptorSelectorTests
    {
        [Test]
        public void SelectInterceptors_returns_SelectorInterceptor_for_properties_marked_with_Selector_attribute()
        {
            var subject = new InterceptorSelector();
            var interceptors = new IInterceptor[] { new SelectorInterceptor() };

            // PageObject

            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandleList)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObject)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectList)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            // ElementObject

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementHandle)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementHandleList)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementObject)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());

            methodInfo = typeof(FakeElementObject).GetProperty(nameof(FakeElementObject.SelectorForElementObjectList)).GetMethod;
            result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.IsInstanceOf<SelectorInterceptor>(result.Single());
        }

        [Test]
        public void SelectInterceptors_returns_other_interceptors_for_properties_not_marked_with_Selector_attribute()
        {
            var subject = new InterceptorSelector();
            var interceptors = new IInterceptor[] { new SelectorInterceptor(), new StandardInterceptor() };

            var methodInfo = typeof(string).GetMethod(nameof(string.GetTypeCode));
            var result = subject.SelectInterceptors(null, methodInfo, interceptors);
            Assert.AreSame(interceptors.Last(), result.Single());
        }
    }
}
