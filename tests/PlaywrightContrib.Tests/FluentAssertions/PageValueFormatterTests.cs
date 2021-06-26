using System.Threading.Tasks;
using FluentAssertions.Formatting;
using Microsoft.Playwright.Contrib.FluentAssertions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Microsoft.Playwright.Contrib.Tests.FluentAssertions
{
    [Parallelizable(ParallelScope.Self)]
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

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public async Task Format_IPage()
        {
            Assert.AreEqual("IPage: about:blank", Subject.Format(Page, new FormattingContext(), null));

            await Page.GotoAsync("https://www.google.com/");
            Assert.AreEqual("IPage: https://www.google.com/", Subject.Format(Page, new FormattingContext(), null));
        }

        [Test, Timeout(TestConstants.DefaultTestTimeout)]
        public void AssertionException_Message()
        {
            var ex = Assert.Throws<AssertionException>(() => Page.Should().BeNull());
            Assert.AreEqual("Expected Page to be <null>, but found IPage: about:blank.", ex.Message);
        }
    }
}
