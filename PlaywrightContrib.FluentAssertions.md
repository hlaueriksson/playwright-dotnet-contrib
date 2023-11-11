# PlaywrightContrib.FluentAssertions 🎭🧪

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

`PlaywrightContrib.FluentAssertions` is a library for writing tests with [`FluentAssertions`](https://www.nuget.org/packages/FluentAssertions/) against the Playwright API.

```cs
using Microsoft.Playwright;
using PlaywrightContrib.FluentAssertions;

var playwright = await Playwright.CreateAsync();
var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();

await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
var link = await page.QuerySelectorAsync("#repository-container-header strong a");
await link.Should().HaveContentAsync("playwright-dotnet");
await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");
await page.Should().HaveContentAsync("Playwright for .NET is the official language port of Playwright");

await page.ClickAsync("#actions-tab");
await page.WaitForSelectorAsync("#partial-actions-workflow-runs");
var latestStatus = await page.QuerySelectorAsync(".checks-list-item-icon svg");
latestStatus.Should().Exist();
await latestStatus.Should().HaveAttributeValueAsync("aria-label", "completed successfully");
```

## Deprecation ⚠️

This package is legacy and is no longer maintained:

- Is is based on the `IElementHandle` interface and was first built with version `1.12.1` of `Microsoft.Playwright`
- The use of [`ElementHandle`](https://playwright.dev/dotnet/docs/api/class-elementhandle) is discouraged, use [`Locator`](https://playwright.dev/dotnet/docs/api/class-locator) objects and web-first assertions instead
- The Locator API was introduced in version [`1.14`](https://playwright.dev/dotnet/docs/release-notes#version-114) of `Microsoft.Playwright`
- [Locator vs ElementHandle](https://playwright.dev/dotnet/docs/locators#locator-vs-elementhandle) describes the difference between the old and new way to access elements
- You can use the vanilla API to achieve the same thing without using this package:
  - [Assertions](https://playwright.dev/dotnet/docs/test-assertions)

## Assertions for `IPage.Should()` 📄

Attribute:

- `HaveElementAttributeAsync`
- `HaveElementAttributeValueAsync`
- `NotHaveElementAttributeAsync`
- `NotHaveElementAttributeValueAsync`

Content:

- `HaveContentAsync`
- `HaveTitleAsync`
- `NotHaveContentAsync`
- `NotHaveTitleAsync`

Element:

- `HaveElementAsync`
- `HaveElementCountAsync`
- `HaveElementWithContentAsync`
- `HaveElementWithContentCountAsync`

State:

- `HaveCheckedElementAsync`
- `HaveDisabledElementAsync`
- `HaveEditableElementAsync`
- `HaveEnabledElementAsync`
- `HaveHiddenElementAsync`
- `HaveVisibleElementAsync`
- `NotHaveCheckedElementAsync`
- `NotHaveEditableElementAsync`

## Assertions for `IElementHandle.Should()` 📑

Attribute:

- `HaveAttributeAsync`
- `HaveAttributeValueAsync`
- `HaveClassAsync`
- `HaveValueAsync`
- `NotHaveAttributeAsync`
- `NotHaveAttributeValueAsync`
- `NotHaveClassAsync`
- `NotHaveValueAsync`

Content:

- `HaveContentAsync`
- `NotHaveContentAsync`

Element:

- `HaveElementAsync`
- `HaveElementCountAsync`
- `HaveElementWithContentAsync`
- `HaveElementWithContentCountAsync`

State:

- `BeCheckedAsync`
- `BeDisabledAsync`
- `BeEditableAsync`
- `BeEnabledAsync`
- `BeHiddenAsync`
- `BeReadOnlyAsync`
- `BeRequiredAsync`
- `BeSelectedAsync`
- `BeVisibleAsync`
- `Exist`
- `HaveFocusAsync`
- `NotBeCheckedAsync`
- `NotBeEditableAsync`
- `NotBeReadOnlyAsync`
- `NotBeRequiredAsync`
- `NotBeSelectedAsync`
- `NotExist`
- `NotHaveFocusAsync`

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/playwright-dotnet-contrib](https://github.com/hlaueriksson/playwright-dotnet-contrib)
