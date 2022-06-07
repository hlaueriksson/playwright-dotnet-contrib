using System.Threading.Tasks;
using FluentAssertions.Formatting;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightContrib.FluentAssertions;

namespace PlaywrightContrib.Tests.FluentAssertions
{
    public class PageValueFormatterTests : PageTest
    {
        private PageValueFormatter Subject { get; set; }

        [SetUp]
        public void SetUp()
        {
            Subject = new PageValueFormatter();
            Formatter.AddFormatter(Subject);
        }

        [TearDown]
        public void TearDown()
        {
            Formatter.RemoveFormatter(Subject);
        }

        [Test]
        public void CanHandle_IPage()
        {
            Assert.IsTrue(Subject.CanHandle(Page));
        }

        [Test]
        public async Task Format_IPage()
        {
            var formattedGraph = new FormattedObjectGraph(1);
            Subject.Format(Page, formattedGraph, new FormattingContext(), null);
            Assert.AreEqual("IPage: about:blank", formattedGraph.ToString());

            await Page.GotoAsync("https://www.google.com/");
            formattedGraph = new FormattedObjectGraph(1);
            Subject.Format(Page, formattedGraph, new FormattingContext(), null);
            Assert.AreEqual("IPage: https://www.google.com/", formattedGraph.ToString());
        }

        [Test]
        public void AssertionException_Message()
        {
            var ex = Assert.Throws<AssertionException>(() => Page.Should().BeNull());
            Assert.AreEqual("Expected Page to be <null>, but found IPage: about:blank.", ex.Message);
        }
    }
}
