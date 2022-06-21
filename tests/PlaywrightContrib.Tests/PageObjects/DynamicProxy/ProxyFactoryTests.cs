using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.PageObjects.DynamicProxy;

namespace PlaywrightContrib.Tests.PageObjects.DynamicProxy
{
    public class ProxyFactoryTests : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            await Page.SetContentAsync(Fake.Html);
        }

        [Test]
        public void PageObject_returns_proxy_of_type()
        {
            var result = ProxyFactory.PageObject<FakePageObject>(Page, null);
            Assert.NotNull(result);
            Assert.IsInstanceOf<FakePageObject>(result);

            result = ProxyFactory.PageObject<FakePageObject>(null, null);
            Assert.Null(result);
        }

        [Test]
        public async Task ElementObject_returns_proxy_of_type()
        {
            var elementHandle = await Page.QuerySelectorAsync(".tweet");
            var result = ProxyFactory.ElementObject<FakeElementObject>(elementHandle);
            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);

            result = ProxyFactory.ElementObject<FakeElementObject>(null);
            Assert.Null(result);
        }

        [Test]
        public async Task ElementObject_returns_proxy_of_given_type()
        {
            var elementHandle = await Page.QuerySelectorAsync(".tweet");
            var result = ProxyFactory.ElementObject(typeof(FakeElementObject), elementHandle);
            Assert.NotNull(result);
            Assert.IsInstanceOf<FakeElementObject>(result);

            result = ProxyFactory.ElementObject(typeof(FakeElementObject), null);
            Assert.Null(result);
        }

        [Test]
        public async Task ElementObjectList_returns_proxy_of_given_type()
        {
            var elementHandles = await Page.QuerySelectorAllAsync("div");
            var result = ProxyFactory.ElementObjectList(typeof(FakeElementObject), elementHandles);
            Assert.IsNotEmpty(result as IList);
            Assert.IsInstanceOf<IReadOnlyList<FakeElementObject>>(result);

            result = ProxyFactory.ElementObjectList(typeof(FakeElementObject), Array.Empty<IElementHandle>());
            Assert.IsEmpty(result as IList);
        }
    }
}
