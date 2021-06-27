using System;
using FluentAssertions.Formatting;
using Microsoft.Playwright;

namespace PlaywrightContrib.FluentAssertions
{
    /// <summary>
    /// Represents a strategy for formatting an <see cref="IPage"/> into a human-readable string representation.
    /// </summary>
    public class PageValueFormatter : IValueFormatter
    {
        /// <summary>
        /// Indicates whether the current <see cref="IValueFormatter"/> can handle the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value for which to create a <see cref="string"/>.</param>
        /// <returns><c>true</c> if the current <see cref="IValueFormatter"/> can handle the specified value; otherwise, <c>false</c>.</returns>
        public bool CanHandle(object value)
        {
            return value is IPage;
        }

        /// <summary>
        /// Returns a human-readable representation of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value for which to format.</param>
        /// <param name="context">Contains additional information about the formatting task.</param>
        /// <param name="formatChild">Allows the formatter to recursively format any child objects.</param>
        /// <returns>A human-readable representation of <paramref name="value"/>.</returns>
        public string Format(object value, FormattingContext context, FormatChild formatChild)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            var newline = context.UseLineBreaks ? Environment.NewLine : string.Empty;
            var padding = new string('\t', context.Depth);

            var page = (IPage)value;
            return $"{newline}{padding}IPage: {page.Url}";
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}
