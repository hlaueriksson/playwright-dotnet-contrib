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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IElementHandle Element { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        internal void Initialize(IElementHandle element)
        {
            Element = element;
        }
    }
}
