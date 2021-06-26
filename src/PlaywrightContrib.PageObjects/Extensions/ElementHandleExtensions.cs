using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightContrib.PageObjects.DynamicProxy;

namespace PlaywrightContrib.PageObjects
{
    /// <summary>
    /// <see cref="IElementHandle"/> extension methods.
    /// </summary>
    public static class ElementHandleExtensions
    {
        /// <summary>
        /// Returns a <see cref="ElementObject"/> from the given <see cref="IElementHandle"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="elementHandle">A <see cref="IElementHandle"/>.</param>
        /// <returns>The <see cref="ElementObject"/>.</returns>
        public static T? To<T>(this IElementHandle elementHandle)
            where T : ElementObject
        {
            return ProxyFactory.ElementObject<T>(elementHandle);
        }

        /// <summary>
        /// Returns a <see cref="IReadOnlyList{ElementObject}"/> from the given <see cref="IReadOnlyList{IElementHandle}"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="elementHandles">A <see cref="IReadOnlyList{IElementHandle}"/>.</param>
        /// <returns>The <see cref="IReadOnlyList{ElementObject}"/>.</returns>
        public static IReadOnlyList<T> To<T>(this IReadOnlyList<IElementHandle> elementHandles)
            where T : ElementObject
        {
            return elementHandles.Select(ProxyFactory.ElementObject<T>).ToArray()!;
        }

        /// <summary>
        /// The method finds all elements matching the specified selector and returns a <see cref="IReadOnlyList{ElementObject}"/>.
        /// If no elements match the selector, the return value resolve to an empty list.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="elementHandle">A <see cref="IElementHandle"/>.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <returns>Task which resolves to the <see cref="IReadOnlyList{ElementObject}"/>.</returns>
        /// <seealso cref="IElementHandle.QuerySelectorAllAsync(string)"/>
        public static async Task<IReadOnlyList<T>> QuerySelectorAllAsync<T>(this IElementHandle elementHandle, string selector)
            where T : ElementObject
        {
            var results = await elementHandle.GuardFromNull().QuerySelectorAllAsync(selector).ConfigureAwait(false);

            return results.Select(ProxyFactory.ElementObject<T>).ToArray()!;
        }

        /// <summary>
        /// The method finds an element matching the specified selector and returns an <see cref="ElementObject"/>.
        /// If no elements match the selector, the return value resolve to <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="elementHandle">A <see cref="IElementHandle"/>.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <returns>Task which resolves to the <see cref="ElementObject"/>.</returns>
        /// <seealso cref="IElementHandle.QuerySelectorAsync(string)"/>
        public static async Task<T?> QuerySelectorAsync<T>(this IElementHandle elementHandle, string selector)
            where T : ElementObject
        {
            var result = await elementHandle.GuardFromNull().QuerySelectorAsync(selector).ConfigureAwait(false);

            return ProxyFactory.ElementObject<T>(result);
        }

        /// <summary>
        /// Waits for a selector to be added to the DOM and returns an <see cref="ElementObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="elementHandle">A <see cref="IElementHandle"/>.</param>
        /// <param name="selector">A selector of an element to wait for.</param>
        /// <param name="options">Optional waiting parameters.</param>
        /// <returns>A task that resolves to the <see cref="ElementObject"/>, when a element specified by selector string is added to DOM.</returns>
        /// <seealso cref="IElementHandle.WaitForSelectorAsync(string, ElementHandleWaitForSelectorOptions)"/>
        public static async Task<T?> WaitForSelectorAsync<T>(this IElementHandle elementHandle, string selector, ElementHandleWaitForSelectorOptions? options = default)
            where T : ElementObject
        {
            var result = await elementHandle.GuardFromNull().WaitForSelectorAsync(selector, options).ConfigureAwait(false);

            return ProxyFactory.ElementObject<T>(result);
        }

        private static IElementHandle GuardFromNull(this IElementHandle elementHandle)
        {
            if (elementHandle == null)
            {
                throw new ArgumentNullException(nameof(elementHandle));
            }

            return elementHandle;
        }
    }
}
