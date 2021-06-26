using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.FluentAssertions;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Sample
{
    public class FluentAssertionsTests
    {
        IBrowser Browser { get; set; }
        IPage Page { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync();
            Page = await Browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
        }

        [Test]
        public async Task Attributes()
        {
            await Page.SetContentAsync("<div data-foo='bar' />");
            await Page.Should().HaveElementAttributeAsync("div", "data-foo");
            await Page.Should().HaveElementAttributeValueAsync("div", "data-foo", "bar");
            await Page.Should().NotHaveElementAttributeAsync("div", "data-bar");
            await Page.Should().NotHaveElementAttributeValueAsync("div", "data-foo", "foo");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().HaveAttributeAsync("data-foo");
            await div.Should().HaveAttributeValueAsync("data-foo", "bar");
            await div.Should().NotHaveAttributeAsync("data-bar");
            await div.Should().NotHaveAttributeValueAsync("data-foo", "foo");
        }

        [Test]
        public async Task Class()
        {
            await Page.SetContentAsync("<div class='foo' />");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().HaveClassAsync("foo");
            await div.Should().NotHaveClassAsync("bar");
        }

        [Test]
        public async Task Content()
        {
            await Page.SetContentAsync("<div>Foo</div>");
            await Page.Should().HaveContentAsync("Foo");
            await Page.Should().NotHaveContentAsync("Bar");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().HaveContentAsync("Foo");
            await div.Should().NotHaveContentAsync("Bar");
        }

        [Test]
        public async Task Visibility()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar' style='display:none'>Bar</div>
</html>");
            await Page.Should().HaveVisibleElementAsync("#foo");
            await Page.Should().HaveHiddenElementAsync("#bar");

            var html = await Page.QuerySelectorAsync("html");
            html.Should().Exist();

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeVisibleAsync();

            div = await Page.QuerySelectorAsync("#bar");
            await div.Should().BeHiddenAsync();
        }

        [Test]
        public async Task State()
        {
            await Page.SetContentAsync(@"
<form>
  <input type='text' autofocus required value='Foo Bar'>
  <input type='radio' readonly>
  <input type='checkbox' checked>
  <select>
    <option id='foo'>Foo</option>
    <option id='bar'>Bar</option>
  </select>
</form>
");
            await Page.Should().HaveEditableElementAsync("input[type=text]");
            await Page.Should().HaveEnabledElementAsync("input[type=radio]");
            await Page.Should().NotHaveEditableElementAsync("input[type=radio]");
            await Page.Should().HaveCheckedElementAsync("input[type=checkbox]");

            var input = await Page.QuerySelectorAsync("input[type=text]");
            await input.Should().HaveFocusAsync();
            await input.Should().BeRequiredAsync();
            await input.Should().HaveValueAsync("Foo Bar");

            input = await Page.QuerySelectorAsync("input[type=radio]");
            await input.Should().BeEnabledAsync();
            await input.Should().BeReadOnlyAsync();

            input = await Page.QuerySelectorAsync("input[type=checkbox]");
            await input.Should().BeCheckedAsync();

            input = await Page.QuerySelectorAsync("#foo");
            await input.Should().BeSelectedAsync();
        }

        [Test]
        public async Task Element()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");
            await Page.Should().HaveElementAsync("div");
            await Page.Should().HaveElementCountAsync(3, "div");
            await Page.Should().HaveElementWithContentAsync("div", "Ba.");
            await Page.Should().HaveElementWithContentCountAsync(2, "div", "Ba.");

            var html = await Page.QuerySelectorAsync("html");
            await html.Should().HaveElementAsync("div");
            await html.Should().HaveElementCountAsync(3, "div");
            await html.Should().HaveElementWithContentAsync("div", "Ba.");
            await html.Should().HaveElementWithContentCountAsync(2, "div", "Ba.");
        }

        [Test, Explicit]
        public async Task Assertion_message()
        {
            await Page.SetContentAsync(@"
<html>
  <body>
   <div id='foo'>Foo</div>
  <body>
</html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().HaveContentAsync("Bar");
        }

        [Test, Explicit]
        public async Task Assertion_message_with_because()
        {
            await Page.SetContentAsync(@"
<html>
  <body>
   <form>
    <input id='foo' value='Foo' />
   </form>
  <body>
</html>");

            var input = await Page.QuerySelectorAsync("#foo");
            await input.Should().HaveValueAsync("Bar", "that would be the perfect example");
        }
    }
}
