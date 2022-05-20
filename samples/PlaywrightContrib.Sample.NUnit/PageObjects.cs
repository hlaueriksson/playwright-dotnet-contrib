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

        [Selector("#actions-tab")]
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
