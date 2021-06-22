namespace Microsoft.Playwright.Contrib.PageObjects
{
    /// <summary>
    /// Base class for element objects.
    /// Create element objects by inheriting <see cref="ElementObject"/> and declare properties decorated with <see cref="SelectorAttribute"/>.
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// <![CDATA[
    /// public class BarElementObject : ElementObject
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
    public abstract class ElementObject
    {
        /// <summary>
        /// The <c>Playwright</c> element handle.
        /// </summary>
        public IElementHandle Element { get; private set; }

        internal void Initialize(IElementHandle element)
        {
            Element = element;
        }
    }
}
