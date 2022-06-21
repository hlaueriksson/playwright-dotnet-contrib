using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.Extensions;
using PlaywrightContrib.FluentAssertions;

namespace PlaywrightContrib.Sample.NUnit
{
    public class PlaywrightDotnetRepoTests : PageTest
    {
        [Test]
        public async Task Should_be_first_search_result_on_GitHub()
        {
            await Page.GotoAsync("https://github.com/");
            var h1 = await Page.QuerySelectorAsync("h1");
            await h1.Should().HaveContentAsync("Where the world builds software");

            var input = await Page.QuerySelectorAsync("input.header-search-input");
            if (await input.IsHiddenAsync()) await Page.ClickAsync(".octicon-three-bars");
            await Page.TypeAsync("input.header-search-input", "playwright dotnet");
            await Page.Keyboard.PressAsync("Enter");
            await Page.WaitForNavigationAsync();

            var repositories = await Page.QuerySelectorAllAsync(".repo-list-item");
            repositories.Should().NotBeEmpty();
            var repository = repositories.First();
            await repository.Should().HaveContentAsync("microsoft/playwright-dotnet");
            var text = await repository.QuerySelectorAsync("p");
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.QuerySelectorAsync("a");
            await link.ClickAsync();

            h1 = await Page.QuerySelectorAsync("article > h1");
            await h1.Should().HaveContentAsync("Playwright for .NET");
            Page.Url.Should().Be("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            await Page.ClickAsync("#actions-tab");
            await Page.WaitForNavigationAsync();

            var status = await Page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
            await status.Should().HaveAttributeValueAsync("title", "This workflow run completed successfully.");
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_TypeScript_version()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await GetLatestReleaseVersion();

            await Page.GotoAsync("https://github.com/microsoft/playwright");
            var typescriptVersion = await GetLatestReleaseVersion();

            dotnetVersion.Should().BeEquivalentTo(typescriptVersion);

            async Task<string> GetLatestReleaseVersion()
            {
                var latest = await Page.QuerySelectorWithContentAsync("a[href*='releases'] span", @"v\d\.\d+\.\d");
                return await latest.TextContentAsync();
            }
        }
    }
}
