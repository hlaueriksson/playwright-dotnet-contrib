using System.Threading.Tasks;
using NUnit.Framework;

namespace PlaywrightVanilla.Sample.NUnit
{
    public class PlaywrightDotnetRepoPageObjectTests : PageObjectTests
    {
        [Test]
        public async Task Should_be_first_search_result_on_GitHub()
        {
            var startPage = new GitHubStartPage(Page);
            await startPage.GotoAsync();

            var searchPage = await startPage.SearchAsync("playwright dotnet");
            Assert.True(await searchPage.Repositories.CountAsync() > 0);
            var repository = searchPage.Repositories.First;
            await Expect(searchPage.About(repository)).ToHaveTextAsync(".NET version of the Playwright testing and automation library.");
            await Expect(searchPage.Link(repository)).ToHaveTextAsync("microsoft/playwright-dotnet");

            var repoPage = await searchPage.GotoAsync(repository);
            await Expect(repoPage.Heading).ToHaveTextAsync("Playwright for .NET 🎭");
            await Expect(repoPage).ToHaveURLAsync("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            var repoPage = new GitHubRepoPage(Page);
            await repoPage.GotoAsync("https://github.com/microsoft/playwright-dotnet");

            var actionsPage = await repoPage.GotoActionsAsync();
            var status = await actionsPage.GetLatestWorkflowRunStatusAsync();
            Assert.AreEqual("This workflow run completed successfully.", status);
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_Node_version()
        {
            var repoPage = new GitHubRepoPage(Page);
            await repoPage.GotoAsync("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await repoPage.GetLatestReleaseVersionAsync();

            await repoPage.GotoAsync("https://github.com/microsoft/playwright");
            var nodeVersion = await repoPage.GetLatestReleaseVersionAsync();

            Assert.AreEqual(nodeVersion, dotnetVersion);
        }
    }
}
