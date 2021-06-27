using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.PageObjects.DynamicProxy;

namespace PlaywrightContrib.Tests.PageObjects.DynamicProxy
{
    [Parallelizable(ParallelScope.Self)]
    public class SelectorInterceptorTests : PageTest
    {
        private SelectorInterceptor _subject;
        private FakePageObject _pageObject;
        private FakeElementObject _elementObject;

        [SetUp]
        public async Task SetUp()
        {
            await Page.SetContentAsync(Fake.Html);

            _subject = new SelectorInterceptor();
            _pageObject = new FakePageObject();
            _pageObject.Initialize(Page, null);
            _elementObject = new FakeElementObject();
            _elementObject.Initialize(await Page.QuerySelectorAsync("html"));
        }

        // PageObject

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementHandle_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _pageObject);

            _subject.Intercept(invocation);
            var result = await (Task<IElementHandle>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<IElementHandle>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementHandle_list_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandleList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _pageObject);

            _subject.Intercept(invocation);
            var result = await (Task<IReadOnlyList<IElementHandle>>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<IElementHandle>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementObject_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementObject)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _pageObject);

            _subject.Intercept(invocation);
            var result = await (Task<FakeElementObject>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementObject_list_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementObjectList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _pageObject);

            _subject.Intercept(invocation);
            var result = await (Task<IReadOnlyList<FakeElementObject>>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<FakeElementObject>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public void Intercept_sets_the_ReturnValue_to_null_for_invalid_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForWrongReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _pageObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _pageObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandleListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _pageObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementObjectWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _pageObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementObjectListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _pageObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);
        }

        // ElementObject

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementHandle_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _elementObject);

            _subject.Intercept(invocation);
            var result = await (Task<IElementHandle>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<IElementHandle>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementHandle_list_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandleList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _elementObject);

            _subject.Intercept(invocation);
            var result = await (Task<IReadOnlyList<IElementHandle>>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<IElementHandle>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementObject_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementObject)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _elementObject);

            _subject.Intercept(invocation);
            var result = await (Task<FakeElementObject>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Intercept_sets_the_ReturnValue_to_Task_of_ElementObject_list_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementObjectList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _elementObject);

            _subject.Intercept(invocation);
            var result = await (Task<IReadOnlyList<FakeElementObject>>)invocation.ReturnValue;

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<FakeElementObject>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public void Intercept_sets_the_ReturnValue_to_null_for_invalid_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForWrongReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, _elementObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _elementObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandleListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _elementObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementObjectWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _elementObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);

            methodInfo = _elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementObjectListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo, _elementObject);
            _subject.Intercept(invocation);
            Assert.Null(invocation.ReturnValue);
        }

        // Unknown

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public void Intercept_sets_the_ReturnValue_to_null_for_property_on_unknown_object_marked_with_SelectorAttribute()
        {
            var objectWithNoBaseClass = new FakeObjectWithNoBaseClass();
            var methodInfo = objectWithNoBaseClass.GetType().GetProperty(nameof(FakeObjectWithNoBaseClass.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, objectWithNoBaseClass);

            _subject.Intercept(invocation);

            Assert.Null(invocation.ReturnValue);
        }
    }
}
