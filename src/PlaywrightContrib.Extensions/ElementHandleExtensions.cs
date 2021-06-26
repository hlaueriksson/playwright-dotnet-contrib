using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Playwright.Contrib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IElementHandle"/>.
    /// </summary>
    public static class ElementHandleExtensions
    {
        /// <summary>
        /// The method runs <c>element.querySelectorAll</c> and then tests a <c>RegExp</c> against the elements <c>textContent</c>. The first element match is returned. If no element matches the selector and regular expression, the return value resolve to <c>null</c>.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/> to query.</param>
        /// <param name="selector">A selector to query element for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns>Task which resolves to an <see cref="IElementHandle"/> pointing to the element.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<IElementHandle?> QuerySelectorWithContentAsync(this IElementHandle elementHandle, string selector, string regex, string flags = "") =>
            await elementHandle.GuardFromNull().EvaluateHandleAsync(
                @"(element, [selector, regex, flags]) => {
                    var elements = element.querySelectorAll(selector);
                    return Array.prototype.find.call(elements, function(element) {
                        return RegExp(regex, flags).test(element.textContent);
                    });
                }",
                new object[] { selector, regex, flags }).ConfigureAwait(false) as IElementHandle;

        /// <summary>
        /// The method runs <c>element.querySelectorAll</c> and then tests a <c>RegExp</c> against the elements <c>textContent</c>. All element matches are returned. If no element matches the selector and regular expression, the return value resolve to an empty <see cref="IReadOnlyList{IElementHandle}"/>.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/> to query.</param>
        /// <param name="selector">A selector to query element for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns>Task which resolves to an <see cref="IReadOnlyList{IElementHandle}"/> pointing to the elements.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<IReadOnlyList<IElementHandle>> QuerySelectorAllWithContentAsync(this IElementHandle elementHandle, string selector, string regex, string flags = "")
        {
            var arrayHandle = await elementHandle.GuardFromNull().EvaluateHandleAsync(
                @"(element, [selector, regex, flags]) => {
                    var elements = element.querySelectorAll(selector);
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
        /// Indicates whether the element exists or not. A non null <see cref="IElementHandle"/> is considered existing.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns><c>true</c> if the element exists.</returns>
        public static bool Exists(this IElementHandle elementHandle) =>
            elementHandle != null;

        /// <summary>
        /// Indicates whether the element has the specified attribute or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <param name="name">The attribute name.</param>
        /// <returns><c>true</c> if the element has the specified attribute.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/Element/hasAttribute"/>
        public static async Task<bool> HasAttributeAsync(this IElementHandle elementHandle, string name) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("(element, name) => element.hasAttribute(name)", name).ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element has the specified attribute value or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns><c>true</c> if the element has the specified attribute value.</returns>
        public static async Task<bool> HasAttributeValueAsync(this IElementHandle elementHandle, string name, string value) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("(element, [name, value]) => element.getAttribute(name) === value", new object[] { name, value }).ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element has the specified value or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the element has the specified value.</returns>
        /// <remarks><![CDATA[Elements: <button>, <option>, <input>, <li>, <meter>, <progress>, <param>]]></remarks>
        public static async Task<bool> HasValueAsync(this IElementHandle elementHandle, string value) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("(element, value) => element.getAttribute('value') === value", value).ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element has the specified class or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <param name="className">The class name.</param>
        /// <returns><c>true</c> if the element has the specified class.</returns>
        public static async Task<bool> HasClassAsync(this IElementHandle elementHandle, string className) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("(element, className) => element.classList.contains(className)", className).ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element has the specified content or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <returns><c>true</c> if the element has the specified content.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public static async Task<bool> HasContentAsync(this IElementHandle elementHandle, string regex, string flags = "") =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("(element, [regex, flags]) => RegExp(regex, flags).test(element.textContent)", new object[] { regex, flags }).ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element has focus or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns><c>true</c> if the element has focus.</returns>
        /// <remarks><![CDATA[Elements: <button>, <input>, <keygen>, <select>, <textarea>]]></remarks>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/DocumentOrShadowRoot/activeElement"/>
        public static async Task<bool> HasFocusAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("element => element === document.activeElement").ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element is read-only or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns><c>true</c> if the element is read-only.</returns>
        /// <remarks><![CDATA[Elements: <input>, <textarea>]]></remarks>
        public static async Task<bool> IsReadOnlyAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("element => element.readOnly").ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element is required or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns><c>true</c> if the element is required.</returns>
        /// <remarks><![CDATA[Elements: <input>, <select>, <textarea>]]></remarks>
        public static async Task<bool> IsRequiredAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("element => element.required").ConfigureAwait(false);

        /// <summary>
        /// Indicates whether the element is selected or not.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns><c>true</c> if the element is selected.</returns>
        /// <remarks><![CDATA[Elements: <option>]]></remarks>
        public static async Task<bool> IsSelectedAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().EvaluateAsync<bool>("element => element.selected").ConfigureAwait(false);

        /// <summary>
        /// ClassName of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>className</c>.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/Element/className"/>
        public static async Task<string> ClassNameAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().EvaluateAsync<string>("element => element.className").ConfigureAwait(false);

        /// <summary>
        /// ClassList of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>classList</c>.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/Element/classList"/>
        public static async Task<IReadOnlyList<string>> ClassListAsync(this IElementHandle elementHandle)
        {
            var jsonElement = await elementHandle.GuardFromNull().EvaluateAsync("element => element.classList").ConfigureAwait(false);
            return
                jsonElement?.EnumerateObject().Select(x => x.Value.GetString()!).Where(x => x != null).ToList().AsReadOnly() ??
                Array.Empty<string>().ToList().AsReadOnly();
        }

        /// <summary>
        /// OuterHTML of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>outerHTML</c>.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/Element/outerHTML"/>
        public static async Task<string> OuterHTMLAsync(this IElementHandle elementHandle) =>
            await elementHandle.GetPropertyValueAsync("outerHTML").ConfigureAwait(false);

        private static async Task<string> GetPropertyValueAsync(this IElementHandle elementHandle, string propertyName)
        {
            var property = await elementHandle.GuardFromNull().GetPropertyAsync(propertyName).ConfigureAwait(false);
            return await property.JsonValueAsync<string>().ConfigureAwait(false);
        }
    }
}
