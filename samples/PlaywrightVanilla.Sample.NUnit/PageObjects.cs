using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightVanilla.Sample.NUnit
{
    public class GitHubStartPage : PageObject
    {
        public ILocator Heading => Page.Locator("h1");

        public GitHubStartPage(IPage page) : base(page)
        {
        }

        public async Task GotoAsync()
        {
            await Page.GotoAsync("https://github.com/");
            await Expect(Heading).ToHaveTextAsync("Where the world builds software");
        }

        public async Task<GitHubSearchPage> SearchAsync(string text)
        {
            var input = Page.Locator("[placeholder='Search GitHub']");
            if (await input.IsHiddenAsync()) await Page.Locator("[aria-label='Toggle navigation']").First.ClickAsync();
            await input.FillAsync("playwright dotnet");
            await Page.Keyboard.PressAsync("Enter");
            await Page.WaitForNavigationAsync();
            return new GitHubSearchPage(Page);
        }
    }

    public class GitHubSearchPage : PageObject
    {
        public ILocator Repositories => Page.Locator(".repo-list-item");

        public GitHubSearchPage(IPage page) : base(page)
        {
        }

        public async Task<GitHubRepoPage> GotoAsync(ILocator repository)
        {
            await Link(repository).ClickAsync();
            return new GitHubRepoPage(Page);
        }

        public ILocator About(ILocator repository) => repository.Locator("p");

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
            var status = Page.Locator("#partial-actions-workflow-runs .Box-row div[title]").First;
            return await status.GetAttributeAsync("title");
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
