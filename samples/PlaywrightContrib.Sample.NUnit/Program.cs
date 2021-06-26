using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.Contrib.Extensions;
using Microsoft.Playwright.Contrib.FluentAssertions;
using System.Threading.Tasks;

var playwright = await Playwright.CreateAsync();
var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();

// Playwright.Contrib.Extensions

await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
var link = await page.QuerySelectorWithContentAsync("h1 a", "playwright-dotnet");
(await link.HrefAsync()).Should().Be("https://github.com/microsoft/playwright-dotnet");
(await page.HasContentAsync("Playwright for .NET is the official language port of Playwright")).Should().BeTrue();

await page.ClickAsync("a span[data-content='Actions']");
await page.WaitForNavigationAsync();
var latestStatus = await page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
latestStatus.Exists().Should().BeTrue();
(await latestStatus.HasAttributeValueAsync("title", "This workflow run completed successfully.")).Should().BeTrue();

// Playwright.Contrib.FluentAssertions

//await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
//var link = await page.QuerySelectorAsync("h1 strong a");
//await link.Should().HaveContentAsync("playwright-dotnet");
//await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");
//await page.Should().HaveContentAsync("Playwright for .NET is the official language port of Playwright");

//await page.ClickAsync("a span[data-content='Actions']");
//await page.WaitForNavigationAsync();
//var latestStatus = await page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
//latestStatus.Should().Exist();
//await latestStatus.Should().HaveAttributeValueAsync("title", "This workflow run completed successfully.");

// Playwright.Contrib.PageObjects

//var repoPage = await page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");
//var link = await repoPage.Link;
//await link.Should().HaveContentAsync("playwright-dotnet");
//await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");

//var actionsPage = await repoPage.GotoActionsAsync();
//var latestStatus = await actionsPage.GetLatestWorkflowRunStatusAsync();
//latestStatus.Should().Be("This workflow run completed successfully.");

//public class GitHubRepoPage : PageObject
//{
//    [Selector("h1 strong a")]
//    public virtual Task<IElementHandle> Link { get; }

//    [Selector("a span[data-content='Actions']")]
//    public virtual Task<IElementHandle> Actions { get; }

//    public async Task<GitHubActionsPage> GotoActionsAsync()
//    {
//        await (await Actions).ClickAsync();
//        return await Page.WaitForNavigationAsync<GitHubActionsPage>();
//    }
//}

//public class GitHubActionsPage : PageObject
//{
//    public async Task<string> GetLatestWorkflowRunStatusAsync()
//    {
//        var status = await Page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
//        return await status.GetAttributeAsync("title");
//    }
//}
