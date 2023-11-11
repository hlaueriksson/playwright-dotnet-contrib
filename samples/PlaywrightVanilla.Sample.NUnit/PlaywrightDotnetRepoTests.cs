using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightVanilla.Sample.NUnit
{
    public class PlaywrightDotnetRepoTests : PageTest
    {
        [Test]
        public async Task Should_be_first_search_result_on_GitHub()
        {
            await Page.GotoAsync("https://github.com/");
            await Expect(Page.Locator("main h1")).ToHaveTextAsync("Let’s build from here");

            var input = Page.Locator("#query-builder-test");
            if (await input.IsHiddenAsync())
            {
                //await Page.ClickAsync("[aria-label=\"Toggle navigation\"][data-view-component=\"true\"]");
                await Page.ClickAsync("[data-target=\"qbsearch-input.inputButtonText\"]");
            }
            await input.FillAsync("playwright dotnet");
            await Page.Keyboard.PressAsync("Enter");
            await Page.WaitForSelectorAsync("[data-testid=\"results-list\"]");

            var repositories = Page.Locator("[data-testid=\"results-list\"] > div");
            Assert.True(await repositories.CountAsync() > 0);
            var repository = repositories.First;
            await Expect(repository.Locator("h3 + div")).ToHaveTextAsync(".NET version of the Playwright testing and automation library.");
            var link = repository.Locator("a").First;
            await Expect(link).ToHaveTextAsync("microsoft/playwright-dotnet");
            await link.ClickAsync();

            await Expect(Page.Locator("article > h1")).ToHaveTextAsync("Playwright for .NET 🎭");
            await Expect(Page).ToHaveURLAsync("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            await Page.ClickAsync("#actions-tab");
            await Page.WaitForSelectorAsync("#partial-actions-workflow-runs");

            var status = Page.Locator(".checks-list-item-icon svg").First;
            await Expect(status).ToHaveAttributeAsync("aria-label", "completed successfully");
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_Node_version()
        {
            await Page.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await GetLatestReleaseVersion();

            await Page.GotoAsync("https://github.com/microsoft/playwright");
            var nodeVersion = await GetLatestReleaseVersion();

            Assert.AreEqual(nodeVersion, dotnetVersion);

            async Task<string> GetLatestReleaseVersion()
            {
                var latest = Page.Locator("a[href*='releases'] span", new() { HasTextRegex = new(@"v\d\.\d+\.\d") });
                return await latest.TextContentAsync();
            }
        }
    }
}
