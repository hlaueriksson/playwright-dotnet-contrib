# PlaywrightContrib.PageObjects 🎭🧪

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

`PlaywrightContrib.PageObjects` is a library for writing browser tests using the _page object pattern_ with the Playwright API.

```cs
using FluentAssertions;
using Microsoft.Playwright;
using PlaywrightContrib.FluentAssertions;
using PlaywrightContrib.PageObjects;
using System.Threading.Tasks;

var playwright = await Playwright.CreateAsync();
var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();

var repoPage = await page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");
var link = await repoPage.Link;
await link.Should().HaveContentAsync("playwright-dotnet");
await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");

var actionsPage = await repoPage.GotoActionsAsync();
var latestStatus = await actionsPage.GetLatestWorkflowRunStatusAsync();
latestStatus.Should().Be("completed successfully");

public class GitHubRepoPage : PageObject
{
    [Selector("#repository-container-header strong a")]
    public virtual Task<IElementHandle> Link { get; }

    [Selector("#actions-tab")]
    public virtual Task<IElementHandle> Actions { get; }

    public async Task<GitHubActionsPage> GotoActionsAsync()
    {
        await (await Actions).ClickAsync();
        await Page.WaitForSelectorAsync("#partial-actions-workflow-runs");
        return Page.To<GitHubActionsPage>();
    }
}

public class GitHubActionsPage : PageObject
{
    public async Task<string> GetLatestWorkflowRunStatusAsync()
    {
        var status = await Page.QuerySelectorAsync(".checks-list-item-icon svg");
        return await status.GetAttributeAsync("aria-label");
    }
}
```

## Deprecation ⚠️

This package is legacy and is no longer maintained:

- Is is based on the `IElementHandle` interface and was first built with version `1.12.1` of `Microsoft.Playwright`
- The use of [`ElementHandle`](https://playwright.dev/dotnet/docs/api/class-elementhandle) is discouraged, use [`Locator`](https://playwright.dev/dotnet/docs/api/class-locator) objects and web-first assertions instead
- The Locator API was introduced in version [`1.14`](https://playwright.dev/dotnet/docs/release-notes#version-114) of `Microsoft.Playwright`
- [Locator vs ElementHandle](https://playwright.dev/dotnet/docs/locators#locator-vs-elementhandle) describes the difference between the old and new way to access elements
- You can use the vanilla API to achieve the same thing without using this package:
  - [Page Object Models](https://playwright.dev/dotnet/docs/pom)

## Page Objects

A page object wraps an [`IPage`](https://playwright.dev/dotnet/docs/api/class-page) and should encapsulate the way tests interact with a web page.

Create page objects by inheriting `PageObject` and declare properties decorated with `[Selector]` attributes.

```csharp
public class GitHubStartPage : PageObject
{
    [Selector("main h1")]
    public virtual Task<IElementHandle> Heading { get; }

    [Selector("header")]
    public virtual Task<GitHubHeader> Header { get; }

    public async Task<GitHubSearchPage> SearchAsync(string text)
    {
        await (await Header).SearchAsync(text);
        await Page.WaitForSelectorAsync("[data-testid=\"results-list\"]");

        return Page.To<GitHubSearchPage>();
    }
}
```

## Element Objects

An element object wraps an [`IElementHandle`](https://playwright.dev/dotnet/docs/api/class-elementhandle) and should encapsulate the way tests interact with an element of a web page.

Create element objects by inheriting `ElementObject` and declare properties decorated with `[Selector]` attributes.

```csharp
public class GitHubHeader : ElementObject
{
    [Selector("#query-builder-test")]
    public virtual Task<IElementHandle> SearchInput { get; }

    [Selector("[data-target=\"qbsearch-input.inputButtonText\"]")]
    public virtual Task<IElementHandle> SearchButton { get; }

    public async Task SearchAsync(string text)
    {
        var input = await SearchInput;
        if (await input.IsHiddenAsync())
        {
            await (await SearchButton).ClickAsync();
        }
        await input.TypeAsync(text);
        await input.PressAsync("Enter");
    }
}
```

## Selector Attributes

`[Selector]` attributes can be applied to properties on a `PageObject` or `ElementObject`.

Properties decorated with a `[Selector]` attribute must be a:

- public
- virtual
- asynchronous
- getter

that returns one of:

- `Task<IElementHandle>`
- `Task<IReadOnlyList<IElementHandle>>`
- `Task<ElementObject>`
- `Task<IReadOnlyList<ElementObject>>`

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

## Extensions for `IPage` 📄

Where `T` is a `PageObject`:

- `GoToAsync<T>`
- `RunAndWaitForNavigationAsync<T>`
- `RunAndWaitForResponseAsync<T>`
- `To<T>`
- `WaitForNavigationAsync<T>`
- `WaitForResponseAsync<T>`

Where `T` is an `ElementObject`:

- `QuerySelectorAllAsync<T>`
- `QuerySelectorAsync<T>`
- `WaitForSelectorAsync<T>`

## Extensions for `IElementHandle` 📑

Where `T` is an `ElementObject`:

- `To<T>`
- `QuerySelectorAllAsync<T>`
- `QuerySelectorAsync<T>`
- `WaitForSelectorAsync<T>`

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/playwright-dotnet-contrib](https://github.com/hlaueriksson/playwright-dotnet-contrib)
