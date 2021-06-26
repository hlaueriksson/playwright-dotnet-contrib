using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.PageObjects;

namespace PlaywrightContrib.Tests.PageObjects.Extensions
{
    [Parallelizable(ParallelScope.Self)]
    public class ElementHandleExtensionsTests : PageTest
    {
        private IElementHandle _elementHandle;

        [SetUp]
        public async Task SetUp()
        {
            await Page.SetContentAsync(Fake.Html);
            _elementHandle = await Page.QuerySelectorAsync("html");
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task To_returns_proxy_of_type()
        {
            var element = await _elementHandle.QuerySelectorAsync(".tweet");
            var result = element.To<FakeElementObject>();
            Assert.NotNull(result);
            Assert.NotNull(result.Element);

            result = ((IElementHandle)null).To<FakeElementObject>();
            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task To_returns_proxies_of_type()
        {
            var elements = await _elementHandle.QuerySelectorAllAsync("div");
            var result = elements.To<FakeElementObject>();
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.All.Matches<FakeElementObject>(x => x.Element != null));

            result = Array.Empty<IElementHandle>().To<FakeElementObject>();
            Assert.IsEmpty(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task QuerySelectorAllAsync_returns_proxies_of_type()
        {
            var result = await _elementHandle.QuerySelectorAllAsync<FakeElementObject>("div");
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.All.Matches<FakeElementObject>(x => x.Element != null));

            result = await _elementHandle.QuerySelectorAllAsync<FakeElementObject>(".missing");
            Assert.IsEmpty(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task QuerySelectorAsync_returns_proxy_of_type()
        {
            var result = await _elementHandle.QuerySelectorAsync<FakeElementObject>(".tweet");
            Assert.NotNull(result);
            Assert.NotNull(result.Element);

            result = await _elementHandle.QuerySelectorAsync<FakeElementObject>(".missing");
            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task WaitForSelectorAsync_returns_proxy_of_type()
        {
            var result = await _elementHandle.WaitForSelectorAsync<FakeElementObject>(".tweet");
            Assert.NotNull(result);
            Assert.NotNull(result.Element);

            Assert.ThrowsAsync<TimeoutException>(async () => await _elementHandle.WaitForSelectorAsync<FakeElementObject>(".missing", new ElementHandleWaitForSelectorOptions { Timeout = 1 }));
        }
    }
}
