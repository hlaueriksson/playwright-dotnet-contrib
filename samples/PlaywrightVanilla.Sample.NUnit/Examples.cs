using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightVanilla.Sample.NUnit
{
    public class Examples
    {
        async Task<IBrowser> Browser()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();

            // IBrowserType
            _ = new[] { playwright.Chromium, playwright.Firefox, playwright.Webkit };

            return browser;
        }

        [Test]
        public async Task close_Browser()
        {
            var browser = await Browser();

            await browser.CloseAsync();
        }

        [Test]
        public async Task using_Browser()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

            // ...
        }

        async Task<IPage> Page()
        {
            var browser = await Browser();
            var page = await browser.NewPageAsync();

            return page;
        }

        [Test]
        public async Task close_Page()
        {
            var page = await Page();

            await page.CloseAsync();
        }

        [Test]
        public async Task navigation()
        {
            var page = await Page();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await page.GoBackAsync();
            await page.GoForwardAsync();
            await page.ReloadAsync();
        }

        [Test]
        public async Task timeout()
        {
            var page = await Page();

            var timeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds; // default value
            page.SetDefaultNavigationTimeout(timeout);
            page.SetDefaultTimeout(timeout);

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout });
        }

        [Test]
        public async Task wait()
        {
            var page = await Page();
            var timeout = (int)TimeSpan.FromSeconds(3).TotalMilliseconds;

            var requestTask = page.WaitForRequestAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout });
            var responseTask = page.WaitForResponseAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout });
            var loadStateTask = page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = timeout });
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Task.WhenAll(requestTask, responseTask, loadStateTask);

            await page.ClickAsync("a >> text=playwright-dotnet");
            await page.WaitForNavigationAsync(new() { WaitUntil = WaitUntilState.NetworkIdle, Timeout = timeout });
            await page.WaitForFunctionAsync("() => window.location.href === 'https://github.com/microsoft/playwright-dotnet'", timeout);
            await page.WaitForSelectorAsync("h1 >> text=Playwright for .NET", new() { State = WaitForSelectorState.Visible, Timeout = timeout });
            await page.WaitForURLAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout });

            await page.Locator("h1 >> text=Playwright for .NET").WaitForAsync(new() { Timeout = timeout });

            await page.RunAndWaitForNavigationAsync(async () =>
            {
                await page.ClickAsync("a >> text=playwright-dotnet");
            }, new() { Timeout = timeout });

            // LoadState
            _ = new[] { LoadState.Load, LoadState.DOMContentLoaded, LoadState.NetworkIdle };

            // WaitUntilState
            _ = new[] { WaitUntilState.Load, WaitUntilState.DOMContentLoaded, WaitUntilState.NetworkIdle, WaitUntilState.Commit };

            // WaitForSelectorState
            _ = new[] { WaitForSelectorState.Attached, WaitForSelectorState.Detached, WaitForSelectorState.Visible, WaitForSelectorState.Hidden };
        }

        [Test]
        public async Task values_from_Page()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            _ = page.Url;
            await page.TitleAsync();
            await page.ContentAsync();
            await page.TextContentAsync("h1 >> text=Playwright for .NET");
            await page.InnerTextAsync("h1 >> text=Playwright for .NET");
            await page.InnerHTMLAsync("h1 >> text=Playwright for .NET");
            await page.GetAttributeAsync("a >> text=playwright-dotnet", "href");
            await page.InputValueAsync("[data-test-selector=nav-search-input]");
        }

        [Test]
        public async Task form()
        {
            var page = await Page();
            await page.GotoAsync("https://www.techlistic.com/p/selenium-practice-form.html");

            // accept cookies
            await page.ClickAsync("button >> text=Continue with Recommended Cookies");

            // input / text
            await page.FillAsync("input[name='firstname']", "Playwright");

            // input / radio
            await page.ClickAsync("#exp-6");

            // input / checkbox
            await page.CheckAsync("#profession-1");

            // select / option
            await page.SelectOptionAsync("#continents", "Europe");

            // input / file
            await page.SetInputFilesAsync("#photo", @"..\..\..\..\..\icon.png");

            // button
            await page.ClickAsync("#submit");
        }

        [Test]
        public async Task evaluate()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            var outerHtml = await page.EvalOnSelectorAsync<string>("a >> text=playwright-dotnet", "e => e.outerHTML");
            var hasContent = await page.EvalOnSelectorAsync<bool>("a >> text=playwright-dotnet", "(e, value) => e.textContent.includes(value)", "playwright-dotnet");
            Assert.AreEqual("<a data-pjax=\"#repo-content-pjax-container\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.True(hasContent);

            var locator = page.Locator("a >> text=playwright-dotnet");

            outerHtml = await locator.EvaluateAsync<string>("e => e.outerHTML");
            hasContent = await locator.EvaluateAsync<bool>("(e, value) => e.textContent.includes(value)", "playwright-dotnet");
            Assert.AreEqual("<a data-pjax=\"#repo-content-pjax-container\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.True(hasContent);
        }

        [Test]
        public async Task is_()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            Assert.ThrowsAsync<PlaywrightException>(async () => await page.IsCheckedAsync("[data-test-selector=nav-search-input]")); // Not a checkbox or radio button
            Assert.False(await page.IsDisabledAsync("[data-test-selector=nav-search-input]"));
            Assert.True(await page.IsEditableAsync("[data-test-selector=nav-search-input]"));
            Assert.True(await page.IsEnabledAsync("[data-test-selector=nav-search-input]"));
            Assert.False(await page.IsHiddenAsync("[data-test-selector=nav-search-input]"));
            Assert.True(await page.IsVisibleAsync("[data-test-selector=nav-search-input]"));

            var locator = page.Locator("[data-test-selector=nav-search-input]");

            Assert.ThrowsAsync<PlaywrightException>(async () => await locator.IsCheckedAsync()); // Not a checkbox or radio button
            Assert.False(await locator.IsDisabledAsync());
            Assert.True(await locator.IsEditableAsync());
            Assert.True(await locator.IsEnabledAsync());
            Assert.False(await locator.IsHiddenAsync());
            Assert.True(await locator.IsVisibleAsync());
        }
    }
}
