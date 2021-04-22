using System.Threading.Tasks;
using Xunit;

namespace PlaywrightSharp.Contrib.Tests
{
    public class Todo
    {
        [Fact]
        public async Task<IPage> Page()
        {
            await Playwright.InstallAsync();
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(headless: true);

            return await browser.NewPageAsync();

            //page.EvaluateExpressionAsync<>()
            //page.EvaluateFunctionAsync<>()
            //page.EvaluateExpressionHandleAsync()
            //page.EvaluateFunctionHandleAsync()
            //page.QueryObjectsAsync()
        }

        [Fact]
        public async Task Assert()
        {
            var page = await Page();

            // Coypu:

            //Assert.That(page, Shows.Content("In France, the coypu is known as a ragondin");
            //Assert.That(page, Shows.No.Content("In France, the coypu is known as a ragondin");

            //Assert.That(page, Shows.Css("ul.menu > li");
            //Assert.That(page, Shows.Css("ul.menu > li", text: "Home");
            //Assert.That(page, Shows.No.Css("ul.menu > li", text: "Admin");

            //Assert.That(page, Shows.ContentContaining(Some","Words","Anywhere","in ","the","document"))
            //Assert.That(page, Shows.CssContaining("ul.menu > li", "match", "in", "any", "order"))
            //Assert.That(page, Shows.AllCssInOrder("ul.menu > li", "has", "exactly", "these", "matches"))

            //Assert.That(page.FindField("total"), Shows.Value("147"));
            //Assert.That(page.FindField("total"), Shows.No.Value("0"));

            // FluentAutomation:

            // Expect/Assert
            //Assert.Exists("#searchBar");
            //Assert.Count(1).Of("#searchBar");
            //Assert.Value(10).In("#quantity");
            //Assert.Value((value) => value.StartsWith("M")).In("#states");
            //Assert.Text("FluentAutomation").In("header");
            //Assert.Text((text) => text.Length > 50).In("#content");
            //Assert.Class("btn-primary").Of("header");
            //Assert.Url("http://fluent.stirno.com/docs/#assert-url");
            //Assert.Url((uri) => uri.Scheme == "https");
            //Assert.Throws(() => Assert.Exists(".error"));

            //var element = Find("select");
            //Assert.True(() => element().IsSelect);

            //var element = Find("input");
            //Assert.False(() => element().IsSelect);
        }

        [Fact]
        public async Task FluentSyntax()
        {
            var page = await Page();

            //.Select("Motorcycles").From(".liveExample tr select:eq(0)")
            //    .Select(2).From(".liveExample tr select:eq(1)")
            //    .Enter(6).In(".liveExample td.quantity input:eq(0)")
            //    .Expect
            //    .Text("$197.72").In(".liveExample tr span:eq(1)")
            //    .Value(6).In(".liveExample td.quantity input:eq(0)");
        }

        [Fact]
        public async Task Convenience()
        {
            var page = await Page();

            // Clear input
            // Append input
            // DoubleClick
            // RightClick
            // Keys; Enter, Esc, Tab, Backspace
        }
    }
}