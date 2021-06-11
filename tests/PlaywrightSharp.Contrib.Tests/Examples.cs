using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

namespace PlaywrightSharp.Documentation
{
    public class Examples
    {
        [Fact]
        public async Task<IBrowser> Browser()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();

            // IBrowserType
            _ = new[] { playwright.Chromium, playwright.Firefox, playwright.Webkit };

            return browser;
        }

        [Fact]
        public async Task close_Browser()
        {
            var browser = await Browser();

            await browser.CloseAsync();
        }

        [Fact]
        public async Task using_Browser()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

            // ...
        }

        [Fact]
        public async Task<IPage> Page()
        {
            var browser = await Browser();
            var page = await browser.NewPageAsync();

            return page;
        }

        [Fact]
        public async Task close_Page()
        {
            var page = await Page();

            await page.CloseAsync();
        }

        [Fact]
        public async Task navigation()
        {
            var page = await Page();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await page.GoBackAsync();
            await page.GoForwardAsync();
            await page.ReloadAsync();
        }

        [Fact]
        public async Task timeout()
        {
            var page = await Page();

            var timeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds; // default value
            page.SetDefaultNavigationTimeout(timeout);
            page.SetDefaultTimeout(timeout);

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet", new PageGotoOptions { Timeout = timeout });
            await page.GoBackAsync(new PageGoBackOptions { Timeout = timeout });
            await page.GoForwardAsync(new PageGoForwardOptions { Timeout = timeout });
            await page.ReloadAsync(new PageReloadOptions { Timeout = timeout });

        }

        [Fact]
        public async Task wait()
        {
            var page = await Page();
            var timeout = (int)TimeSpan.FromSeconds(3).TotalMilliseconds;

            var requestTask = page.WaitForRequestAsync("https://github.com/microsoft/playwright-dotnet", new PageWaitForRequestOptions { Timeout = timeout });
            var responseTask = page.WaitForResponseAsync("https://github.com/microsoft/playwright-dotnet", new PageWaitForResponseOptions { Timeout = timeout });
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Task.WhenAll(requestTask, responseTask);

            var eventTask = page.WaitForResponseAsync("https://github.com/microsoft/playwright-dotnet");
            var loadStateTask = page.WaitForLoadStateAsync(options: new PageWaitForLoadStateOptions { Timeout = timeout });
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Task.WhenAll(eventTask, loadStateTask);

            await page.ClickAsync("h1 > strong > a");
            await page.WaitForNavigationAsync(new PageWaitForNavigationOptions { Timeout = timeout });

            await page.WaitForFunctionAsync("() => window.location.href === 'https://github.com/microsoft/playwright-dotnet'", timeout);
            await page.WaitForSelectorAsync("#readme", new PageWaitForSelectorOptions { Timeout = timeout });
            await page.WaitForTimeoutAsync(timeout);

            // LoadState
            _ = new[] { LoadState.Load, LoadState.DOMContentLoaded, LoadState.NetworkIdle };

            // WaitForSelectorState
            _ = new[] { WaitForSelectorState.Attached, WaitForSelectorState.Detached, WaitForSelectorState.Visible, WaitForSelectorState.Hidden };
        }

        [Fact]
        public async Task values_from_Page()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            var url = page.Url;
            var title = await page.TitleAsync();
            var content = await page.ContentAsync();
            var textContent = await page.TextContentAsync("#readme h1:nth-child(1)");
            var innerText = await page.InnerTextAsync("#readme h1:nth-child(1)");
            var innerHtml = await page.InnerHTMLAsync("#readme h1:nth-child(1)");
            var attribute = await page.GetAttributeAsync("#readme h1:nth-child(1) a", "href");
        }

        [Fact]
        public async Task form()
        {
            var page = await Page();
            await page.GotoAsync("https://www.techlistic.com/p/selenium-practice-form.html");

            await page.SetViewportSizeAsync(1024, 1024); // fix

            // input / text
            await page.TypeAsync("input[name='firstname']", "Playwright");

            // input / radio
            await page.ClickAsync("#exp-6");

            // input / checkbox
            await page.CheckAsync("#profession-1");

            // select / option
            await page.SelectOptionAsync("#continents", "Europe");

            // input / file
            var file = await page.QuerySelectorAsync("#photo");
            await file.SetInputFilesAsync(@"c:\temp\test.png");

            // button
            await page.ClickAsync("#submit");
        }

        [Fact]
        public async Task query()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            var element = await page.QuerySelectorAsync("div#readme");
            var elements = await page.QuerySelectorAllAsync("div");
            Assert.NotNull(element);
            Assert.NotEmpty(elements);

            var missingElement = await page.QuerySelectorAsync("div#missing");
            var missingElements = await page.QuerySelectorAllAsync("div.missing");
            Assert.Null(missingElement);
            Assert.Empty(missingElements);

            var elementInElement = await element.QuerySelectorAsync("h1");
            var elementsInElement = await element.QuerySelectorAllAsync("h1");
            Assert.NotNull(elementInElement);
            Assert.NotEmpty(elementsInElement);
        }

        [Fact]
        public async Task evaluate()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var element = await page.QuerySelectorAsync("h1 > strong > a");

            var outerHtml = await element.EvaluateAsync<string>("e => e.outerHTML");
            var innerText = await element.EvaluateAsync<string>("e => e.innerText");
            var url = await element.EvaluateAsync<string>("e => e.getAttribute('href')");
            var hasContent = await element.EvaluateAsync<bool>("(e, value) => e.textContent.includes(value)", "playwright-dotnet");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.Equal("playwright-dotnet", innerText);
            Assert.Equal("/microsoft/playwright-dotnet", url);
            Assert.True(hasContent);

            outerHtml = await page.EvalOnSelectorAsync<string>("h1 > strong > a", "e => e.outerHTML");
            innerText = await page.EvalOnSelectorAsync<string>("h1 > strong > a", "e => e.innerText");
            url = await page.EvalOnSelectorAsync<string>("h1 > strong > a", "e => e.getAttribute('href')");
            hasContent = await page.EvalOnSelectorAsync<bool>("h1 > strong > a", "(e, value) => e.textContent.includes(value)", "playwright-dotnet");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.Equal("playwright-dotnet", innerText);
            Assert.Equal("/microsoft/playwright-dotnet", url);
            Assert.True(hasContent);

            outerHtml = await page.EvaluateAsync<string>("e => e.outerHTML", element);
            innerText = await page.EvaluateAsync<string>("e => e.innerText", element);
            url = await page.EvaluateAsync<string>("e => e.getAttribute('href')", element);
            hasContent = await page.EvaluateAsync<bool>($"e => e.textContent.includes({"'playwright-dotnet'"})", element);
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.Equal("playwright-dotnet", innerText);
            Assert.Equal("/microsoft/playwright-dotnet", url);
            Assert.True(hasContent);

            outerHtml = await (await element.GetPropertyAsync("outerHTML")).JsonValueAsync<string>();
            innerText = await (await element.GetPropertyAsync("innerText")).JsonValueAsync<string>();
            url = await (await element.GetPropertyAsync("href")).JsonValueAsync<string>();
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.Equal("playwright-dotnet", innerText);
            Assert.Equal("https://github.com/microsoft/playwright-dotnet", url);
        }

        [Fact]
        public async Task is_()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Assert.ThrowsAsync<PlaywrightException>(() => page.IsCheckedAsync("input[name='q']")); // Not a checkbox or radio button
            Assert.False(await page.IsDisabledAsync("input[name='q']"));
            Assert.True(await page.IsEditableAsync("input[name='q']"));
            Assert.True(await page.IsEnabledAsync("input[name='q']"));
            Assert.False(await page.IsHiddenAsync("input[name='q']"));
            Assert.True(await page.IsVisibleAsync("input[name='q']"));

            var element = await page.QuerySelectorAsync("input[name='q']");
            await Assert.ThrowsAsync<PlaywrightException>(() => element.IsCheckedAsync()); // Not a checkbox or radio button
            Assert.False(await element.IsDisabledAsync());
            Assert.True(await element.IsEditableAsync());
            Assert.True(await element.IsEnabledAsync());
            Assert.False(await element.IsHiddenAsync());
            Assert.True(await element.IsVisibleAsync());
        }
    }
}