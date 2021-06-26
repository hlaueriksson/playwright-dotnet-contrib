using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.PageObjects;

namespace PlaywrightContrib.Tests.PageObjects.Extensions
{
    [Parallelizable(ParallelScope.Self)]
    public class PageExtensionsTests : PageTest
    {
        [SetUp]
        public async Task SetUp() => await Page.SetContentAsync(Fake.Html);

        // PageObject

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public void To_returns_proxy_of_type()
        {
            var result = Page.To<FakePageObject>();
            Assert.NotNull(result);

            result = ((IPage)null).To<FakePageObject>();
            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GotoAsync_returns_proxy_of_type()
        {
            var result = await Page.GotoAsync<FakePageObject>("about:blank");
            Assert.NotNull(result);

            result = await Page.GotoAsync<FakePageObject>("https://github.com/microsoft/playwright-dotnet");
            Assert.NotNull(result.Page);
            Assert.NotNull(result.Response);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.GotoAsync<FakePageObject>("https://github.com/microsoft/playwright-dotnet", new PageGotoOptions { Timeout = 1 }));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task WaitForNavigationAsync_returns_proxy_of_type()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Page.ClickAsync("h1 > strong > a");
            var result = await Page.WaitForNavigationAsync<FakePageObject>();
            Assert.NotNull(result);
            Assert.NotNull(result.Page);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.WaitForNavigationAsync<FakePageObject>(new PageWaitForNavigationOptions { Timeout = 1 }));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task RunAndWaitForNavigationAsync_returns_proxy_of_type()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var result = await Page.RunAndWaitForNavigationAsync<FakePageObject>(async () => await Page.ClickAsync("h1 > strong > a"));
            Assert.NotNull(result);
            Assert.NotNull(result.Page);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.RunAndWaitForNavigationAsync<FakePageObject>(async () => await Page.ClickAsync("h1 > strong > a"), new PageRunAndWaitForNavigationOptions { Timeout = 1 }));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task WaitForResponseAsync_returns_proxy_of_type()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Page.ClickAsync("a span[data-content='Actions']");
            var result = await Page.WaitForResponseAsync<FakePageObject>(response => response.Url == "https://api.github.com/_private/browser/stats" && response.Status == 200);
            Assert.NotNull(result);
            Assert.NotNull(result.Page);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.WaitForResponseAsync<FakePageObject>(response => response.Url == "https://missing.com", new PageWaitForResponseOptions { Timeout = 1 }));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task RunAndWaitForResponseAsync_returns_proxy_of_type()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var result = await Page.RunAndWaitForResponseAsync<FakePageObject>(async () => await Page.ClickAsync("a span[data-content='Actions']"), response => response.Url == "https://api.github.com/_private/browser/stats" && response.Status == 200);
            Assert.NotNull(result);
            Assert.NotNull(result.Page);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.RunAndWaitForResponseAsync<FakePageObject>(async () => await Task.Delay(1), response => response.Url == "https://missing.com", new PageRunAndWaitForResponseOptions { Timeout = 1 }));
        }

        // ElementObject

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task QuerySelectorAllAsync_returns_proxies_of_type()
        {
            var result = await Page.QuerySelectorAllAsync<FakeElementObject>("div");
            Assert.IsNotEmpty(result);
            Assert.That(result, Is.All.Matches<FakeElementObject>(x => x.Element != null));

            result = await Page.QuerySelectorAllAsync<FakeElementObject>(".missing");
            Assert.IsEmpty(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task QuerySelectorAsync_returns_proxy_of_type()
        {
            var result = await Page.QuerySelectorAsync<FakeElementObject>(".tweet");
            Assert.NotNull(result);
            Assert.NotNull(result.Element);

            result = await Page.QuerySelectorAsync<FakeElementObject>(".missing");
            Assert.Null(result);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task WaitForSelectorAsync_returns_proxy_of_type()
        {
            var result = await Page.WaitForSelectorAsync<FakeElementObject>(".tweet");
            Assert.NotNull(result);
            Assert.NotNull(result.Element);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.WaitForSelectorAsync<FakeElementObject>(".missing", new PageWaitForSelectorOptions { Timeout = 1 }));
        }
    }
}
