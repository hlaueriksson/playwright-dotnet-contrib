using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright.Contrib.Extensions;

namespace Microsoft.Playwright.Contrib.FluentAssertions
{
    /// <summary>
    /// Contains a number of methods to assert that an <see cref="IPage"/> is in the expected state.
    /// </summary>
    public class PageAssertions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageAssertions"/> class.
        /// </summary>
        /// <param name="subject">The <see cref="IPage"/> to assert.</param>
        public PageAssertions(IPage subject) => Subject = subject;

        /// <summary>
        /// Gets the object which value is being asserted.
        /// </summary>
        public IPage Subject { get; }

        /// <summary>
        /// Asserts that the page has the specified content.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>document.documentElement.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public async Task<AndConstraint<PageAssertions>> HaveContentAsync(string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasContentAsync(regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} to have content {0}{reason}.", $"/{regex}/{flags}");

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the page does not have the specified content.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>document.documentElement.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public async Task<AndConstraint<PageAssertions>> NotHaveContentAsync(string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasContentAsync(regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} not to have content {0}{reason}.", $"/{regex}/{flags}");

            return new AndConstraint<PageAssertions>(this);
        }
    }
}
