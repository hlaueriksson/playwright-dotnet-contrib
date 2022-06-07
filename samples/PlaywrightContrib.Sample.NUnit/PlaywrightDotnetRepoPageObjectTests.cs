using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.FluentAssertions;
using PlaywrightContrib.PageObjects;

namespace PlaywrightContrib.Sample.NUnit
{
    public class PlaywrightDotnetRepoPageObjectTests : PageTest
    {
        [Test]
        public async Task Should_be_first_search_result_on_GitHub()
        {
            var startPage = await Page.GotoAsync<GitHubStartPage>("https://github.com/");
            var heading = await startPage.Heading;
            await heading.Should().HaveContentAsync("Where the world builds software");

            var searchPage = await startPage.SearchAsync("playwright dotnet");
            var repositories = await searchPage.RepoListItems;
            repositories.Should().NotBeEmpty();
            var repository = repositories.First();
            var text = await repository.Text;
            await text.Should().HaveContentAsync(".NET version of the Playwright testing and automation library.");
            var link = await repository.Link;
            await link.Should().HaveContentAsync("microsoft/playwright-dotnet");

            var repoPage = await searchPage.GotoAsync(repository);
            heading = await repoPage.Heading;
            await heading.Should().HaveContentAsync("Playwright for .NET");
            repoPage.Page.Url.Should().Be("https://github.com/microsoft/playwright-dotnet");
        }

        [Test]
        public async Task Should_have_successful_build_status()
        {
            var repoPage = await Page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");

            var actionsPage = await repoPage.GotoActionsAsync();
            var status = await actionsPage.GetLatestWorkflowRunStatusAsync();
            status.Should().Be("This workflow run completed successfully.");
        }

        [Test]
        public async Task Should_be_up_to_date_with_the_TypeScript_version()
        {
            var repoPage = await Page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright-dotnet");
            var dotnetVersion = await repoPage.GetLatestReleaseVersionAsync();

            repoPage = await Page.GotoAsync<GitHubRepoPage>("https://github.com/microsoft/playwright");
            var typescriptVersion = await repoPage.GetLatestReleaseVersionAsync();

            dotnetVersion.Should().BeEquivalentTo(typescriptVersion);
        }
    }
}
