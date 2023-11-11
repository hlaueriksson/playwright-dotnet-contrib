using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightVanilla.Sample.NUnit
{
    public class GitHubStartPage : PageObject
    {
        public ILocator Heading => Page.Locator("main h1");

        public GitHubStartPage(IPage page) : base(page)
        {
        }

        public async Task GotoAsync()
        {
            await Page.GotoAsync("https://github.com/");
            await Expect(Heading).ToHaveTextAsync("Let’s build from here");
        }

        public async Task<GitHubSearchPage> SearchAsync(string text)
        {
            var input = Page.Locator("#query-builder-test");
            if (await input.IsHiddenAsync())
            {
                //await Page.ClickAsync("[aria-label=\"Toggle navigation\"][data-view-component=\"true\"]");
                await Page.ClickAsync("[data-target=\"qbsearch-input.inputButtonText\"]");
            }
            await input.FillAsync("playwright dotnet");
            await Page.Keyboard.PressAsync("Enter");
            await Page.WaitForSelectorAsync("[data-testid=\"results-list\"]");
            return new GitHubSearchPage(Page);
        }
    }

    public class GitHubSearchPage : PageObject
    {
        public ILocator Repositories => Page.Locator("[data-testid=\"results-list\"] > div");

        public GitHubSearchPage(IPage page) : base(page)
        {
        }

        public async Task<GitHubRepoPage> GotoAsync(ILocator repository)
        {
            await Link(repository).ClickAsync();
            return new GitHubRepoPage(Page);
        }

        public ILocator About(ILocator repository) => repository.Locator("h3 + div");

        public ILocator Link(ILocator repository) => repository.Locator("a").First;
    }

    public class GitHubRepoPage : PageObject
    {
        public ILocator Heading => Page.Locator("article > h1");

        public GitHubRepoPage(IPage page) : base(page)
        {
        }

        public async Task GotoAsync(string url) => await Page.GotoAsync(url);

        public async Task<GitHubActionsPage> GotoActionsAsync()
        {
            await Page.Locator("#actions-tab").ClickAsync();
            return new GitHubActionsPage(Page);
        }

        public async Task<string> GetLatestReleaseVersionAsync()
        {
            var latest = Page.Locator("a[href*='releases'] span", new() { HasTextRegex = new(@"v\d\.\d+\.\d") });
            return await latest.TextContentAsync();
        }
    }

    public class GitHubActionsPage : PageObject
    {
        public GitHubActionsPage(IPage page) : base(page)
        {
        }

        public async Task<string> GetLatestWorkflowRunStatusAsync()
        {
            var status = Page.Locator(".checks-list-item-icon svg").First;
            return await status.GetAttributeAsync("aria-label");
        }
    }

    public class PageObject
    {
        protected readonly IPage Page;

        protected PageObject(IPage page) => Page = page;

        protected IPageAssertions Expect()
        {
            return Assertions.Expect(Page);
        }

        protected ILocatorAssertions Expect(ILocator locator)
        {
            return Assertions.Expect(locator);
        }
    }
}
