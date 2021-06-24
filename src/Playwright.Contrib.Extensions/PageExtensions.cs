using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Playwright.Contrib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IPage"/>.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// The method runs <c>document.querySelectorAll</c> within the page and then tests a <c>RegExp</c> against the elements <c>textContent</c>. The first element match is returned. If no element matches the selector and regular expression, the return value resolve to <c>null</c>.
        /// </summary>
        /// <param name="page">An <see cref="IPage"/> to query.</param>
        /// <param name="selector">A selector to query page for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns>Task which resolves to an <see cref="IElementHandle"/> pointing to the frame element.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<IElementHandle?> QuerySelectorWithContentAsync(this IPage page, string selector, string regex, string flags = "") =>
            await page.GuardFromNull().EvaluateHandleAsync(
                @"([selector, regex, flags]) => {
                    var elements = document.querySelectorAll(selector);
                    return Array.prototype.find.call(elements, function(element) {
                        return RegExp(regex, flags).test(element.textContent);
                    });
                }",
                new object[] { selector, regex, flags }).ConfigureAwait(false) as IElementHandle;

        /// <summary>
        /// The method runs <c>document.querySelectorAll</c> within the page and then tests a <c>RegExp</c> against the elements <c>textContent</c>. All element matches are returned. If no element matches the selector and regular expression, the return value resolve to an empty <see cref="IReadOnlyList{IElementHandle}"/>.
        /// </summary>
        /// <param name="page">An <see cref="IPage"/> to query.</param>
        /// <param name="selector">A selector to query page for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns>Task which resolves to an <see cref="IReadOnlyList{IElementHandle}"/> pointing to the frame elements.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<IReadOnlyList<IElementHandle>> QuerySelectorAllWithContentAsync(this IPage page, string selector, string regex, string flags = "")
        {
            var arrayHandle = await page.GuardFromNull().EvaluateHandleAsync(
                @"([selector, regex, flags]) => {
                    var elements = document.querySelectorAll(selector);
                    return Array.prototype.filter.call(elements, function(element) {
                        return RegExp(regex, flags).test(element.textContent);
                    });
                }",
                new object[] { selector, regex, flags }).ConfigureAwait(false);

            var properties = await arrayHandle.GetPropertiesAsync().ConfigureAwait(false);
            await arrayHandle.DisposeAsync().ConfigureAwait(false);

            return properties.Values.OfType<IElementHandle>().ToList().AsReadOnly();
        }

        /// <summary>
        /// Indicates whether the page has the specified content or not.
        /// </summary>
        /// <param name="page">An <see cref="IPage"/>.</param>
        /// <param name="regex">A regular expression to test against <c>document.documentElement.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns><c>true</c> if the page has the specified content.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<bool> HasContentAsync(this IPage page, string regex, string flags = "") =>
            await page.GuardFromNull().EvaluateAsync<bool>("([regex, flags]) => RegExp(regex, flags).test(document.documentElement.textContent)", new object[] { regex, flags }).ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the page has the specified title or not.
        /// </summary>
        /// <param name="page">An <see cref="IPage"/>.</param>
        /// <param name="regex">A regular expression to test against <c>document.title</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns><c>true</c> if the page has the specified title.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<bool> HasTitleAsync(this IPage page, string regex, string flags = "") =>
            await page.GuardFromNull().EvaluateAsync<bool>("([regex, flags]) => RegExp(regex, flags).test(document.title)", new object[] { regex, flags }).ConfigureAwait(false);

        /// <summary>
        /// Returns element attribute value, or a default value if no attribute is found.
        /// </summary>
        /// <param name="page">An <see cref="IPage"/>.</param>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="name">The attribute name.</param>
        /// <returns>The attribute value, or a default value if no attribute is found.</returns>
        public static async Task<string?> GetAttributeOrDefaultAsync(this IPage page, string selector, string name)
        {
            try
            {
                return await page.GuardFromNull().GetAttributeAsync(selector, name).ConfigureAwait(false);
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }
    }
}
