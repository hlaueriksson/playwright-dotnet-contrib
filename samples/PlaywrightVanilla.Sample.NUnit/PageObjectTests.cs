using System.Reflection;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightVanilla.Sample.NUnit
{
    public class PageObjectTests : PageTest
    {
        protected IPageAssertions Expect(PageObject pageObject)
        {
            var page = typeof(PageObject).GetField("Page", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(pageObject);

            return Assertions.Expect((IPage)page);
        }
    }
}
