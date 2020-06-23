using System;
using System.Threading.Tasks;
using PlaywrightSharp.Chromium;
using PlaywrightSharp.Firefox;
using Xunit;

namespace PlaywrightSharp.Documentation
{
    public class Examples
    {
        private readonly IBrowserType _browserType;

        public Examples()
        {
            _browserType = new ChromiumBrowserType();
            //_browserType = new FirefoxBrowserType();
        }

        [Fact]
        public async Task download_Browser()
        {
            await _browserType.CreateBrowserFetcher().DownloadAsync();
        }

        [Fact]
        public async Task<IBrowser> Browser()
        {
            var browser = await _browserType.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

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
            var options = new LaunchOptions { Headless = true };
            await using var browser = await _browserType.LaunchAsync(options);

            // ...
        }

        [Fact]
        public async Task<IPage> Page()
        {
            var browser = await Browser();
            var page = await browser.DefaultContext.NewPageAsync();

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

            await page.GoToAsync("https://github.com/hardkoded/playwright-sharp");
            await page.GoBackAsync();
            await page.GoForwardAsync();
            await page.ReloadAsync();
        }

        [Fact]
        public async Task timeout()
        {
            var page = await Page();

            var timeout = TimeSpan.FromSeconds(30).Milliseconds; // default value
            page.DefaultNavigationTimeout = timeout;
            page.DefaultTimeout = timeout;
            var options = new GoToOptions { Timeout = timeout };

            await page.GoToAsync("https://github.com/hardkoded/playwright-sharp", options);
            await page.GoBackAsync(options);
            await page.GoForwardAsync(options);
            await page.ReloadAsync(options);
        }

        [Fact]
        public async Task wait()
        {
            var page = await Page();
            var timeout = TimeSpan.FromSeconds(3).Milliseconds;

            var requestTask = page.WaitForRequestAsync("https://github.com/hardkoded/playwright-sharp", new WaitForOptions { Timeout = timeout });
            var responseTask = page.WaitForResponseAsync("https://github.com/hardkoded/playwright-sharp", new WaitForOptions { Timeout = timeout });
            await page.GoToAsync("https://github.com/hardkoded/playwright-sharp");
            await Task.WhenAll(requestTask, responseTask);

            var eventTask = page.WaitForEvent(PageEvent.Response, new WaitForEventOptions<ResponseEventArgs> { Predicate = e => e.Response.Url == "https://github.com/hardkoded/playwright-sharp" });
            var loadStateTask = page.WaitForLoadStateAsync(new NavigationOptions { Timeout = timeout });
            await page.GoToAsync("https://github.com/hardkoded/playwright-sharp");
            await Task.WhenAll(eventTask, loadStateTask);

            await page.ClickAsync("h1 > strong > a");
            await page.WaitForNavigationAsync(new WaitForNavigationOptions { Timeout = timeout });

            await page.WaitForFunctionAsync("() => window.location.href === 'https://github.com/hardkoded/playwright-sharp'", new WaitForFunctionOptions { Timeout = timeout });
            await page.WaitForSelectorAsync("#readme", new WaitForSelectorOptions { Timeout = timeout });
            await page.WaitForTimeoutAsync(timeout);

            // WaitUntilNavigation
            new NavigationOptions().WaitUntil = new[]
            {
                WaitUntilNavigation.Load,
                WaitUntilNavigation.DOMContentLoaded,
                WaitUntilNavigation.Networkidle0,
                WaitUntilNavigation.Networkidle2
            };
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

            // input / text
            await page.TypeAsync("input[name='firstname']", "Playwright");

            // input / radio
            await page.ClickAsync("#exp-6");

            // input / checkbox
            await page.ClickAsync("#profession-1");

            // select / option
            await page.SelectAsync("#continents", "Europe");

            // input / file
            var file = await page.QuerySelectorAsync("#photo");
            await file.SetInputFilesAsync(@"c:\temp\test.jpg");

            // button
            await page.ClickAsync("#submit");
        }

        [Fact]
        public async Task query()
        {
            var page = await Page();
            await page.GoToAsync("https://github.com/hardkoded/playwright-sharp");

            var element = await page.QuerySelectorAsync("h1 > strong > a");
            var elements = await page.QuerySelectorAllAsync("a");
            Assert.NotNull(element);
            Assert.NotEmpty(elements);

            var missingElement = await page.QuerySelectorAsync("a#missing-link");
            var missingElements = await page.QuerySelectorAllAsync("a.missing-link");
            Assert.Null(missingElement);
            Assert.Empty(missingElements);

            var outerHtml = await element.EvaluateAsync<string>("e => e.outerHTML");
            var innerText = await element.EvaluateAsync<string>("e => e.innerText");
            var url = await element.EvaluateAsync<string>("e => e.getAttribute('href')");
            var hasContent = await element.EvaluateAsync<bool>("(e, value) => e.textContent.includes(value)", "playwright-sharp");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/hardkoded/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("/hardkoded/playwright-sharp", url);
            Assert.True(hasContent);

            outerHtml = await page.QuerySelectorEvaluateAsync<string>("h1 > strong > a", "e => e.outerHTML");
            innerText = await page.QuerySelectorEvaluateAsync<string>("h1 > strong > a", "e => e.innerText");
            url = await page.QuerySelectorEvaluateAsync<string>("h1 > strong > a", "e => e.getAttribute('href')");
            hasContent = await page.QuerySelectorEvaluateAsync<bool>("h1 > strong > a", "(e, value) => e.textContent.includes(value)", "playwright-sharp");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/hardkoded/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("/hardkoded/playwright-sharp", url);
            Assert.True(hasContent);

            outerHtml = await page.EvaluateAsync<string>("e => e.outerHTML", element);
            innerText = await page.EvaluateAsync<string>("e => e.innerText", element);
            url = await page.QuerySelectorEvaluateAsync<string>("h1 > strong > a", "e => e.getAttribute('href')");
            hasContent = await page.QuerySelectorEvaluateAsync<bool>("h1 > strong > a", "(e, value) => e.textContent.includes(value)", "playwright-sharp");
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/hardkoded/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("/hardkoded/playwright-sharp", url);
            Assert.True(hasContent);

            outerHtml = await (await element.GetPropertyAsync("outerHTML")).GetJsonValueAsync<string>();
            innerText = await (await element.GetPropertyAsync("innerText")).GetJsonValueAsync<string>();
            url = await (await element.GetPropertyAsync("href")).GetJsonValueAsync<string>();
            Assert.Equal("<a data-pjax=\"#js-repo-pjax-container\" href=\"/hardkoded/playwright-sharp\">playwright-sharp</a>", outerHtml);
            Assert.Equal("playwright-sharp", innerText);
            Assert.Equal("https://github.com/hardkoded/playwright-sharp", url);
        }
    }
}