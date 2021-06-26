using System.Threading.Tasks;
using FluentAssertions.Formatting;
using Microsoft.Playwright.Contrib.FluentAssertions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.FluentAssertions
{
    [Parallelizable(ParallelScope.Self)]
    public class ElementHandleValueFormatterTests : PageTest
    {
        private ElementHandleValueFormatter Subject { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            await Page.SetContentAsync("<html><head></head><body></body></html>");

            Subject = new ElementHandleValueFormatter();
            Formatter.AddFormatter(Subject);
        }

        [TearDown]
        public void TearDown()
        {
            Formatter.RemoveFormatter(Subject);
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task CanHandle_IElementHandle()
        {
            var element = await Page.QuerySelectorAsync("html");
            Assert.IsTrue(Subject.CanHandle(element));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Format_IElementHandle()
        {
            var element = await Page.QuerySelectorAsync("html");
            Assert.AreEqual("IElementHandle: <html><head></head><body></body></html>", Subject.Format(element, new FormattingContext(), null));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task AssertionException_Message()
        {
            var element = await Page.QuerySelectorAsync("html");
            var ex = Assert.Throws<AssertionException>(() => element.Should().BeNull());
            Assert.AreEqual("Expected element to be <null>, but found IElementHandle: <html><head></head><body></body></html>.", ex.Message);
        }
    }
}
