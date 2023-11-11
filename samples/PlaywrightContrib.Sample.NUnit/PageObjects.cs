using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightContrib.Extensions;
using PlaywrightContrib.PageObjects;

namespace PlaywrightContrib.Sample.NUnit
{
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

    public class GitHubHeader : ElementObject
    {
        [Selector("#query-builder-test")]
        public virtual Task<IElementHandle> SearchInput { get; }

        [Selector("[aria-label=\"Toggle navigation\"][data-view-component=\"true\"]")]
        public virtual Task<IElementHandle> NavigationButton { get; }

        [Selector("[data-target=\"qbsearch-input.inputButtonText\"]")]
        public virtual Task<IElementHandle> SearchButton { get; }

        public async Task SearchAsync(string text)
        {
            var input = await SearchInput;
            if (await input.IsHiddenAsync())
            {
                //await (await NavigationButton).ClickAsync();
                await (await SearchButton).ClickAsync();
            }
            await input.TypeAsync(text);
            await input.PressAsync("Enter");
        }
    }

    public class GitHubSearchPage : PageObject
    {
        [Selector("[data-testid=\"results-list\"] > div")]
        public virtual Task<IReadOnlyList<GitHubRepoListItem>> RepoListItems { get; }

        public async Task<GitHubRepoPage> GotoAsync(GitHubRepoListItem repo)
        {
            var link = await repo.Link;
            await link.ClickAsync();
            await Page.WaitForSelectorAsync("article > h1");

            return Page.To<GitHubRepoPage>();
        }
    }

    public class GitHubRepoListItem : ElementObject
    {
        [Selector("a")]
        public virtual Task<IElementHandle> Link { get; }

        [Selector("h3 + div")]
        public virtual Task<IElementHandle> Text { get; }
    }

    public class GitHubRepoPage : PageObject
    {
        [Selector("article > h1")]
        public virtual Task<IElementHandle> Heading { get; }

        [Selector("#actions-tab")]
        public virtual Task<IElementHandle> Actions { get; }

        public async Task<GitHubActionsPage> GotoActionsAsync()
        {
            await (await Actions).ClickAsync();
            await Page.WaitForSelectorAsync("#partial-actions-workflow-runs");
            return Page.To<GitHubActionsPage>();
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
            var status = await Page.QuerySelectorAsync(".checks-list-item-icon svg");
            return await status.GetAttributeAsync("aria-label");
        }
    }
}
