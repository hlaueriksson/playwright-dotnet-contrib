using System;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightSharp.Documentation
{
    public class Examples
    {
        [Fact]
        public async Task<IBrowser> Browser()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(headless: true);

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
            await using var browser = await playwright.Chromium.LaunchAsync(headless: true);

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

            await page.GoToAsync("https://github.com/microsoft/playwright-sharp");
            await page.GoBackAsync();
            await page.GoForwardAsync();
            await page.ReloadAsync();
        }

        [Fact]
        public async Task timeout()
        {
            var page = await Page();

            var timeout = (int) TimeSpan.FromSeconds(30).TotalMilliseconds; // default value
            page.DefaultNavigationTimeout = timeout;
            page.DefaultTimeout = timeout;

            await page.GoToAsync("https://github.com/microsoft/playwright-sharp", timeout: timeout);
            await page.GoBackAsync(timeout);
            await page.GoForwardAsync(timeout);
            await page.ReloadAsync(timeout);
        }

        [Fact]
        public async Task wait()
        {
            var page = await Page();
            var timeout = (int) TimeSpan.FromSeconds(3).TotalMilliseconds;

            var requestTask = page.WaitForRequestAsync("https://github.com/microsoft/playwright-sharp", timeout);
            var responseTask = page.WaitForResponseAsync("https://github.com/microsoft/playwright-sharp", timeout);
            await page.GoToAsync("https://github.com/microsoft/playwright-sharp");
            await Task.WhenAll(requestTask, responseTask);

            var eventTask = page.WaitForEventAsync(PageEvent.Response, e => e.Response.Url == "https://github.com/microsoft/playwright-sharp");
            var loadStateTask = page.WaitForLoadStateAsync(timeout: timeout);
            await page.GoToAsync("https://github.com/microsoft/playwright-sharp");
            await Task.WhenAll(eventTask, loadStateTask);

            await page.ClickAsync("h1 > strong > a");
            await page.WaitForNavigationAsync(timeout: timeout);

            await page.WaitForFunctionAsync("() => window.location.href === 'https://github.com/microsoft/playwright-sharp'", timeout);
            await page.WaitForSelectorAsync("#readme", timeout: timeout);
            await page.WaitForTimeoutAsync(timeout);

            // LifecycleEvent
            _ = new[] { LifecycleEvent.Load, LifecycleEvent.DOMContentLoaded, LifecycleEvent.Networkidle };

            // WaitForState
            _ = new[] { WaitForState.Attached, WaitForState.Detached, WaitForState.Visible, WaitForState.Hidden };
        }

        [Fact]
        public async Task values_from_Page()
        {
            var page = await Page();

            var url = page.Url;
            var title = await page.GetTitleAsync();
            var content = await page.GetContentAsync();
        }

        [Fact]
        public async Task form()
        {
            var page = await Page();
            await page.GoToAsync("https://www.techlistic.com/p/selenium-practice-form.html");

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
            await page.GoToAsync("https://github.com/microsoft/playwright-sharp");

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
            await page.GoToAsync("https://github.com/microsoft/playwright-sharp");
            var element = await page.QuerySelectorAsync("h1 > strong > a");

            var outerHtml = await element.EvaluateAsync<string>("e => e.outerHTML");
            var innerText = await element.EvaluateAsync<string>("e => e.innerText");
            var url = await element.EvaluateAsync<string>("e => e.getAttribute('href')");
            var hasContent = await element.EvaluateAsync<bool>("(e, value) => e.textContent.includes(value)", "playwright-sharp");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" class=\"\" href=\"/microsoft/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("/microsoft/playwright-sharp", url);
            Assert.True(hasContent);

            outerHtml = await page.EvalOnSelectorAsync<string>("h1 > strong > a", "e => e.outerHTML");
            innerText = await page.EvalOnSelectorAsync<string>("h1 > strong > a", "e => e.innerText");
            url = await page.EvalOnSelectorAsync<string>("h1 > strong > a", "e => e.getAttribute('href')");
            hasContent = await page.EvalOnSelectorAsync<bool>("h1 > strong > a", "(e, value) => e.textContent.includes(value)", "playwright-sharp");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" class=\"\" href=\"/microsoft/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("/microsoft/playwright-sharp", url);
            Assert.True(hasContent);

            outerHtml = await page.EvaluateAsync<string>("e => e.outerHTML", element);
            innerText = await page.EvaluateAsync<string>("e => e.innerText", element);
            url = await page.EvaluateAsync<string>("e => e.getAttribute('href')", element);
            hasContent = await page.EvaluateAsync<bool>($"e => e.textContent.includes({"'playwright-sharp'"})", element);
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" class=\"\" href=\"/microsoft/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("/microsoft/playwright-sharp", url);
            Assert.True(hasContent);

            outerHtml = await (await element.GetPropertyAsync("outerHTML")).GetJsonValueAsync<string>();
            innerText = await (await element.GetPropertyAsync("innerText")).GetJsonValueAsync<string>();
            url = await (await element.GetPropertyAsync("href")).GetJsonValueAsync<string>();
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" class=\"\" href=\"/microsoft/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("https://github.com/microsoft/playwright-sharp", url);
        }
    }
}