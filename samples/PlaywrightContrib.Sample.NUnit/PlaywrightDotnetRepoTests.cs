using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightContrib.Extensions;
using PlaywrightContrib.FluentAssertions;

namespace PlaywrightContrib.Sample.NUnit
{
    public class PlaywrightDotnetRepoTests
    {
        IBrowser Browser { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
        }

        [Test]
        public async Task Should_be_first_search_result_on_GitHub()
        {
            var page = await Browser.NewPageAsync();

            await page.GotoAsync("https://github.com/");
            var h1 = await page.QuerySelectorAsync("h1");
            await h1.Should().HaveContentAsync("Where the world builds software");

            var input = await page.QuerySelectorAsync("input.header-search-input");
            if (await input.IsHiddenAsync()) await page.ClickAsync(".octicon-three-bars");
            await page.TypeAsync("input.header-search-input", "playwright dotnet");
            await page.Keyboard.PressAsync("Enter");
            await page.WaitForNavigationAsync();

            var repositories = await page.QuerySelectorAllAsync(".repo-list-item");
            repositories.Should().NotBeEmpty();
            var repository = repositories.First();
            await repository.Should().HaveContentAsync("microsoft/playwright-dotnet");
            var text = await repository.QuerySelectorAsync("p");
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.QuerySelectorAsync("a");
            await link.ClickAsync();

            h1 = await page.QuerySelectorAsync("article > h1");
            await h1.Should().HaveContentAsync("Playwright for .NET");
            page.Url.Should().Be("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            var page = await Browser.NewPageAsync();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            await page.ClickAsync("a span[data-content='Actions']");
            await page.WaitForNavigationAsync();

            var status = await page.QuerySelectorAsync("#partial-actions-workflow-runs .Box-row div[title]");
            await status.Should().HaveAttributeValueAsync("title", "This workflow run completed successfully.");
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_TypeScript_version()
        {
            var page = await Browser.NewPageAsync();

            await page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await GetLatestReleaseVersion();

            await page.GotoAsync("https://github.com/microsoft/playwright");
            var typescriptVersion = await GetLatestReleaseVersion();

            dotnetVersion.Should().BeEquivalentTo(typescriptVersion);

            async Task<string> GetLatestReleaseVersion()
            {
                var latest = await page.QuerySelectorWithContentAsync("a[href*='releases'] span", @"v\d\.\d+\.\d");
                return await latest.TextContentAsync();
            }
        }
    }
}
