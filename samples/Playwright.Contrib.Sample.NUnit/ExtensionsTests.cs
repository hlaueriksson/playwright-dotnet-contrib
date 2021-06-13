using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Contrib.Extensions;

namespace Playwright.Contrib.Sample
{
    public class ExtensionsTests
    {
        IBrowser Browser { get; set; }
        IPage Page { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync(); // TODO: why FQN
            Browser = await playwright.Chromium.LaunchAsync();
            Page = await Browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
        }

        [Test]
        public async Task Query()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

            var html = await Page.QuerySelectorAsync("html");

            var div = await Page.QuerySelectorWithContentAsync("div", "Ba.");
            Assert.AreEqual("bar", await div.IdAsync());

            div = await html.QuerySelectorWithContentAsync("div", "Ba.");
            Assert.AreEqual("bar", await div.IdAsync());

            var divs = await Page.QuerySelectorAllWithContentAsync("div", "Ba.");
            Assert.AreEqual(new[] { "bar", "baz" }, await Task.WhenAll(divs.Select(x => x.IdAsync())));

            divs = await html.QuerySelectorAllWithContentAsync("div", "Ba.");
            Assert.AreEqual(new[] { "bar", "baz" }, await Task.WhenAll(divs.Select(x => x.IdAsync())));
        }

        [Test]
        public async Task Attributes()
        {
            await Page.SetContentAsync(@"
<html>
  <form method='post'>
      Name: <input type='text' name='name' id='name' required>
      Email: <input type='email' name='email' id='email' required>
      <input type='submit' value='Subscribe!'>
  </form>
  <img src='unsubscribe.png' />
  <a href='/unsubscribe/'>Unsubscribe</a>
</html>");

            var input = await Page.QuerySelectorAsync("#name");
            Assert.True(await input.HasAttributeAsync("required"));

            var link = await Page.QuerySelectorAsync("a");
            Assert.AreEqual("/unsubscribe/", await link.HrefAsync());

            input = await Page.QuerySelectorAsync("input[type=email]");
            Assert.AreEqual("email", await input.IdAsync());

            input = await Page.QuerySelectorAsync("#email");
            Assert.AreEqual("email", await input.NameAsync());

            var img = await Page.QuerySelectorAsync("img");
            Assert.AreEqual("unsubscribe.png", await img.SrcAsync());

            input = await Page.QuerySelectorAsync("input[type=submit]");
            Assert.AreEqual("Subscribe!", await input.ValueAsync());
        }

        [Test]
        public async Task Class()
        {
            await Page.SetContentAsync("<div class='foo bar' />");

            var div = await Page.QuerySelectorAsync("div");
            Assert.AreEqual("foo bar", await div.ClassNameAsync());
            Assert.AreEqual(new[] { "foo", "bar" }, await div.ClassListAsync());
            Assert.True(await div.HasClassAsync("bar"));
        }

        [Test]
        public async Task Content()
        {
            await Page.SetContentAsync(@"
<html>
  <head>
    <title>Foo Bar Baz</title>
  </head>
  <body>
    <div>
      Foo
      <span>Bar</span>
    </div>
  </body>
</html>
");

            Assert.True(await Page.HasTitleAsync("Foo Bar Baz"));

            var html = await Page.QuerySelectorAsync("html");
            Assert.True(await html.HasContentAsync("Foo"));

            var div = await Page.QuerySelectorAsync("div");
            Assert.AreEqual("<div>\n      Foo\n      <span>Bar</span>\n    </div>", await div.OuterHtmlAsync());
        }

        [Test]
        public async Task Input()
        {
            await Page.SetContentAsync(@"
<form>
  <input type='text' autofocus required>
  <input type='radio' readonly>
  <input type='checkbox' checked>
  <select>
    <option id='foo'>Foo</option>
    <option id='bar'>Bar</option>
  </select>
</form>
");

            var input = await Page.QuerySelectorAsync("input[type=text]");
            Assert.True(input.Exists());
            Assert.True(await input.HasFocusAsync());
            Assert.True(await input.IsRequiredAsync());

            input = await Page.QuerySelectorAsync("input[type=radio]");
            Assert.True(await input.IsReadOnlyAsync());

            input = await Page.QuerySelectorAsync("#foo");
            Assert.True(await input.IsSelectedAsync());
        }
    }
}
