using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright.Contrib.PageObjects.DynamicProxy;

namespace Microsoft.Playwright.Contrib.PageObjects
{
    /// <summary>
    /// <see cref="IPage"/> extension methods.
    /// </summary>
    public static class PageExtensions
    {
        // PageObject

        /// <summary>
        /// Returns a <see cref="PageObject"/> from the given <see cref="IPage"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="PageObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <returns>The <see cref="PageObject"/>.</returns>
        public static T To<T>(this IPage page)
            where T : PageObject
        {
            return ProxyFactory.PageObject<T>(page, null);
        }

        /// <summary>
        /// Navigates to an url and returns a <see cref="PageObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="PageObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="url">URL to navigate page to. The url should include scheme, e.g. <c>https://</c>.</param>
        /// <param name="options">Call options.</param>
        /// <returns>Task which resolves to the <see cref="PageObject"/>.</returns>
        /// <seealso cref="IPage.GotoAsync(string, PageGotoOptions)"/>
        public static async Task<T> GotoAsync<T>(this IPage page, string url, PageGotoOptions options = default)
            where T : PageObject
        {
            var response = await page.GuardFromNull().GotoAsync(url, options).ConfigureAwait(false);

            return ProxyFactory.PageObject<T>(page, response);
        }

        /// <summary>
        /// Waits for navigation and returns a <see cref="PageObject"/>.
        /// This resolves when the page navigates to a new URL or reloads.
        /// It is useful for when you run code which will indirectly cause the page to navigate.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="PageObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="options">Call options.</param>
        /// <returns>Task which resolves to the <see cref="PageObject"/>.
        /// In case of multiple redirects, the <see cref="PageObject.Response"/> is the response of the last redirect.
        /// In case of navigation to a different anchor or navigation due to History API usage, the <see cref="PageObject.Response"/> is <c>null</c>.
        /// </returns>
        /// <seealso cref="IPage.WaitForNavigationAsync(PageWaitForNavigationOptions)"/>
        public static async Task<T> WaitForNavigationAsync<T>(this IPage page, PageWaitForNavigationOptions options = default)
            where T : PageObject
        {
            var response = await page.GuardFromNull().WaitForNavigationAsync(options).ConfigureAwait(false);

            return ProxyFactory.PageObject<T>(page, response);
        }

        /// <summary>
        /// Runs <see cref="Func{Task}"/>, waits for navigation and returns a <see cref="PageObject"/>.
        /// This resolves when the page navigates to a new URL or reloads.
        /// It is useful for when you run code which will indirectly cause the page to navigate.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="PageObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="action">Action that triggers the event.</param>
        /// <param name="options">Call options.</param>
        /// <returns>Task which resolves to the <see cref="PageObject"/>.
        /// In case of multiple redirects, the <see cref="PageObject.Response"/> is the response of the last redirect.
        /// In case of navigation to a different anchor or navigation due to History API usage, the <see cref="PageObject.Response"/> is <c>null</c>.
        /// </returns>
        /// <seealso cref="IPage.RunAndWaitForNavigationAsync(Func{Task}, PageRunAndWaitForNavigationOptions)"/>
        public static async Task<T> RunAndWaitForNavigationAsync<T>(this IPage page, Func<Task> action, PageRunAndWaitForNavigationOptions options = default)
            where T : PageObject
        {
            var response = await page.GuardFromNull().RunAndWaitForNavigationAsync(action, options).ConfigureAwait(false);

            return ProxyFactory.PageObject<T>(page, response);
        }

        /// <summary>
        /// Waits for matched response and returns a <see cref="PageObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="PageObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="urlOrPredicate">Request predicate receiving <see cref="IResponse"/> object.</param>
        /// <param name="options">Call options.</param>
        /// <returns>Task which resolves to the <see cref="PageObject"/>.</returns>
        /// <seealso cref="IPage.WaitForResponseAsync(Func{IResponse, bool}, PageWaitForResponseOptions)"/>
        public static async Task<T> WaitForResponseAsync<T>(this IPage page, Func<IResponse, bool> urlOrPredicate, PageWaitForResponseOptions options = default)
            where T : PageObject
        {
            var response = await page.GuardFromNull().WaitForResponseAsync(urlOrPredicate, options).ConfigureAwait(false);

            return ProxyFactory.PageObject<T>(page, response);
        }

        /// <summary>
        /// Runs <see cref="Func{Task}"/>, waits for matched response and returns a <see cref="PageObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="PageObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="action">Action that triggers the event.</param>
        /// <param name="urlOrPredicate">Request predicate receiving <see cref="IResponse"/> object.</param>
        /// <param name="options">Call options.</param>
        /// <returns>Task which resolves to the <see cref="PageObject"/>.</returns>
        /// <seealso cref="IPage.RunAndWaitForResponseAsync(Func{Task}, Func{IResponse, bool}, PageRunAndWaitForResponseOptions)"/>
        public static async Task<T> RunAndWaitForResponseAsync<T>(this IPage page, Func<Task> action, Func<IResponse, bool> urlOrPredicate, PageRunAndWaitForResponseOptions options = default)
            where T : PageObject
        {
            var response = await page.GuardFromNull().RunAndWaitForResponseAsync(action, urlOrPredicate, options).ConfigureAwait(false);

            return ProxyFactory.PageObject<T>(page, response);
        }

        // ElementObject

        /// <summary>
        /// The method finds all elements matching the specified selector within the page and returns a <see cref="IReadOnlyList{ElementObject}"/>.
        /// If no elements match the selector, the return value resolve to an empty list.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <returns>Task which resolves to the <see cref="IReadOnlyList{ElementObject}"/>.</returns>
        /// <seealso cref="IPage.QuerySelectorAllAsync(string)"/>
        public static async Task<IReadOnlyList<T>> QuerySelectorAllAsync<T>(this IPage page, string selector)
            where T : ElementObject
        {
            var results = await page.GuardFromNull().QuerySelectorAllAsync(selector).ConfigureAwait(false);

            return results.Select(ProxyFactory.ElementObject<T>).ToArray();
        }

        /// <summary>
        /// The method finds an element matching the specified selector within the page and returns an <see cref="ElementObject"/>.
        /// If no elements match the selector, the return value resolve to <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <returns>Task which resolves to the <see cref="ElementObject"/>.</returns>
        /// <seealso cref="IPage.QuerySelectorAsync(string)"/>
        public static async Task<T> QuerySelectorAsync<T>(this IPage page, string selector)
            where T : ElementObject
        {
            var result = await page.GuardFromNull().QuerySelectorAsync(selector).ConfigureAwait(false);

            return ProxyFactory.ElementObject<T>(result);
        }

        /// <summary>
        /// Waits for a selector to be added to the DOM and returns an <see cref="ElementObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ElementObject"/>.</typeparam>
        /// <param name="page">A <see cref="IPage"/>.</param>
        /// <param name="selector">A selector of an element to wait for.</param>
        /// <param name="options">Call options.</param>
        /// <returns>A task that resolves to the <see cref="ElementObject"/>, when a element specified by selector string is added to DOM.</returns>
        /// <seealso cref="IPage.WaitForSelectorAsync(string, PageWaitForSelectorOptions)"/>
        public static async Task<T> WaitForSelectorAsync<T>(this IPage page, string selector, PageWaitForSelectorOptions options = default)
            where T : ElementObject
        {
            var result = await page.GuardFromNull().WaitForSelectorAsync(selector, options).ConfigureAwait(false);

            return ProxyFactory.ElementObject<T>(result);
        }

        private static IPage GuardFromNull(this IPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return page;
        }
    }
}
