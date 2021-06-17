using System;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.FluentAssertions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.FluentAssertions
{
    [Parallelizable(ParallelScope.Self)]
    public class PageAssertionsTests : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            await Page.SetContentAsync("<html><body><div class='tweet'><div class='like'>100</div><div class='retweets'>10</div></div></body></html>");

            Page.SetDefaultTimeout((float)TimeSpan.FromSeconds(1).TotalMilliseconds);
        }

        // Content

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveContentAsync_throws_if_page_does_not_have_the_content()
        {
            await Page.Should().HaveContentAsync("10.");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveContentAsync("20.", "i"));
            Assert.AreEqual("Expected page to have content \"/20./i\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveContentAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveContentAsync_throws_if_element_has_the_content()
        {
            await Page.Should().NotHaveContentAsync("20.");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().NotHaveContentAsync("10.", "i"));
            Assert.AreEqual("Expected page not to have content \"/10./i\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().NotHaveContentAsync(""));
        }

        // Title

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveTitleAsync_throws_if_element_does_not_have_the_title()
        {
            await Page.SetContentAsync("<html><head><title>100</title></head></html>");

            await Page.Should().HaveTitleAsync("10.");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveTitleAsync("20.", "i"));
            Assert.AreEqual("Expected page to have title \"/20./i\", but found \"100\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveTitleAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveTitleAsync_throws_if_element_has_the_content()
        {
            await Page.SetContentAsync("<html><head><title>100</title></head></html>");

            await Page.Should().NotHaveTitleAsync("20.");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().NotHaveTitleAsync("10.", "i"));
            Assert.AreEqual("Expected page not to have title \"/10./i\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().NotHaveTitleAsync(""));
        }

        // Visible

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveVisibleElementAsync_throws_if_element_is_hidden()
        {
            await Page.SetContentAsync("<html><body><div id='foo'>Foo</div><div id='bar' style='display:none'>Bar</div></body></html>");

            await Page.Should().HaveVisibleElementAsync("#foo");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveVisibleElementAsync("#bar"));
            Assert.AreEqual("Expected element \"#bar\" on page to be visible.", ex.Message);

            Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveVisibleElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveVisibleElementAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveHiddenElementAsync_throws_if_element_is_visible()
        {
            await Page.SetContentAsync("<html><body><div id='foo'>Foo</div><div id='bar' style='display:none'>Bar</div></body></html>");

            await Page.Should().HaveHiddenElementAsync("#bar");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveHiddenElementAsync("#foo"));
            Assert.AreEqual("Expected element \"#foo\" on page to be hidden.", ex.Message);

            await Page.Should().HaveHiddenElementAsync(".missing");

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveHiddenElementAsync(""));
        }

        // Checked

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveCheckedElementAsync_throws_if_element_is_not_checked()
        {
            await Page.SetContentAsync("<html><body><input type='checkbox' id='foo' checked><input type='checkbox' id='bar'></body></html>");

            await Page.Should().HaveCheckedElementAsync("#foo");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveCheckedElementAsync("#bar"));
            Assert.AreEqual("Expected element \"#bar\" on page to be checked.", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().HaveCheckedElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveCheckedElementAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveCheckedElementAsync_throws_if_element_is_checked()
        {
            await Page.SetContentAsync("<html><body><input type='checkbox' id='foo' checked><input type='checkbox' id='bar'></body></html>");

            await Page.Should().NotHaveCheckedElementAsync("#bar");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().NotHaveCheckedElementAsync("#foo"));
            Assert.AreEqual("Expected element \"#foo\" on page not to be checked.", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().NotHaveCheckedElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().NotHaveCheckedElementAsync(""));
        }

        // Disabled

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveDisabledElementAsync_throws_if_element_is_enabled()
        {
            await Page.SetContentAsync("<html><body><input id='foo' disabled><input id='bar'></body></html>");

            await Page.Should().HaveDisabledElementAsync("#foo");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveDisabledElementAsync("#bar"));
            Assert.AreEqual("Expected element \"#bar\" on page to be disabled.", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().HaveDisabledElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveDisabledElementAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveEnabledElementAsync_throws_if_element_is_disabled()
        {
            await Page.SetContentAsync("<html><body><input id='foo' disabled><input id='bar'></body></html>");

            await Page.Should().HaveEnabledElementAsync("#bar");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveEnabledElementAsync("#foo"));
            Assert.AreEqual("Expected element \"#foo\" on page to be enabled.", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().HaveEnabledElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveEnabledElementAsync(""));
        }

        // Editable

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveEditableElementAsync_throws_if_element_is_not_required()
        {
            // https://playwright.dev/docs/actionability#editable
            await Page.SetContentAsync("<html><body><input id='foo'><input id='bar' disabled readonly></body></html>");

            await Page.Should().HaveEditableElementAsync("#foo");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveEditableElementAsync("#bar"));
            Assert.AreEqual("Expected element \"#bar\" on page to be editable.", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().HaveEditableElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveEditableElementAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveEditableElementAsync_throws_if_element_is_required()
        {
            await Page.SetContentAsync("<html><body><input id='foo'><input id='bar' disabled readonly></body></html>");

            await Page.Should().NotHaveEditableElementAsync("#bar");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().NotHaveEditableElementAsync("#foo"));
            Assert.AreEqual("Expected element \"#foo\" on page not to be editable.", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().NotHaveEditableElementAsync(".missing"));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().NotHaveEditableElementAsync(""));
        }

        // Attribute

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementAttributeAsync_throws_if_element_does_not_have_the_attribute()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            await Page.Should().HaveElementAttributeAsync("div", "class");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementAttributeAsync("div", "id"));
            Assert.AreEqual("Expected element \"div\" on page to have attribute \"id\".", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().HaveElementAttributeAsync(".missing", ""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveElementAttributeAsync("", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveElementAttributeAsync_throws_if_element_has_the_attribute()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            await Page.Should().NotHaveElementAttributeAsync("div", "id");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().NotHaveElementAttributeAsync("div", "class"));
            Assert.AreEqual("Expected element \"div\" on page not to have attribute \"class\".", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().NotHaveElementAttributeAsync(".missing", ""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().NotHaveElementAttributeAsync("", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementAttributeValueAsync_throws_if_element_does_not_have_the_attribute_value()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            await Page.Should().HaveElementAttributeValueAsync("div", "class", "class");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementAttributeValueAsync("div", "class", "id"));
            Assert.AreEqual("Expected element \"div\" on page to have attribute \"class\" with value \"id\".", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementAttributeValueAsync("div", "id", "class"));
            Assert.AreEqual("Expected element \"div\" on page to have attribute \"id\" with value \"class\".", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().HaveElementAttributeValueAsync(".missing", "", ""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveElementAttributeValueAsync("", "", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveElementAttributeValueAsync_throws_if_element_has_the_attribute_value()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            await Page.Should().NotHaveElementAttributeValueAsync("div", "class", "id");

            await Page.Should().NotHaveElementAttributeValueAsync("div", "id", "class");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().NotHaveElementAttributeValueAsync("div", "class", "class"));
            Assert.AreEqual("Expected element \"div\" on page not to have attribute \"class\" with value \"class\".", ex.Message);

            Assert.ThrowsAsync<TimeoutException>(async () => await Page.Should().NotHaveElementAttributeValueAsync(".missing", "", ""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().NotHaveElementAttributeValueAsync("", "", ""));
        }

        // Element

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementAsync_throws_if_page_does_not_have_the_element()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

            await Page.Should().HaveElementAsync("div");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementAsync(".missing"));
            Assert.AreEqual("Expected page to have element matching \".missing\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveElementAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementCountAsync_throws_if_page_does_not_have_the_element()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

            await Page.Should().HaveElementCountAsync(3, "div");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementCountAsync(1, "div"));
            Assert.AreEqual("Expected page to have 1 element(s) matching \"div\", but found 3.", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementCountAsync(3, ".missing"));
            Assert.AreEqual("Expected page to have 3 element(s) matching \".missing\", but found 0.", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveElementCountAsync(0, ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementWithContentAsync_throws_if_page_does_not_have_the_element()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

            await Page.Should().HaveElementWithContentAsync("div", "Foo");

            await Page.Should().HaveElementWithContentAsync("div", "Ba.");

            await Page.Should().HaveElementWithContentAsync("div", "foo", "i");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementWithContentAsync("div", "Missing", "i"));
            Assert.AreEqual("Expected page to have element matching \"div\" with content \"/Missing/i\".", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementWithContentAsync(".missing", "Foo"));
            Assert.AreEqual("Expected page to have element matching \".missing\" with content \"/Foo/\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveElementWithContentAsync("", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementWithContentCountAsync_throws_if_page_does_not_have_the_element()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");

            await Page.Should().HaveElementWithContentCountAsync(1, "div", "Foo");

            await Page.Should().HaveElementWithContentCountAsync(2, "div", "Ba.");

            await Page.Should().HaveElementWithContentCountAsync(1, "div", "foo", "i");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementWithContentCountAsync(3, "div", "Foo"));
            Assert.AreEqual("Expected page to have 3 element(s) matching \"div\" with content \"/Foo/\", but found 1.", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementWithContentCountAsync(1, "div", "Missing", "i"));
            Assert.AreEqual("Expected page to have 1 element(s) matching \"div\" with content \"/Missing/i\", but found 0.", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await Page.Should().HaveElementWithContentCountAsync(1, ".missing", "Foo"));
            Assert.AreEqual("Expected page to have 1 element(s) matching \".missing\" with content \"/Foo/\", but found 0.", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IPage)null).Should().HaveElementWithContentCountAsync(0, "", ""));
        }
    }
}
