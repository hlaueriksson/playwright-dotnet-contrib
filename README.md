# Playwright Contributions<!-- omit in toc -->

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://www.codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

[![PlaywrightContrib.Extensions](https://img.shields.io/nuget/v/PlaywrightContrib.Extensions.svg?label=PlaywrightContrib.Extensions)](https://www.nuget.org/packages/PlaywrightContrib.Extensions)
[![PlaywrightContrib.FluentAssertions](https://img.shields.io/nuget/v/PlaywrightContrib.FluentAssertions.svg?label=PlaywrightContrib.FluentAssertions)](https://www.nuget.org/packages/PlaywrightContrib.FluentAssertions)
[![PlaywrightContrib.PageObjects](https://img.shields.io/nuget/v/PlaywrightContrib.PageObjects.svg?label=PlaywrightContrib.PageObjects)](https://www.nuget.org/packages/PlaywrightContrib.PageObjects)

Contributions to Playwright for .NET 🎭🧪

Playwright Contributions offers extensions to Playwright for .NET

It provides a convenient way to write readable and reliable browser tests in C#

> [Playwright for .NET](https://github.com/microsoft/playwright-dotnet) is the official language port of Playwright.
>
> [Playwright](https://github.com/microsoft/playwright) is a Node.js library to automate Chromium, Firefox and WebKit with a single API. Playwright is built to enable cross-browser web automation that is ever-green, capable, reliable and fast.

## Content<!-- omit in toc -->

- [Introduction](#introduction)
- [PlaywrightContrib.Extensions](#playwrightcontribextensions)
- [PlaywrightContrib.FluentAssertions](#playwrightcontribfluentassertions)
- [PlaywrightContrib.PageObjects](#playwrightcontribpageobjects)
- [Samples](#samples)
- [Attribution](#attribution)

## Introduction

`Microsoft.Playwright` is a great library to automate *Chromium*, *Firefox* and *WebKit* in .NET / C# on *Linux*, *macOS* and *Windows*.

Playwright Contributions consists of a few libraries that helps you write browser automation tests:

* `PlaywrightContrib.Extensions`
* `PlaywrightContrib.FluentAssertions`
* `PlaywrightContrib.PageObjects`

These libraries contains _extension methods_ to the Playwright API and they are test framework agnostic.

## PlaywrightContrib.Extensions

`PlaywrightContrib.Extensions` is a library with extension methods for writing tests with the Playwright API.

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
* `HasAttributeValueAsync`
* `HasClassAsync`
* `HasContentAsync`
* `HasFocusAsync`
* `HasValueAsync`
* `IsReadOnlyAsync`
* `IsRequiredAsync`
* `IsSelectedAsync`

Query:

* `QuerySelectorAllWithContentAsync`
* `QuerySelectorWithContentAsync`

## PlaywrightContrib.FluentAssertions

`PlaywrightContrib.FluentAssertions` is a library for writing tests with [`FluentAssertions`](https://github.com/fluentassertions/fluentassertions) against the Playwright API.

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

* `HaveElementAsync`
* `HaveElementCountAsync`
* `HaveElementWithContentAsync`
* `HaveElementWithContentCountAsync`

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

* `HaveElementAsync`
* `HaveElementCountAsync`
* `HaveElementWithContentAsync`
* `HaveElementWithContentCountAsync`

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

## PlaywrightContrib.PageObjects

`PlaywrightContrib.PageObjects` is a library for writing browser tests using the _page object pattern_ with the Playwright API.

### Page Objects<!-- omit in toc -->

A page object wraps an [`IPage`](https://playwright.dev/dotnet/docs/api/class-page) and should encapsulate the way tests interact with a web page.

Create page objects by inheriting `PageObject` and declare properties decorated with `[Selector]` attributes.

```csharp
public class GitHubStartPage : PageObject
{
    [Selector("h1")]
    public virtual Task<IElementHandle> Heading { get; }

    [Selector("header")]
    public virtual Task<GitHubHeader> Header { get; }

    public async Task<GitHubSearchPage> SearchAsync(string text)
    {
        await (await Header).SearchAsync(text);
        return Page.To<GitHubSearchPage>();
    }
}
```

### Element Objects<!-- omit in toc -->

An element object wraps an [`IElementHandle`](https://playwright.dev/dotnet/docs/api/class-elementhandle) and should encapsulate the way tests interact with an element of a web page.

Create element objects by inheriting `ElementObject` and declare properties decorated with `[Selector]` attributes.

```csharp
public class GitHubHeader : ElementObject
{
    [Selector("input.header-search-input")]
    public virtual Task<IElementHandle> SearchInput { get; }

    [Selector(".octicon-three-bars")]
    public virtual Task<IElementHandle> ThreeBars { get; }

    public async Task SearchAsync(string text)
    {
        var input = await SearchInput;
        if (await input.IsHiddenAsync()) await (await ThreeBars).ClickAsync();
        await input.TypeAsync(text);
        await input.PressAsync("Enter");
    }
}
```

### Selector Attributes<!-- omit in toc -->

`[Selector]` attributes can be applied to properties on a `PageObject` or `ElementObject`.

Properties decorated with a `[Selector]` attribute must be a:

* public
* virtual
* asynchronous
* getter

that returns one of:

* `Task<IElementHandle>`
* `Task<IReadOnlyList<IElementHandle>>`
* `Task<ElementObject>`
* `Task<IReadOnlyList<ElementObject>>`

Example:

```csharp
[Selector("#foo")]
public virtual Task<IElementHandle> SelectorForElementHandle { get; }

[Selector(".bar")]
public virtual Task<IReadOnlyList<IElementHandle>> SelectorForElementHandleList { get; }

[Selector("#foo")]
public virtual Task<FooElementObject> SelectorForElementObject { get; }

[Selector(".bar")]
public virtual Task<IReadOnlyList<BarElementObject>> SelectorForElementObjectList { get; }
```

### Extensions for `IPage`<!-- omit in toc -->

Where `T` is a `PageObject`:

* `GoToAsync<T>`
* `RunAndWaitForNavigationAsync<T>`
* `RunAndWaitForResponseAsync<T>`
* `To<T>`
* `WaitForNavigationAsync<T>`
* `WaitForResponseAsync<T>`

Where `T` is an `ElementObject`:

* `QuerySelectorAllAsync<T>`
* `QuerySelectorAsync<T>`
* `WaitForSelectorAsync<T>`

### Extensions for `IElementHandle`<!-- omit in toc -->

Where `T` is an `ElementObject`:

* `To<T>`
* `QuerySelectorAllAsync<T>`
* `QuerySelectorAsync<T>`
* `WaitForSelectorAsync<T>`

## Samples

Sample projects are located in the [`samples`](/samples/) folder.

Examples are written with these test frameworks:

- [ ] ~~MSTest~~
- [x] NUnit
- [ ] ~~SpecFlow~~
- [ ] ~~Xunit~~

This is an example with `NUnit` and `PlaywrightContrib.FluentAssertions`:

```csharp
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightContrib.Extensions;
using PlaywrightContrib.FluentAssertions;

namespace PlaywrightContrib.Sample.NUnit
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
            repositories.Should().NotBeEmpty();
            var repository = repositories.First();
            await repository.Should().HaveContentAsync("microsoft/playwright-dotnet");
            var text = await repository.QuerySelectorAsync("p");
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.QuerySelectorAsync("a");
            await link.ClickAsync();

            h1 = await page.QuerySelectorAsync("article > h1");
            await h1.Should().HaveContentAsync("Playwright for .NET");
            page.Url.Should().Be("https://github.com/microsoft/playwright-dotnet");
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

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await GetLatestReleaseVersion();

            await page.GotoAsync("https://github.com/microsoft/playwright");
            var typescriptVersion = await GetLatestReleaseVersion();

            dotnetVersion.Should().BeEquivalentTo(typescriptVersion);

            async Task<string> GetLatestReleaseVersion()
            {
                var latest = await page.QuerySelectorWithContentAsync("a[href*='releases'] span", @"v\d\.\d+\.\d");
                return await latest.TextContentAsync();
            }
        }
    }
}
```

This is an example with `NUnit` and `PlaywrightContrib.PageObjects`:

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightContrib.Extensions;
using PlaywrightContrib.PageObjects;

namespace PlaywrightContrib.Sample.NUnit
{
    public class GitHubStartPage : PageObject
    {
        [Selector("h1")]
        public virtual Task<IElementHandle> Heading { get; }

        [Selector("header")]
        public virtual Task<GitHubHeader> Header { get; }

        public async Task<GitHubSearchPage> SearchAsync(string text)
        {
            await (await Header).SearchAsync(text);
            return Page.To<GitHubSearchPage>();
        }
    }

    public class GitHubHeader : ElementObject
    {
        [Selector("input.header-search-input")]
        public virtual Task<IElementHandle> SearchInput { get; }

        [Selector(".octicon-three-bars")]
        public virtual Task<IElementHandle> ThreeBars { get; }

        public async Task SearchAsync(string text)
        {
            var input = await SearchInput;
            if (await input.IsHiddenAsync()) await (await ThreeBars).ClickAsync();
            await input.TypeAsync(text);
            await input.PressAsync("Enter");
        }
    }

    public class GitHubSearchPage : PageObject
    {
        [Selector(".repo-list-item")]
        public virtual Task<IReadOnlyList<GitHubRepoListItem>> RepoListItems { get; }

        public async Task<GitHubRepoPage> GotoAsync(GitHubRepoListItem repo)
        {
            return await Page.RunAndWaitForNavigationAsync<GitHubRepoPage>(async () =>
            {
                var link = await repo.Link;
                await link.ClickAsync();
            });
        }
    }

    public class GitHubRepoListItem : ElementObject
    {
        [Selector("a")]
        public virtual Task<IElementHandle> Link { get; }

        [Selector("p")]
        public virtual Task<IElementHandle> Text { get; }
    }

    public class GitHubRepoPage : PageObject
    {
        [Selector("article > h1")]
        public virtual Task<IElementHandle> Heading { get; }

        [Selector("a span[data-content='Actions']")]
        public virtual Task<IElementHandle> Actions { get; }

        public async Task<GitHubActionsPage> GotoActionsAsync()
        {
            await (await Actions).ClickAsync();
            return await Page.WaitForNavigationAsync<GitHubActionsPage>();
        }

        public async Task<string> GetLatestReleaseVersionAsync()
        {
            var latest = await Page.QuerySelectorWithContentAsync("a[href*='releases'] span", @"v\d\.\d+\.\d");
            return await latest.TextContentAsync();
        }
    }

    public class GitHubActionsPage : PageObject
    {
        public async Task<string> GetLatestWorkflowRunStatusAsync()
        {
            var status = await Page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
            return await status.GetAttributeAsync("title");
        }
    }
}
```

```csharp
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightContrib.FluentAssertions;
using PlaywrightContrib.PageObjects;

namespace PlaywrightContrib.Sample.NUnit
{
    public class PlaywrightDotnetRepoPageObjectTests
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
            var startPage = await page.GotoAsync<GitHubStartPage>("https://github.com/");
            var heading = await startPage.Heading;
            await heading.Should().HaveContentAsync("Where the world builds software");

            var searchPage = await startPage.SearchAsync("playwright dotnet");
            var repositories = await searchPage.RepoListItems;
            repositories.Should().NotBeEmpty();
            var repository = repositories.First();
            var text = await repository.Text;
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.Link;
            await link.Should().HaveContentAsync("microsoft/playwright-dotnet");

            var repoPage = await searchPage.GotoAsync(repository);
            heading = await repoPage.Heading;
            await heading.Should().HaveContentAsync("Playwright for .NET");
            repoPage.Page.Url.Should().Be("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            var page = await Browser.NewPageAsync();
            var repoPage = await page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");

            var actionsPage = await repoPage.GotoActionsAsync();
            var status = await actionsPage.GetLatestWorkflowRunStatusAsync();
            status.Should().Be("This workflow run completed successfully.");
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_TypeScript_version()
        {
            var page = await Browser.NewPageAsync();

            var repoPage = await page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await repoPage.GetLatestReleaseVersionAsync();

            repoPage = await page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright");
            var typescriptVersion = await repoPage.GetLatestReleaseVersionAsync();

            dotnetVersion.Should().BeEquivalentTo(typescriptVersion);
        }
    }
}
```

## Attribution

*Playwright Contributions* is standing on the shoulders of giants.

It would not exist without https://github.com/microsoft/playwright-dotnet and https://github.com/microsoft/playwright

Inspiration and experience has been drawn from the previous development of https://github.com/hlaueriksson/puppeteer-sharp-contrib
