using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightContrib.PageObjects
{
    /// <summary>
    /// Represents a selector for a property on a <see cref="PageObject"/> or <see cref="ElementObject"/>.
    ///
    /// Properties decorated with a <see cref="SelectorAttribute"/> must be a:
    /// <list type="bullet">
    /// <item><description>public</description></item>
    /// <item><description>virtual</description></item>
    /// <item><description>getter</description></item>
    /// </list>
    /// that returns a <see cref="Task{TResult}"/> of:
    /// <list type="bullet">
    /// <item><description><see cref="IElementHandle"/>,</description></item>
    /// <item><description><see cref="IReadOnlyList{IElementHandle}"/>,</description></item>
    /// <item><description><see cref="ElementObject"/> or</description></item>
    /// <item><description><see cref="IReadOnlyList{ElementObject}"/></description></item>
    /// </list>
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// <![CDATA[
    /// [Selector("#foo")]
    /// public virtual Task<IElementHandle> SelectorForElementHandle { get; }
    ///
    /// [Selector(".bar")]
    /// public virtual Task<IReadOnlyList<IElementHandle>> SelectorForElementHandleList { get; }
    ///
    /// [Selector("#foo")]
    /// public virtual Task<FooElementObject> SelectorForElementObject { get; }
    ///
    /// [Selector(".bar")]
    /// public virtual Task<IReadOnlyList<BarElementObject>> SelectorForElementObjectList { get; }
    /// ]]>
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SelectorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorAttribute"/> class.
        /// </summary>
        /// <param name="selector">A selector to query a <see cref="IPage"/> or <see cref="IElementHandle"/> for.</param>
        public SelectorAttribute(string selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// A selector to query a <see cref="IPage"/> or <see cref="IElementHandle"/> for.
        /// </summary>
        public string Selector { get; }
    }
}
