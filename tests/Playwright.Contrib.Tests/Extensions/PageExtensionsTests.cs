using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.Extensions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.Extensions
{
    [Parallelizable(ParallelScope.Self)]
    public class PageExtensionsTests : PageTest
    {
        [SetUp]
        public async Task SetUp() => await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task QuerySelectorWithContentAsync_should_return_the_first_element_that_match_the_selector_and_has_the_content()
        {
            var foo = await Page.QuerySelectorWithContentAsync("div", "Foo");
            Assert.AreEqual("foo", await foo.IdAsync());

            var bar = await Page.QuerySelectorWithContentAsync("div", "Ba.");
            Assert.AreEqual("bar", await bar.IdAsync());

            var flags = await Page.QuerySelectorWithContentAsync("div", "foo", "i");
            Assert.AreEqual("foo", await flags.IdAsync());

            var missing = await Page.QuerySelectorWithContentAsync("div", "Missing");
            Assert.Null(missing);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).QuerySelectorWithContentAsync("", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task QuerySelectorAllWithContentAsync_should_return_all_elements_that_match_the_selector_and_has_the_content()
        {
            var divs = await Page.QuerySelectorAllWithContentAsync("div", "Foo");
            Assert.AreEqual(new[] { "foo" }, await Task.WhenAll(divs.Select(x => x.IdAsync())));

            divs = await Page.QuerySelectorAllWithContentAsync("div", "Ba.");
            Assert.AreEqual(new[] { "bar", "baz" }, await Task.WhenAll(divs.Select(x => x.IdAsync())));

            var flags = await Page.QuerySelectorAllWithContentAsync("div", "foo", "i");
            Assert.AreEqual(new[] { "foo" }, await Task.WhenAll(flags.Select(x => x.IdAsync())));

            var missing = await Page.QuerySelectorAllWithContentAsync("div", "Missing");
            Assert.IsEmpty(missing);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).QuerySelectorAllWithContentAsync("", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task HasContentAsync_should_return_true_if_page_has_the_content()
        {
            Assert.True(await Page.HasContentAsync("Ba."));
            Assert.True(await Page.HasContentAsync("ba.", "i"));
            Assert.True(await Page.HasContentAsync(""));
            Assert.False(await Page.HasContentAsync("Missing"));
            Assert.False(await Page.HasContentAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).HasContentAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task HasTitleAsync_should_return_true_if_page_has_the_title()
        {
            await Page.SetContentAsync(@"
<html>
 <head>
  <title>Foo Bar Baz</title>
 </head>
</html>");

            Assert.True(await Page.HasTitleAsync("Ba."));
            Assert.True(await Page.HasTitleAsync("ba.", "i"));
            Assert.True(await Page.HasTitleAsync(""));
            Assert.False(await Page.HasTitleAsync("Missing"));
            Assert.False(await Page.HasTitleAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).HasTitleAsync(""));
        }
    }
}
