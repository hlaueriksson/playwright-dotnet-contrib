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
            var heading = await Page.QuerySelectorAsync("main h1");
            await heading.Should().HaveContentAsync("Let’s build from here");

            var input = await Page.QuerySelectorAsync("#query-builder-test");
            if (await input.IsHiddenAsync())
            {
                //await Page.ClickAsync("[aria-label=\"Toggle navigation\"][data-view-component=\"true\"]");
                await Page.ClickAsync("[data-target=\"qbsearch-input.inputButtonText\"]");
            }
            await Page.TypeAsync("#query-builder-test", "playwright dotnet");
            await Page.Keyboard.PressAsync("Enter");
            await Page.WaitForSelectorAsync("[data-testid=\"results-list\"]");

            var repositories = await Page.QuerySelectorAllAsync("[data-testid=\"results-list\"] > div");
            repositories.Should().NotBeEmpty();
            var repository = repositories.First();
            await repository.Should().HaveContentAsync("microsoft/playwright-dotnet");
            var text = await repository.QuerySelectorAsync("h3 + div");
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.QuerySelectorAsync("a");
            await link.ClickAsync();
            await Page.WaitForSelectorAsync("article > h1");

            heading = await Page.QuerySelectorAsync("article > h1");
            await heading.Should().HaveContentAsync("Playwright for .NET");
            Page.Url.Should().Be("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            await Page.ClickAsync("#actions-tab");
            await Page.WaitForSelectorAsync("#partial-actions-workflow-runs");

            var status = await Page.QuerySelectorAsync(".checks-list-item-icon svg");
            await status.Should().HaveAttributeValueAsync("aria-label", "completed successfully");
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
