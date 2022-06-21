using System.Threading.Tasks;
using FluentAssertions.Formatting;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.FluentAssertions;

namespace PlaywrightContrib.Tests.FluentAssertions
{
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

        [Test]
        public async Task CanHandle_IElementHandle()
        {
            var element = await Page.QuerySelectorAsync("html");
            Assert.IsTrue(Subject.CanHandle(element));
        }

        [Test]
        public async Task Format_IElementHandle()
        {
            var element = await Page.QuerySelectorAsync("html");
            var formattedGraph = new FormattedObjectGraph(1);
            Subject.Format(element, formattedGraph, new FormattingContext(), null);
            Assert.AreEqual("IElementHandle: <html><head></head><body></body></html>", formattedGraph.ToString());
        }

        [Test]
        public async Task AssertionException_Message()
        {
            var element = await Page.QuerySelectorAsync("html");
            var ex = Assert.Throws<AssertionException>(() => element.Should().BeNull());
            Assert.AreEqual("Expected element to be <null>, but found IElementHandle: <html><head></head><body></body></html>.", ex.Message);
        }
    }
}
