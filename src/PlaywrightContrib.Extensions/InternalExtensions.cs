using System;
using Microsoft.Playwright;

namespace PlaywrightContrib.Extensions
{
    internal static class InternalExtensions
    {
        internal static IPage GuardFromNull(this IPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return page;
        }

        internal static IElementHandle GuardFromNull(this IElementHandle elementHandle)
        {
            if (elementHandle == null)
            {
                throw new ArgumentNullException(nameof(elementHandle));
            }

            return elementHandle;
        }
    }
}
