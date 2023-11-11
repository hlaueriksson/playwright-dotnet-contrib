//using FluentAssertions;
//using Microsoft.Playwright;
//using PlaywrightContrib.Extensions;
//using PlaywrightContrib.FluentAssertions;
//using PlaywrightContrib.PageObjects;
//using System.Threading.Tasks;

//var playwright = await Playwright.CreateAsync();
//var browser = await playwright.Chromium.LaunchAsync();
//var page = await browser.NewPageAsync();

//// PlaywrightContrib.Extensions

//await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
//var link = await page.QuerySelectorWithContentAsync("a", "playwright-dotnet");
//(await link.HrefAsync()).Should().Be("https://github.com/microsoft/playwright-dotnet");
//(await page.HasContentAsync("Playwright for .NET is the official language port of Playwright")).Should().BeTrue();

//await page.ClickAsync("#actions-tab");
//await page.WaitForSelectorAsync("#partial-actions-workflow-runs");
//var latestStatus = await page.QuerySelectorAsync(".checks-list-item-icon svg");
//latestStatus.Exists().Should().BeTrue();
//(await latestStatus.HasAttributeValueAsync("aria-label", "completed successfully")).Should().BeTrue();

//// PlaywrightContrib.FluentAssertions

//await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
//var link = await page.QuerySelectorAsync("#repository-container-header strong a");
//await link.Should().HaveContentAsync("playwright-dotnet");
//await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");
//await page.Should().HaveContentAsync("Playwright for .NET is the official language port of Playwright");

//await page.ClickAsync("#actions-tab");
//await page.WaitForSelectorAsync("#partial-actions-workflow-runs");
//var latestStatus = await page.QuerySelectorAsync(".checks-list-item-icon svg");
//latestStatus.Should().Exist();
//await latestStatus.Should().HaveAttributeValueAsync("aria-label", "completed successfully");

//// PlaywrightContrib.PageObjects

//var repoPage = await page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");
//var link = await repoPage.Link;
//await link.Should().HaveContentAsync("playwright-dotnet");
//await link.Should().HaveAttributeValueAsync("href", "/microsoft/playwright-dotnet");

//var actionsPage = await repoPage.GotoActionsAsync();
//var latestStatus = await actionsPage.GetLatestWorkflowRunStatusAsync();
//latestStatus.Should().Be("completed successfully");

//public class GitHubRepoPage : PageObject
//{
//    [Selector("#repository-container-header strong a")]
//    public virtual Task<IElementHandle> Link { get; }

//    [Selector("#actions-tab")]
//    public virtual Task<IElementHandle> Actions { get; }

//    public async Task<GitHubActionsPage> GotoActionsAsync()
//    {
//        await (await Actions).ClickAsync();
//        await Page.WaitForSelectorAsync("#partial-actions-workflow-runs");
//        return Page.To<GitHubActionsPage>();
//    }
//}

//public class GitHubActionsPage : PageObject
//{
//    public async Task<string> GetLatestWorkflowRunStatusAsync()
//    {
//        var status = await Page.QuerySelectorAsync(".checks-list-item-icon svg");
//        return await status.GetAttributeAsync("aria-label");
//    }
//}
