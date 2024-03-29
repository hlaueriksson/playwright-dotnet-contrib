using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightContrib.Extensions
{
    /// <summary>
    /// Extension methods for accessing attributes on <see cref="IElementHandle"/>.
    /// </summary>
    /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes"/>
    public static class ElementHandleAttributeExtensions
    {
        /// <summary>
        /// Id of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>id</c>, or <c>null</c> if the attribute is missing.</returns>
        public static async Task<string?> IdAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().GetAttributeOrDefaultAsync("id").ConfigureAwait(false);

        /// <summary>
        /// Name of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>name</c>, or <c>null</c> if the attribute is missing.</returns>
        /// <remarks><![CDATA[Elements: <button>, <form>, <fieldset>, <iframe>, <input>, <keygen>, <object>, <output>, <select>, <textarea>, <map>, <meta>, <param>]]></remarks>
        public static async Task<string?> NameAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().GetAttributeOrDefaultAsync("name").ConfigureAwait(false);

        /// <summary>
        /// Src of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>src</c>, or <c>null</c> if the attribute is missing.</returns>
        /// <remarks><![CDATA[Elements: <audio>, <embed>, <iframe>, <img>, <input>, <script>, <source>, <track>, <video>]]></remarks>
        public static async Task<string?> SrcAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().GetAttributeOrDefaultAsync("src").ConfigureAwait(false);

        /// <summary>
        /// Value of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>value</c>, or <c>null</c> if the attribute is missing.</returns>
        /// <remarks><![CDATA[Elements: <button>, <option>, <input>, <li>, <meter>, <progress>, <param>]]></remarks>
        [Obsolete("This method has been deprecated. Use the vanilla method IElementHandle.InputValueAsync instead.")]
        public static async Task<string?> ValueAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().GetAttributeOrDefaultAsync("value").ConfigureAwait(false);

        /// <summary>
        /// Href of the element.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <returns>The element's <c>href</c>, or <c>null</c> if the attribute is missing.</returns>
        /// <remarks><![CDATA[Elements: <a>, <area>, <base>, <link>]]></remarks>
        public static async Task<string?> HrefAsync(this IElementHandle elementHandle) =>
            await elementHandle.GuardFromNull().EvaluateAsync<string>("(element) => element.href").ConfigureAwait(false);

        /// <summary>
        /// Returns element attribute value, or a default value if no attribute is found.
        /// </summary>
        /// <param name="elementHandle">An <see cref="IElementHandle"/>.</param>
        /// <param name="name">The attribute name.</param>
        /// <returns>The attribute value, or a default value if no attribute is found.</returns>
        [Obsolete("This method has been deprecated. Use the vanilla method IElementHandle.GetAttributeAsync instead.")]
        public static async Task<string?> GetAttributeOrDefaultAsync(this IElementHandle elementHandle, string name)
        {
            try
            {
                return await elementHandle.GuardFromNull().GetAttributeAsync(name).ConfigureAwait(false);
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }
    }
}
