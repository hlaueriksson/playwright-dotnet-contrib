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
            await Expect(Page.Locator("h1")).ToHaveTextAsync("Where the world builds software");

            var input = Page.Locator("[placeholder='Search GitHub']");
            if (await input.IsHiddenAsync()) await Page.Locator("[aria-label='Toggle navigation']").First.ClickAsync();
            await input.FillAsync("playwright dotnet");
            await Page.Keyboard.PressAsync("Enter");
            await Page.WaitForNavigationAsync();

            var repositories = Page.Locator(".repo-list-item");
            Assert.True(await repositories.CountAsync() > 0);
            var repository = repositories.First;
            await Expect(repository.Locator("p")).ToHaveTextAsync(".NET version of the Playwright testing and automation library.");
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

            var status = Page.Locator("#partial-actions-workflow-runs .Box-row div[title]").First;
            await Expect(status).ToHaveAttributeAsync("title", "This workflow run completed successfully.");
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
