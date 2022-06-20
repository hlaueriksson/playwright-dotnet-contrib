# PlaywrightContrib.Extensions 🎭🧪

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

`PlaywrightContrib.Extensions` is a library with extension methods for writing tests with the Playwright API.

```cs
using FluentAssertions;
using Microsoft.Playwright;
using PlaywrightContrib.Extensions;

var playwright = await Playwright.CreateAsync();
var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();

await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
var link = await page.QuerySelectorWithContentAsync("h1 a", "playwright-dotnet");
(await link.HrefAsync()).Should().Be("https://github.com/microsoft/playwright-dotnet");
(await page.HasContentAsync("Playwright for .NET is the official language port of Playwright")).Should().BeTrue();

await page.ClickAsync("#actions-tab");
await page.WaitForNavigationAsync();
var latestStatus = await page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
latestStatus.Exists().Should().BeTrue();
(await latestStatus.HasAttributeValueAsync("title", "This workflow run completed successfully.")).Should().BeTrue();
```

## Deprecation ⚠️

This package is legacy and is no longer maintained:

- Is is based on the `IElementHandle` interface and was first built with version `1.12.1` of `Microsoft.Playwright`
- The use of [`ElementHandle`](https://playwright.dev/dotnet/docs/api/class-elementhandle) is discouraged, use [`Locator`](https://playwright.dev/dotnet/docs/api/class-locator) objects and web-first assertions instead
- The Locator API was introduced in version [`1.14`](https://playwright.dev/dotnet/docs/release-notes#version-114) of `Microsoft.Playwright`
- [Locator vs ElementHandle](https://playwright.dev/dotnet/docs/locators#locator-vs-elementhandle) describes the difference between the old and new way to access elements
- You can use the vanilla API to achieve the same thing without using this package:
  - [Locator](https://playwright.dev/dotnet/docs/api/class-locator)

## Extensions for `IPage` 📄

Attribute:

- `GetAttributeOrDefaultAsync`

Evaluation:

- `HasContentAsync`
- `HasTitleAsync`

Query:

- `QuerySelectorAllWithContentAsync`
- `QuerySelectorWithContentAsync`

## Extensions for `IElementHandle` 📑

Attribute:

- `ClassListAsync`
- `ClassNameAsync`
- `GetAttributeOrDefaultAsync`
- `HrefAsync`
- `IdAsync`
- `NameAsync`
- `SrcAsync`
- `ValueAsync`

Content:

- `OuterHTMLAsync`

Evaluation:

- `Exists`
- `HasAttributeAsync`
- `HasAttributeValueAsync`
- `HasClassAsync`
- `HasContentAsync`
- `HasFocusAsync`
- `HasValueAsync`
- `IsReadOnlyAsync`
- `IsRequiredAsync`
- `IsSelectedAsync`

Query:

- `QuerySelectorAllWithContentAsync`
- `QuerySelectorWithContentAsync`

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/playwright-dotnet-contrib](https://github.com/hlaueriksson/playwright-dotnet-contrib)
