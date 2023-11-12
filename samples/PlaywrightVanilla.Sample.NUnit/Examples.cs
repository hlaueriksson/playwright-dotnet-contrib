using System;
using System.Runtime.InteropServices;
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
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
            });

            // IBrowserType
            _ = new[] { playwright.Chromium, playwright.Firefox, playwright.Webkit };

            // OSPlatform
            _ = new[] { OSPlatform.FreeBSD, OSPlatform.Linux, OSPlatform.OSX, OSPlatform.Windows };

            // Architecture
            _ = new[] { Architecture.X86, Architecture.X64, Architecture.Arm, Architecture.Arm64, Architecture.Wasm, Architecture.S390x };

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
            await using var browser = await playwright.Chromium.LaunchAsync();
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

            var tasks = new Task[]
            {
                /*
                page.WaitForConsoleMessageAsync(new() { Timeout = timeout }),
                page.WaitForDownloadAsync(new() { Timeout = timeout }),
                page.WaitForFileChooserAsync(new() { Timeout = timeout }),
                */
                page.WaitForFunctionAsync("() => window.location.href === 'https://github.com/microsoft/playwright-dotnet'", null, new() { Timeout = timeout }),
                page.WaitForLoadStateAsync(LoadState.Load, new() { Timeout = timeout }),
                //page.WaitForNavigationAsync(), // Obsolete
                /*
                page.WaitForPopupAsync(new() { Timeout = timeout }),
                */
                page.WaitForRequestAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout }),
                page.WaitForRequestFinishedAsync(new() { Timeout = timeout }),
                page.WaitForResponseAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout }),
                //page.WaitForSelectorAsync(), // Using ILocator objects and web-first assertions makes the code wait-for-selector-free.
                page.WaitForTimeoutAsync(timeout),
                page.WaitForURLAsync("https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout }),
                /*
                page.WaitForWebSocketAsync(new() { Timeout = timeout }),
                page.WaitForWorkerAsync(new() { Timeout = timeout }),
                */
            };
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Task.WhenAll(tasks);

            tasks = new Task[]
            {
                /*
                page.RunAndWaitForConsoleMessageAsync(async () => await Task.Delay(1), new() { Timeout = timeout }),
                page.RunAndWaitForDownloadAsync(async() => await Task.Delay(1), new() { Timeout = timeout }),
                page.RunAndWaitForFileChooserAsync(async() => await Task.Delay(1), new() { Timeout = timeout }),
                */
                //page.RunAndWaitForNavigationAsync(), // Obsolete
                /*
                page.RunAndWaitForPopupAsync(async() => await Task.Delay(1), new() { Timeout = timeout }),
                */
                page.RunAndWaitForRequestAsync(async() => await Task.Delay(1), "https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout }),
                page.RunAndWaitForRequestFinishedAsync(async() => await Task.Delay(1), new() { Timeout = timeout }),
                page.RunAndWaitForResponseAsync(async() => await Task.Delay(1), "https://github.com/microsoft/playwright-dotnet", new() { Timeout = timeout }),
                /*
                page.RunAndWaitForWebSocketAsync(async() => await Task.Delay(1), new() { Timeout = timeout }),
                page.RunAndWaitForWorkerAsync(async() => await Task.Delay(1), new() { Timeout = timeout }),
                */
            };
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            await Task.WhenAll(tasks);

            await page.Locator("h1 >> text=Playwright for .NET").WaitForAsync(new() { Timeout = timeout });

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
            await page.ContentAsync();
            await page.GetAttributeAsync("a >> text=playwright-dotnet", "href");
            await page.InnerHTMLAsync("h1 >> text=Playwright for .NET");
            await page.InnerTextAsync("h1 >> text=Playwright for .NET");
            await page.InputValueAsync("#query-builder-test");
            await page.TextContentAsync("h1 >> text=Playwright for .NET");
            await page.TitleAsync();

            await page.PdfAsync();
            await page.ScreenshotAsync();
        }

        [Test]
        public async Task values_from_Locator()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var locator = page.Locator("h1 >> text=Playwright for .NET");

            await locator.AllInnerTextsAsync();
            await locator.AllTextContentsAsync();
            await locator.CountAsync();
            await locator.GetAttributeAsync("href");
            await locator.InnerHTMLAsync();
            await locator.InnerTextAsync();
            /*
            await locator.InputValueAsync();
            */
            await locator.TextContentAsync();
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
            await page.SelectOptionAsync("#continents", "Europe"); // Use ILocator.SelectOptionAsync instead.

            // input / file
            await page.SetInputFilesAsync("#photo", @"..\..\..\..\..\icon.png");

            // button
            await page.ClickAsync("#submit");

            // other
            await page.DblClickAsync("#main");
            /*
            await page.DragAndDropAsync("", "");
            */
            await page.FocusAsync("#submit");
            await page.HoverAsync("#main");
            await page.PressAsync("#main", "Control+C");
            await page.SetCheckedAsync("#profession-1", true);
            /*
            await page.TapAsync("#main");
            */
            //await page.TypeAsync(); // Obsolete
            await page.UncheckAsync("#profession-1");

            // Locator
            var locator = page.Locator("main");
            /*
            await locator.CheckAsync();
            await locator.ClearAsync();
            */
            await locator.ClickAsync();
            await locator.DblClickAsync();
            /*
            await locator.DragToAsync();
            await locator.FillAsync("");
            */
            await locator.FocusAsync();
            await locator.HoverAsync();
            await locator.PressAsync("Control");
            await locator.PressSequentiallyAsync("Control+C");
            /*
            await locator.SelectOptionAsync("");
            await locator.SetCheckedAsync(true);
            await locator.SetInputFilesAsync("");
            await locator.TapAsync();
            */
            //await locator.TypeAsync(); // Obsolete
            /*
            await locator.UncheckAsync();
            */
        }

        [Test]
        public async Task evaluate()
        {
            var page = await Page();
            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            var outerHtml = await page.EvalOnSelectorAsync<string>("a >> text=playwright-dotnet", "e => e.outerHTML");
            var hasContent = await page.EvalOnSelectorAsync<bool>("a >> text=playwright-dotnet", "(e, value) => e.textContent.includes(value)", "playwright-dotnet");
            Assert.AreEqual("<a data-pjax=\"#repo-content-pjax-container\" data-turbo-frame=\"repo-content-turbo-frame\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.True(hasContent);

            // other
            await page.EvalOnSelectorAllAsync("a >> text=playwright-dotnet", "e => e.outerHTML");
            await page.EvaluateAsync("document.body");
            await page.EvaluateHandleAsync("document.body");

            var locator = page.Locator("a >> text=playwright-dotnet");

            outerHtml = await locator.EvaluateAsync<string>("e => e.outerHTML");
            hasContent = await locator.EvaluateAsync<bool>("(e, value) => e.textContent.includes(value)", "playwright-dotnet");
            Assert.AreEqual("<a data-pjax=\"#repo-content-pjax-container\" data-turbo-frame=\"repo-content-turbo-frame\" href=\"/microsoft/playwright-dotnet\">playwright-dotnet</a>", outerHtml);
            Assert.True(hasContent);

            // other
            await locator.EvaluateAllAsync<string>("e => e.outerHTML");
            await locator.EvaluateHandleAsync("e => e.outerHTML");
        }

        [Test]
        public async Task is_()
        {
            var page = await Page();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            Assert.False(page.IsClosed);
            Assert.False(await page.IsCheckedAsync("#include_email"));
            Assert.False(await page.IsDisabledAsync("#query-builder-test"));
            Assert.True(await page.IsEditableAsync("#query-builder-test"));
            Assert.True(await page.IsEnabledAsync("#query-builder-test"));
            Assert.True(await page.IsHiddenAsync("#query-builder-test"));
            Assert.False(await page.IsVisibleAsync("#query-builder-test"));

            var locator = page.Locator("#include_email");
            Assert.False(await locator.IsCheckedAsync());

            locator = page.Locator("#query-builder-test");
            Assert.False(await locator.IsDisabledAsync());
            Assert.True(await locator.IsEditableAsync());
            Assert.True(await locator.IsEnabledAsync());
            Assert.True(await locator.IsHiddenAsync());
            Assert.False(await locator.IsVisibleAsync());
        }
    }
}
