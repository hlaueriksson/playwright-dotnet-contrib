using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.PageObjects;
using Microsoft.Playwright.Contrib.PageObjects.DynamicProxy;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.PageObjects.DynamicProxy
{
    [Parallelizable(ParallelScope.Self)]
    public class InvocationExtensionsTests : PageTest
    {
        [SetUp]
        public async Task SetUp() => await Page.SetContentAsync(Fake.Html);

        // GetReturnValueAsync

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_value_from_PageObject()
        {
            var pageObject = new FakePageObject();
            pageObject.Initialize(Page, null);
            var methodInfo = pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, pageObject);

            var result = await invocation.GetReturnValueAsync();

            Assert.NotNull(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_value_from_ElementObject()
        {
            var elementHandle = await Page.QuerySelectorAsync("html");
            var elementObject = new FakeElementObject();
            elementObject.Initialize(elementHandle);
            var methodInfo = elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, elementObject);

            var result = await invocation.GetReturnValueAsync();

            Assert.NotNull(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_null_for_member_without_SelectorAttribute()
        {
            var methodInfo = typeof(string).GetMethod(nameof(string.GetTypeCode));
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync();

            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_null_for_non_PageObject_or_ElementObject()
        {
            var objectWithNoBaseClass = new FakeObjectWithNoBaseClass();
            var methodInfo = objectWithNoBaseClass.GetType().GetProperty(nameof(FakeObjectWithNoBaseClass.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo, objectWithNoBaseClass);

            var result = await invocation.GetReturnValueAsync();

            Assert.Null(result);
        }

        // PageObject

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementHandle_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var pageObject = new FakePageObject();
            pageObject.Initialize(Page, null);
            var methodInfo = pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(pageObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IElementHandle>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementHandle_list_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var pageObject = new FakePageObject();
            pageObject.Initialize(Page, null);
            var methodInfo = pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandleList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(pageObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<IElementHandle>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementObject_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var pageObject = new FakePageObject();
            pageObject.Initialize(Page, null);
            var methodInfo = pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementObject)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(pageObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementObject_list_for_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var pageObject = new FakePageObject();
            pageObject.Initialize(Page, null);
            var methodInfo = pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementObjectList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(pageObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<FakeElementObject>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_null_for_property_on_PageObject_marked_with_SelectorAttribute_when_Page_is_null()
        {
            var pageObject = new FakePageObject();
            var methodInfo = pageObject.GetType().GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(pageObject);

            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_null_for_invalid_property_on_PageObject_marked_with_SelectorAttribute()
        {
            var pageObject = new FakePageObject();
            pageObject.Initialize(Page, null);
            var type = pageObject.GetType();

            var methodInfo = type.GetProperty(nameof(FakePageObject.SelectorForWrongReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            var result = await invocation.GetReturnValueAsync(pageObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakePageObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(pageObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakePageObject.SelectorForElementHandleListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(pageObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakePageObject.SelectorForElementObjectWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(pageObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakePageObject.SelectorForElementObjectListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(pageObject);
            Assert.Null(result);
        }

        // ElementObject

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementHandle_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var elementHandle = await Page.QuerySelectorAsync("html");
            var elementObject = new FakeElementObject();
            elementObject.Initialize(elementHandle);
            var methodInfo = elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(elementObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IElementHandle>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementHandle_list_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var elementHandle = await Page.QuerySelectorAsync("html");
            var elementObject = new FakeElementObject();
            elementObject.Initialize(elementHandle);
            var methodInfo = elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandleList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(elementObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<IElementHandle>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementObject_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var elementHandle = await Page.QuerySelectorAsync("html");
            var elementObject = new FakeElementObject();
            elementObject.Initialize(elementHandle);
            var methodInfo = elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementObject)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(elementObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_ElementObject_list_for_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var elementHandle = await Page.QuerySelectorAsync("html");
            var elementObject = new FakeElementObject();
            elementObject.Initialize(elementHandle);
            var methodInfo = elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementObjectList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(elementObject);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<FakeElementObject>>(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_null_for_property_on_ElementObject_marked_with_SelectorAttribute_when_Element_is_null()
        {
            var elementObject = new FakeElementObject();
            var methodInfo = elementObject.GetType().GetProperty(nameof(FakeElementObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);

            var result = await invocation.GetReturnValueAsync(elementObject);

            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetReturnValueAsync_returns_null_for_invalid_property_on_ElementObject_marked_with_SelectorAttribute()
        {
            var elementHandle = await Page.QuerySelectorAsync("html");
            var elementObject = new FakeElementObject();
            elementObject.Initialize(elementHandle);
            var type = elementObject.GetType();

            var methodInfo = type.GetProperty(nameof(FakeElementObject.SelectorForWrongReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            var result = await invocation.GetReturnValueAsync(elementObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakeElementObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(elementObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakeElementObject.SelectorForElementHandleListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(elementObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakeElementObject.SelectorForElementObjectWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(elementObject);
            Assert.Null(result);

            methodInfo = type.GetProperty(nameof(FakeElementObject.SelectorForElementObjectListWithNonTaskReturnType)).GetMethod;
            invocation = new FakeInvocation(methodInfo);
            result = await invocation.GetReturnValueAsync(elementObject);
            Assert.Null(result);
        }

        // Internal

        [Test]
        public void HasValidReturnType_returns_true_for_invocation_that_return_Task_of_T()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.True(invocation.HasValidReturnType());
        }

        [Test]
        public void HasValidReturnType_returns_false_for_invocation_that_does_not_return_Task_of_T()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.False(invocation.HasValidReturnType());
        }

        [Test]
        public void IsGetterPropertyWithAttribute_returns_true_for_invocation_of_getter_property_marked_with_given_attribute()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.True(invocation.IsGetterPropertyWithAttribute<SelectorAttribute>());
        }

        [Test]
        public void IsGetterPropertyWithAttribute_returns_false_for_invocation_of_non_getter_property()
        {
            var methodInfo = typeof(string).GetMethod(nameof(string.GetTypeCode));
            var invocation = new FakeInvocation(methodInfo);
            Assert.False(invocation.IsGetterPropertyWithAttribute<SelectorAttribute>());
        }

        [Test]
        public void GetAttribute_returns_the_given_attribute()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.NotNull(invocation.GetAttribute<SelectorAttribute>());
        }

        [Test]
        public void IsReturning_returns_true_for_invocation_of_method_that_returns_Task_of_given_type()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandle)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.True(invocation.IsReturning<IElementHandle>());
        }

        [Test]
        public void IsReturning_returns_false_for_invocation_of_method_that_does_not_return_Task_of_given_type()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementHandleWithNonTaskReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.False(invocation.IsReturning<IElementHandle>());
        }

        [Test]
        public void IsReturningElementObject_returns_true_for_invocation_of_method_that_returns_Task_of_ElementObject_subclass()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObject)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.True(invocation.IsReturningElementObject());
        }

        [Test]
        public void IsReturningElementObject_returns_false_for_invocation_of_method_that_does_not_return_Task_of_ElementObject_subclass()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectWithNonTaskReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.False(invocation.IsReturningElementObject());
        }

        [Test]
        public void IsReturningElementObjectList_returns_true_for_invocation_of_method_that_returns_Task_of_ElementObject_subclass_list()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectList)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.True(invocation.IsReturningElementObjectList());
        }

        [Test]
        public void IsReturningElementObjectList_returns_false_for_invocation_of_method_that_does_not_return_Task_of_ElementObject_subclass_list()
        {
            var methodInfo = typeof(FakePageObject).GetProperty(nameof(FakePageObject.SelectorForElementObjectListWithNonTaskReturnType)).GetMethod;
            var invocation = new FakeInvocation(methodInfo);
            Assert.False(invocation.IsReturningElementObjectList());
        }
    }
}
