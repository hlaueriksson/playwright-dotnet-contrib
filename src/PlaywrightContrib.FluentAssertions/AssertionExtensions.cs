using Microsoft.Playwright;

namespace PlaywrightContrib.FluentAssertions
{
    /// <summary>
    /// Contains extension methods for custom assertions in unit tests.
    /// </summary>
    public static class AssertionExtensions
    {
        /// <summary>
        /// Returns an <see cref="PageAssertions"/> object that can be used to assert the current <see cref="IPage"/>.
        /// </summary>
        /// <param name="actualValue">An <see cref="IPage"/>.</param>
        /// <returns>An <see cref="PageAssertions"/> object.</returns>
        public static PageAssertions Should(this IPage actualValue)
        {
            return new PageAssertions(actualValue);
        }

        /// <summary>
        /// Returns an <see cref="ElementHandleAssertions"/> object that can be used to assert the current <see cref="IElementHandle"/>.
        /// </summary>
        /// <param name="actualValue">An <see cref="IElementHandle"/>.</param>
        /// <returns>An <see cref="ElementHandleAssertions"/> object.</returns>
        public static ElementHandleAssertions Should(this IElementHandle actualValue)
        {
            return new ElementHandleAssertions(actualValue);
        }
    }
}
