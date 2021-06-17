using System;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.Extensions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.Extensions
{
    [Parallelizable(ParallelScope.Self)]
    public class ElementHandleAttributeExtensionsTests : PageTest
    {
        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task IdAsync_should_return_the_id_of_the_element()
        {
            await Page.SetContentAsync("<html><body><input id='foo' value='input' /><button id='bar' value='button' /></body></html>");

            var input = await Page.QuerySelectorAsync("input");
            Assert.AreEqual("foo", await input.IdAsync());

            var body = await Page.QuerySelectorAsync("body");
            Assert.Null(await body.IdAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).IdAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task NameAsync_should_return_the_name_of_the_element()
        {
            await Page.SetContentAsync("<html><body><input name='foo' value='input' /><button name='bar' value='button' /></body></html>");

            var input = await Page.QuerySelectorAsync("input");
            Assert.AreEqual("foo", await input.NameAsync());

            var body = await Page.QuerySelectorAsync("body");
            Assert.Null(await body.NameAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).NameAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task HrefAsync_should_return_the_href_of_the_element()
        {
            await Page.SetContentAsync("<html><body><a href='file.html'></a><link href='file.css' /></body></html>");

            var a = await Page.QuerySelectorAsync("a");
            Assert.AreEqual("file.html", await a.HrefAsync());

            var body = await Page.QuerySelectorAsync("body");
            Assert.Null(await body.HrefAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).HrefAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task SrcAsync_should_return_the_src_of_the_element()
        {
            await Page.SetContentAsync("<html><body><img src='image.png' /><iframe src='file.html'></iframe></body></html>");

            var img = await Page.QuerySelectorAsync("img");
            Assert.AreEqual("image.png", await img.SrcAsync());

            var body = await Page.QuerySelectorAsync("body");
            Assert.Null(await body.SrcAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).SrcAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task ValueAsync_should_return_the_value_of_the_element()
        {
            await Page.SetContentAsync("<html><body><input value='input' /><button value='button' /></body></html>");

            var input = await Page.QuerySelectorAsync("input");
            Assert.AreEqual("input", await input.ValueAsync());

            var body = await Page.QuerySelectorAsync("body");
            Assert.Null(await body.ValueAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).ValueAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task GetAttributeOrDefaultAsync_should_return_the_attribute_value_of_the_element()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            Assert.AreEqual("class", await div.GetAttributeOrDefaultAsync("class"));

            div = await Page.QuerySelectorAsync("div");
            Assert.Null(await div.GetAttributeOrDefaultAsync("id"));

            var body = await Page.QuerySelectorAsync("body");
            Assert.Null(await body.GetAttributeOrDefaultAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).GetAttributeOrDefaultAsync(""));
        }
    }
}
