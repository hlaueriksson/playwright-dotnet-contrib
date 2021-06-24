### Playwright.Contrib.PageObjects 🎭🧪

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

`Playwright.Contrib.PageObjects` is a library for writing browser tests using the _page object pattern_ with the Playwright API.

```cs
using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.Contrib.FluentAssertions;
using Microsoft.Playwright.Contrib.PageObjects;
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
latestStatus.Should().Be("This workflow run completed successfully.");

public class GitHubRepoPage : PageObject
{
    [Selector("h1 strong a")]
    public virtual Task<IElementHandle> Link { get; }

    [Selector("a span[data-content='Actions']")]
    public virtual Task<IElementHandle> Actions { get; }

    public async Task<GitHubActionsPage> GotoActionsAsync()
    {
        await (await Actions).ClickAsync();
        return await Page.WaitForNavigationAsync<GitHubActionsPage>();
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
```

### Extensions for `IPage` 📄

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

### Extensions for `IElementHandle` 📑

Where `T` is an `ElementObject`:

* `To<T>`
* `QuerySelectorAllAsync<T>`
* `QuerySelectorAsync<T>`
* `WaitForSelectorAsync<T>`

### Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/playwright-dotnet-contrib](https://github.com/hlaueriksson/playwright-dotnet-contrib)
