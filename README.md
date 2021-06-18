# Playwright Contributions<!-- omit in toc -->

Contributions to Playwright for .NET 🎭🧪

Playwright Contributions offers extensions to Playwright for .NET

It provides a convenient way to write readable and reliable browser tests in C#

> [Playwright for .NET](https://github.com/microsoft/playwright-dotnet) is the official language port of Playwright.
>
> [Playwright](https://github.com/microsoft/playwright) is a Node.js library to automate Chromium, Firefox and WebKit with a single API. Playwright is built to enable cross-browser web automation that is ever-green, capable, reliable and fast.

## Content<!-- omit in toc -->

- [Introduction](#introduction)
- [Playwright.Contrib.Extensions](#playwrightcontribextensions)
- [Playwright.Contrib.FluentAssertions](#playwrightcontribfluentassertions)
- [Samples](#samples)
- [Attribution](#attribution)

## Introduction

`Microsoft.Playwright` is a great library to automate *Chromium*, *Firefox* and *WebKit* in .NET / C# on *Linux*, *macOS* and *Windows*.

Playwright Contributions consists of a few libraries that helps you write browser automation tests:

* `Playwright.Contrib.Extensions`
* `Playwright.Contrib.FluentAssertions`

These libraries contains _extension methods_ to the Playwright API and they are test framework agnostic.

## Playwright.Contrib.Extensions

`Playwright.Contrib.Extensions` is a library with extension methods for writing tests with the Playwright API.

### Extensions for `IPage`<!-- omit in toc -->

Attribute:

* `GetAttributeOrDefaultAsync`

Evaluation:

* `HasContentAsync`
* `HasTitleAsync`

Query:

* `QuerySelectorAllWithContentAsync`
* `QuerySelectorWithContentAsync`

### Extensions for `IElementHandle`<!-- omit in toc -->

Attribute:

* `ClassListAsync`
* `ClassNameAsync`
* `GetAttributeOrDefaultAsync`
* `HrefAsync`
* `IdAsync`
* `NameAsync`
* `SrcAsync`
* `ValueAsync`

Content:

* `OuterHTMLAsync`

Evaluation:

* `Exists`
* `HasAttributeAsync`
* `HasClassAsync`
* `HasContentAsync`
* `HasFocusAsync`
* `IsReadOnlyAsync`
* `IsRequiredAsync`
* `IsSelectedAsync`

Query:

* `QuerySelectorAllWithContentAsync`
* `QuerySelectorWithContentAsync`

## Playwright.Contrib.FluentAssertions

`Playwright.Contrib.FluentAssertions` is a library for writing tests with [`FluentAssertions`](https://github.com/fluentassertions/fluentassertions) against the Playwright API.

*Fluent Assertions* offers a very extensive set of extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style unit tests.

### Assertions for `IPage.Should()`<!-- omit in toc -->

Attribute:

* `HaveElementAttributeAsync`
* `HaveElementAttributeValueAsync`
* `NotHaveElementAttributeAsync`
* `NotHaveElementAttributeValueAsync`

Content:

* `HaveContentAsync`
* `HaveTitleAsync`
* `NotHaveContentAsync`
* `NotHaveTitleAsync`

Element:

* `HaveElement`
* `HaveElementCount`
* `HaveElementWithContent`
* `HaveElementWithContentCount`

State:

* `HaveCheckedElementAsync`
* `HaveDisabledElementAsync`
* `HaveEditableElementAsync`
* `HaveEnabledElementAsync`
* `HaveHiddenElementAsync`
* `HaveVisibleElementAsync`
* `NotHaveCheckedElementAsync`
* `NotHaveEditableElementAsync`

### Assertions for `IElementHandle.Should()`<!-- omit in toc -->

Attribute:

* `HaveAttributeAsync`
* `HaveAttributeValueAsync`
* `HaveClassAsync`
* `HaveValueAsync`
* `NotHaveAttributeAsync`
* `NotHaveAttributeValueAsync`
* `NotHaveClassAsync`
* `NotHaveValueAsync`

Content:

* `HaveContentAsync`
* `NotHaveContentAsync`

Element:

* `HaveElement`
* `HaveElementCount`
* `HaveElementWithContent`
* `HaveElementWithContentCount`

State:

* `BeCheckedAsync`
* `BeDisabledAsync`
* `BeEditableAsync`
* `BeEnabledAsync`
* `BeHiddenAsync`
* `BeReadOnlyAsync`
* `BeRequiredAsync`
* `BeSelectedAsync`
* `BeVisibleAsync`
* `Exist`
* `HaveFocusAsync`
* `NotBeCheckedAsync`
* `NotBeEditableAsync`
* `NotBeReadOnlyAsync`
* `NotBeRequiredAsync`
* `NotBeSelectedAsync`
* `NotExist`
* `NotHaveFocusAsync`

## Samples

Sample projects are located in the [`samples`](/samples/) folder.

Examples are written with these test frameworks:

- [ ] ~~MSTest~~
- [x] NUnit
- [ ] ~~SpecFlow~~
- [ ] ~~Xunit~~

This is an example with `NUnit` and `Playwright.Contrib.FluentAssertions`:

```csharp
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.Extensions;
using Microsoft.Playwright.Contrib.FluentAssertions;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Sample
{
    public class PlaywrightDotnetRepoTests
    {
        IBrowser Browser { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
        }

        [Test]
        public async Task Should_be_first_search_result_on_GitHub()
        {
            var page = await Browser.NewPageAsync();

            await page.GotoAsync("https://github.com/");
            var h1 = await page.QuerySelectorAsync("h1");
            await h1.Should().HaveContentAsync("Where the world builds software");

            var input = await page.QuerySelectorAsync("input.header-search-input");
            if (await input.IsHiddenAsync()) await page.ClickAsync(".octicon-three-bars");
            await page.TypeAsync("input.header-search-input", "playwright dotnet");
            await page.Keyboard.PressAsync("Enter");
            await page.WaitForNavigationAsync();

            var repositories = await page.QuerySelectorAllAsync(".repo-list-item");
            Assert.IsNotEmpty(repositories);
            var repository = repositories.First();
            await repository.Should().HaveContentAsync("microsoft/playwright-dotnet");
            var text = await repository.QuerySelectorAsync("p");
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.QuerySelectorAsync("a");
            await link.ClickAsync();

            h1 = await page.QuerySelectorAsync("article > h1");
            await h1.Should().HaveContentAsync("Playwright for .NET");
            Assert.AreEqual("https://github.com/microsoft/playwright-dotnet", page.Url);
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            var page = await Browser.NewPageAsync();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            await page.ClickAsync("a span[data-content='Actions']");
            await page.WaitForNavigationAsync();

            var status = await page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
            await status.Should().HaveAttributeValueAsync("title", "This workflow run completed successfully.");
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_TypeScript_version()
        {
            var page = await Browser.NewPageAsync();

            await page.GotoAsync("https://github.com/microsoft/playwright");
            var typescriptVersion = await GetLatestReleaseVersion();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await GetLatestReleaseVersion();

            Assert.AreEqual(typescriptVersion, dotnetVersion);

            async Task<string> GetLatestReleaseVersion()
            {
                var latest = await page.QuerySelectorWithContentAsync("a[href*='releases'] span", @"v\d\.\d+\.\d");
                return await latest.TextContentAsync();
            }
        }
    }
}
```

## Attribution

*Playwright Contributions* is standing on the shoulders of giants.

It would not exist without https://github.com/microsoft/playwright-dotnet and https://github.com/microsoft/playwright

Inspiration and experience has been drawn from the previous development of https://github.com/hlaueriksson/puppeteer-sharp-contrib
