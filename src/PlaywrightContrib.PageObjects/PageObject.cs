using Microsoft.Playwright;

namespace PlaywrightContrib.PageObjects
{
    /// <summary>
    /// Base class for page objects.
    /// Create page objects by inheriting <see cref="PageObject"/> and declare properties decorated with <see cref="SelectorAttribute"/>.
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// <![CDATA[
    /// public class FooPageObject : PageObject
    /// {
    ///     [Selector("#foo")]
    ///     public virtual Task<IElementHandle> SelectorForElementHandle { get; }
    ///
    ///     [Selector(".bar")]
    ///     public virtual Task<IReadOnlyList<IElementHandle>> SelectorForElementHandleList { get; }
    ///
    ///     [Selector("#foo")]
    ///     public virtual Task<FooElementObject> SelectorForElementObject { get; }
    ///
    ///     [Selector(".bar")]
    ///     public virtual Task<IReadOnlyList<BarElementObject>> SelectorForElementObjectList { get; }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public abstract class PageObject
    {
        /// <summary>
        /// The <c>Playwright</c> page.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IPage Page { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// The response from page navigation.
        /// </summary>
        public IResponse? Response { get; private set; }

        internal void Initialize(IPage page, IResponse? response)
        {
            Page = page;
            Response = response;
        }
    }
}
