using FluentAssertions.Formatting;
using Microsoft.Playwright;
using PlaywrightContrib.Extensions;
using PlaywrightContrib.FluentAssertions.Internal;

namespace PlaywrightContrib.FluentAssertions
{
    /// <summary>
    /// Represents a strategy for formatting an <see cref="IElementHandle"/> into a human-readable string representation.
    /// </summary>
    public class ElementHandleValueFormatter : IValueFormatter
    {
        /// <summary>
        /// Indicates whether the current <see cref="IValueFormatter"/> can handle the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value for which to create a <see cref="string"/>.</param>
        /// <returns><c>true</c> if the current <see cref="IValueFormatter"/> can handle the specified value; otherwise, <c>false</c>.</returns>
        public bool CanHandle(object value)
        {
            return value is IElementHandle;
        }

        /// <summary>
        /// Returns a human-readable representation of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to format into a human-readable representation.</param>
        /// <param name="formattedGraph">An object to write the textual representation to.</param>
        /// <param name="context">Contains additional information that the implementation should take into account.</param>
        /// <param name="formatChild">Allows the formatter to recursively format any child objects.</param>
        public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
        {
            var element = (IElementHandle)value;
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            var html = element.OuterHTMLAsync().Result();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
            var result = $"IElementHandle: {html}";

#pragma warning disable CA1062 // Validate arguments of public methods
            if (context.UseLineBreaks)
            {
                formattedGraph.AddLine(result);
            }
            else
            {
                formattedGraph.AddFragment(result);
            }
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}
