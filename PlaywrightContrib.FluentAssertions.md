### Playwright.Contrib.FluentAssertions 🎭🧪

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

`Playwright.Contrib.FluentAssertions` is a library for writing tests with [`FluentAssertions`](https://www.nuget.org/packages/FluentAssertions/) against the Playwright API.

```cs
using Microsoft.Playwright;
using Microsoft.Playwright.Contrib.FluentAssertions;

var playwright = await Playwright.CreateAsync();
var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();

await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
var link = await page.QuerySelectorAsync("h1 strong a");
await link.Should().HaveContentAsync("playwright-dotnet");
await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");
await page.Should().HaveContentAsync("Playwright for .NET is the official language port of Playwright");

await page.ClickAsync("a span[data-content='Actions']");
await page.WaitForNavigationAsync();
var latestStatus = await page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
latestStatus.Should().Exist();
await latestStatus.Should().HaveAttributeValueAsync("title", "This workflow run completed successfully.");
```

### Assertions for `IPage.Should()` 📄

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

### Assertions for `IElementHandle.Should()` 📑

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

### Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/playwright-dotnet-contrib](https://github.com/hlaueriksson/playwright-dotnet-contrib)
