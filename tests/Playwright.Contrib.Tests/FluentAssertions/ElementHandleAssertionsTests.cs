using System;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.FluentAssertions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.FluentAssertions
{
    [Parallelizable(ParallelScope.Self)]
    public class ElementHandleAssertionsTests : PageTest
    {
        [SetUp]
        public async Task SetUp() => await Page.SetContentAsync("<html><body><div class='tweet'><div class='like'>100</div><div class='retweets'>10</div></div></body></html>");

        // Exist

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_Exist_throws_if_element_is_missing()
        {
            var tweet = await Page.QuerySelectorAsync(".tweet");
            tweet.Should().Exist();

            var missing = await Page.QuerySelectorAsync(".missing");
            var ex = Assert.Throws<AssertionException>(() => missing.Should().Exist());
            Assert.AreEqual("Expected element to exist, but it did not.", ex.Message);

            Assert.Throws<AssertionException>(() => ((IElementHandle)null).Should().Exist());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotExist_throws_if_element_is_present()
        {
            var missing = await Page.QuerySelectorAsync(".missing");
            missing.Should().NotExist();

            var tweet = await Page.QuerySelectorAsync(".tweet");
            var ex = Assert.Throws<AssertionException>(() => tweet.Should().NotExist());
            Assert.AreEqual("Expected element not to exist, but it did.", ex.Message);

            ((IElementHandle)null).Should().NotExist();
        }

        // Value

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveValueAsync_throws_if_element_does_not_have_the_value()
        {
            await Page.SetContentAsync("<html><body><input value='input' /><button value='button' /></body></html>");

            var input = await Page.QuerySelectorAsync("input");
            await input.Should().HaveValueAsync("input");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await input.Should().HaveValueAsync("button"));
            Assert.AreEqual("Expected element to have value \"button\", but found \"input\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().HaveValueAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveValueAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveValueAsync_throws_if_element_has_the_value()
        {
            await Page.SetContentAsync("<html><body><input value='input' /><button value='button' /></body></html>");

            var input = await Page.QuerySelectorAsync("input");
            await input.Should().NotHaveValueAsync("button");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await input.Should().NotHaveValueAsync("input"));
            Assert.AreEqual("Expected element not to have value \"input\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotHaveValueAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotHaveValueAsync(""));
        }

        // Attribute

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveAttributeAsync_throws_if_element_does_not_have_the_attribute()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().HaveAttributeAsync("class");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().HaveAttributeAsync("id"));
            Assert.AreEqual("Expected element to have attribute \"id\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().HaveAttributeAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveAttributeAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveAttributeAsync_throws_if_element_has_the_attribute()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().NotHaveAttributeAsync("id");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotHaveAttributeAsync("class"));
            Assert.AreEqual("Expected element not to have attribute \"class\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotHaveAttributeAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotHaveAttributeAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveAttributeValueAsync_throws_if_element_does_not_have_the_attribute_value()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().HaveAttributeValueAsync("class", "class");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().HaveAttributeValueAsync("class", "id"));
            Assert.AreEqual("Expected element to have attribute \"class\" with value \"id\", but found \"class\".", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().HaveAttributeValueAsync("id", "class"));
            Assert.AreEqual("Expected element to have attribute \"id\" with value \"class\", but found <null>.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().HaveAttributeValueAsync("", ""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveAttributeValueAsync("", ""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveAttributeValueAsync_throws_if_element_has_the_attribute_value()
        {
            await Page.SetContentAsync("<html><body><div class='class' data-foo='bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().NotHaveAttributeValueAsync("id", "class");

            await div.Should().NotHaveAttributeValueAsync("class", "id");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotHaveAttributeValueAsync("class", "class"));
            Assert.AreEqual("Expected element not to have attribute \"class\" with value \"class\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotHaveAttributeValueAsync("", ""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotHaveAttributeValueAsync("", ""));
        }

        // Content

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveContentAsync_throws_if_element_does_not_have_the_content()
        {
            var like = await Page.QuerySelectorAsync(".like");
            await like.Should().HaveContentAsync("100");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await like.Should().HaveContentAsync("200", "i"));
            Assert.AreEqual("Expected element to have content \"/200/i\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().HaveContentAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveContentAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveContentAsync_throws_if_element_has_the_content()
        {
            var like = await Page.QuerySelectorAsync(".like");
            await like.Should().NotHaveContentAsync("200");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await like.Should().NotHaveContentAsync("100", "i"));
            Assert.AreEqual("Expected element not to have content \"/100/i\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotHaveContentAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotHaveContentAsync(""));
        }

        // Class

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveClassAsync_throws_if_element_does_not_have_the_class()
        {
            await Page.SetContentAsync("<html><body><div class='foo bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().HaveClassAsync("foo");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().HaveClassAsync("baz"));
            Assert.AreEqual("Expected element to have class \"baz\", but found \"foo bar\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().HaveClassAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveClassAsync(""));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveClassAsync_throws_if_element_has_the_class()
        {
            await Page.SetContentAsync("<html><body><div class='foo bar' /></body></html>");

            var div = await Page.QuerySelectorAsync("div");
            await div.Should().NotHaveClassAsync("baz");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotHaveClassAsync("foo"));
            Assert.AreEqual("Expected element not to have class \"foo\".", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotHaveClassAsync(""));

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotHaveClassAsync(""));
        }

        // Visible

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeVisibleAsync_throws_if_element_is_hidden()
        {
            await Page.SetContentAsync("<html><body><div id='foo'>Foo</div><div id='bar' style='display:none'>Bar</div></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeVisibleAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeVisibleAsync());
            Assert.AreEqual("Expected element to be visible.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeVisibleAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeVisibleAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeHiddenAsync_throws_if_element_is_visible()
        {
            await Page.SetContentAsync("<html><body><div id='foo'>Foo</div><div id='bar' style='display:none'>Bar</div></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().BeHiddenAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeHiddenAsync());
            Assert.AreEqual("Expected element to be hidden.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeHiddenAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeHiddenAsync());
        }

        // Selected

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeSelectedAsync_throws_if_element_is_not_selected()
        {
            await Page.SetContentAsync("<html><body><select><option id='foo'>Foo</option><option id='bar'>Bar</option></select></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeSelectedAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeSelectedAsync());
            Assert.AreEqual("Expected element to be selected.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeSelectedAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeSelectedAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotBeSelectedAsync_throws_if_element_is_selected()
        {
            await Page.SetContentAsync("<html><body><select><option id='foo'>Foo</option><option id='bar'>Bar</option></select></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().NotBeSelectedAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotBeSelectedAsync());
            Assert.AreEqual("Expected element not to be selected.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotBeSelectedAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotBeSelectedAsync());
        }

        // Checked

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeCheckedAsync_throws_if_element_is_not_checked()
        {
            await Page.SetContentAsync("<html><body><input type='checkbox' id='foo' checked><input type='checkbox' id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeCheckedAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeCheckedAsync());
            Assert.AreEqual("Expected element to be checked.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeCheckedAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeCheckedAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotBeCheckedAsync_throws_if_element_is_checked()
        {
            await Page.SetContentAsync("<html><body><input type='checkbox' id='foo' checked><input type='checkbox' id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().NotBeCheckedAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotBeCheckedAsync());
            Assert.AreEqual("Expected element not to be checked.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotBeCheckedAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotBeCheckedAsync());
        }

        // Disabled

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeDisabledAsync_throws_if_element_is_enabled()
        {
            await Page.SetContentAsync("<html><body><input id='foo' disabled><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeDisabledAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeDisabledAsync());
            Assert.AreEqual("Expected element to be disabled.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeDisabledAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeDisabledAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeEnabledAsync_throws_if_element_is_disabled()
        {
            await Page.SetContentAsync("<html><body><input id='foo' disabled><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().BeEnabledAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeEnabledAsync());
            Assert.AreEqual("Expected element to be enabled.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeEnabledAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeEnabledAsync());
        }

        // ReadOnly

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeReadOnlyAsync_throws_if_element_is_not_read_only()
        {
            await Page.SetContentAsync("<html><body><input id='foo' readonly><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeReadOnlyAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeReadOnlyAsync());
            Assert.AreEqual("Expected element to be read-only.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeReadOnlyAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeReadOnlyAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotBeReadOnlyAsync_throws_if_element_is_read_only()
        {
            await Page.SetContentAsync("<html><body><input id='foo' readonly><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().NotBeReadOnlyAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotBeReadOnlyAsync());
            Assert.AreEqual("Expected element not to be read-only.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotBeReadOnlyAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotBeReadOnlyAsync());
        }

        // Required

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeRequiredAsync_throws_if_element_is_not_required()
        {
            await Page.SetContentAsync("<html><body><input id='foo' required><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeRequiredAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeRequiredAsync());
            Assert.AreEqual("Expected element to be required.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeRequiredAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeRequiredAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotBeRequiredAsync_throws_if_element_is_required()
        {
            await Page.SetContentAsync("<html><body><input id='foo' required><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().NotBeRequiredAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotBeRequiredAsync());
            Assert.AreEqual("Expected element not to be required.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotBeRequiredAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotBeRequiredAsync());
        }

        // Focus

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveFocusAsync_throws_if_element_does_not_have_focus()
        {
            await Page.SetContentAsync("<html><body><input id='foo' autofocus><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().HaveFocusAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().HaveFocusAsync());
            Assert.AreEqual("Expected element to have focus.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().HaveFocusAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveFocusAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotHaveFocusAsync_throws_if_element_has_focus()
        {
            await Page.SetContentAsync("<html><body><input id='foo' autofocus><input id='bar'></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().NotHaveFocusAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotHaveFocusAsync());
            Assert.AreEqual("Expected element not to have focus.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotHaveFocusAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotHaveFocusAsync());
        }

        // Editable

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_BeEditableAsync_throws_if_element_is_not_required()
        {
            // https://playwright.dev/docs/actionability#editable
            await Page.SetContentAsync("<html><body><input id='foo'><input id='bar' disabled readonly></body></html>");

            var div = await Page.QuerySelectorAsync("#foo");
            await div.Should().BeEditableAsync();

            div = await Page.QuerySelectorAsync("#bar");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().BeEditableAsync());
            Assert.AreEqual("Expected element to be editable.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().BeEditableAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().BeEditableAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_NotBeEditableAsync_throws_if_element_is_required()
        {
            await Page.SetContentAsync("<html><body><input id='foo'><input id='bar' disabled readonly></body></html>");

            var div = await Page.QuerySelectorAsync("#bar");
            await div.Should().NotBeEditableAsync();

            div = await Page.QuerySelectorAsync("#foo");
            var ex = Assert.ThrowsAsync<AssertionException>(async () => await div.Should().NotBeEditableAsync());
            Assert.AreEqual("Expected element not to be editable.", ex.Message);

            var missing = await Page.QuerySelectorAsync(".missing");
            Assert.ThrowsAsync<ArgumentNullException>(async () => await missing.Should().NotBeEditableAsync());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().NotBeEditableAsync());
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Should_HaveElementAsync_throws_if_page_does_not_have_the_element()
        {
            await Page.SetContentAsync(@"
<html>
  <div id='foo'>Foo</div>
  <div id='bar'>Bar</div>
  <div id='baz'>Baz</div>
</html>");
            var element = await Page.QuerySelectorAsync("html");
            
            await element.Should().HaveElementAsync("div");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementAsync(".missing"));
            Assert.AreEqual("Expected element to have element matching \".missing\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveElementAsync(""));
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
            var element = await Page.QuerySelectorAsync("html");

            await element.Should().HaveElementCountAsync(3, "div");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementCountAsync(1, "div"));
            Assert.AreEqual("Expected element to have 1 element(s) matching \"div\", but found 3.", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementCountAsync(3, ".missing"));
            Assert.AreEqual("Expected element to have 3 element(s) matching \".missing\", but found 0.", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveElementCountAsync(0, ""));
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
            var element = await Page.QuerySelectorAsync("html");

            await element.Should().HaveElementWithContentAsync("div", "Foo");

            await element.Should().HaveElementWithContentAsync("div", "Ba.");

            await element.Should().HaveElementWithContentAsync("div", "foo", "i");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementWithContentAsync("div", "Missing", "i"));
            Assert.AreEqual("Expected element to have element matching \"div\" with content \"/Missing/i\".", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementWithContentAsync(".missing", "Foo"));
            Assert.AreEqual("Expected element to have element matching \".missing\" with content \"/Foo/\".", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveElementWithContentAsync("", ""));
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
            var element = await Page.QuerySelectorAsync("html");

            await element.Should().HaveElementWithContentCountAsync(1, "div", "Foo");

            await element.Should().HaveElementWithContentCountAsync(2, "div", "Ba.");

            await element.Should().HaveElementWithContentCountAsync(1, "div", "foo", "i");

            var ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementWithContentCountAsync(3, "div", "Foo"));
            Assert.AreEqual("Expected element to have 3 element(s) matching \"div\" with content \"/Foo/\", but found 1.", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementWithContentCountAsync(1, "div", "Missing", "i"));
            Assert.AreEqual("Expected element to have 1 element(s) matching \"div\" with content \"/Missing/i\", but found 0.", ex.Message);

            ex = Assert.ThrowsAsync<AssertionException>(async () => await element.Should().HaveElementWithContentCountAsync(1, ".missing", "Foo"));
            Assert.AreEqual("Expected element to have 1 element(s) matching \".missing\" with content \"/Foo/\", but found 0.", ex.Message);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await ((IElementHandle)null).Should().HaveElementWithContentCountAsync(0, "", ""));
        }
    }
}
