using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.Playwright;
using Microsoft.Playwright.Contrib.Extensions;
using PlaywrightContrib.FluentAssertions.Internal;

namespace PlaywrightContrib.FluentAssertions
{
    /// <summary>
    /// Contains a number of methods to assert that an <see cref="IPage"/> is in the expected state.
    /// </summary>
    public class PageAssertions : ReferenceTypeAssertions<IPage, PageAssertions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageAssertions"/> class.
        /// </summary>
        /// <param name="subject">The <see cref="IPage"/> to assert.</param>
        public PageAssertions(IPage subject) => Subject = subject;

        /// <summary>
        /// Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Identifier => nameof(IPage);

        // Content

        /// <summary>
        /// Asserts that the page has the specified content.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>document.documentElement.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
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
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
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

        // Title

        /// <summary>
        /// Asserts that the page has the specified title.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>document.title</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public async Task<AndConstraint<PageAssertions>> HaveTitleAsync(string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasTitleAsync(regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} to have title {0}{reason}, but found {1}.", $"/{regex}/{flags}", await Subject.TitleAsync().ConfigureAwait(false));

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the page does not have the specified title.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>document.title</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public async Task<AndConstraint<PageAssertions>> NotHaveTitleAsync(string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasTitleAsync(regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} not to have title {0}{reason}.", $"/{regex}/{flags}");

            return new AndConstraint<PageAssertions>(this);
        }

        // Visible

        /// <summary>
        /// Asserts that the element on page is visible.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveVisibleElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsVisibleAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to be visible{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page is hidden.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveHiddenElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsHiddenAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to be hidden{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        // Checked

        /// <summary>
        /// Asserts that the element on page is checked.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <command>, <input>]]></remarks>
        public async Task<AndConstraint<PageAssertions>> HaveCheckedElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsCheckedAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to be checked{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page is not checked.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <command>, <input>]]></remarks>
        public async Task<AndConstraint<PageAssertions>> NotHaveCheckedElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsCheckedAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} not to be checked{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        // Disabled

        /// <summary>
        /// Asserts that the element on page is disabled.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <button>, <command>, <fieldset>, <input>, <keygen>, <optgroup>, <option>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<PageAssertions>> HaveDisabledElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsDisabledAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to be disabled{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page is enabled.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <button>, <command>, <fieldset>, <input>, <keygen>, <optgroup>, <option>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<PageAssertions>> HaveEnabledElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsEnabledAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to be enabled{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        // Editable

        /// <summary>
        /// Asserts that the element on page is editable.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveEditableElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsEditableAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to be editable{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page is not editable.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> NotHaveEditableElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsEditableAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} not to be editable{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        // Attribute

        /// <summary>
        /// Asserts that the element on page has the specified attribute.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveElementAttributeAsync(string selector, string name, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GetAttributeOrDefaultAsync(selector, name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to have attribute {1}{reason}.", selector, name);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page does not have the specified attribute.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> NotHaveElementAttributeAsync(string selector, string name, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GetAttributeOrDefaultAsync(selector, name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result == null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} not to have attribute {1}{reason}.", selector, name);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page has the specified attribute value.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveElementAttributeValueAsync(string selector, string name, string value, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GetAttributeOrDefaultAsync(selector, name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result == value)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} to have attribute {1} with value {2}{reason}, but found {3}.", selector, name, value, result);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element on page does not have the specified attribute.
        /// </summary>
        /// <param name="selector">A selector to search for element. If there are multiple elements satisfying the selector, the first will be used.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> NotHaveElementAttributeValueAsync(string selector, string name, string value, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GetAttributeOrDefaultAsync(selector, name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != value)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected element {0} on {context:page} not to have attribute {1} with value {2}{reason}.", selector, name, value);

            return new AndConstraint<PageAssertions>(this);
        }

        // Element

        /// <summary>
        /// Asserts that the page has the specified selector.
        /// </summary>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().QuerySelectorAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} to have element matching {0}{reason}.", selector);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the page has the expected number of elements matching the specified selector.
        /// </summary>
        /// <param name="count">The expected number of elements matching the specified selector.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveElementCountAsync(int count, string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().QuerySelectorAllAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result.Count == count)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} to have {0} element(s) matching {1}{reason}, but found {2}.", count, selector, result.Count);

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the page has the specified selector with the specified content.
        /// </summary>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveElementWithContentAsync(string selector, string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.QuerySelectorWithContentAsync(selector, regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} to have element matching {0} with content {1}{reason}.", selector, $"/{regex}/{flags}");

            return new AndConstraint<PageAssertions>(this);
        }

        /// <summary>
        /// Asserts that the page has the expected number of elements matching the specified selector with the specified content.
        /// </summary>
        /// <param name="count">The expected number of elements matching the specified selector.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])"/> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
        /// <returns>An <see cref="AndConstraint{PageAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<PageAssertions>> HaveElementWithContentCountAsync(int count, string selector, string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.QuerySelectorAllWithContentAsync(selector, regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result.Count == count)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:page} to have {0} element(s) matching {1} with content {2}{reason}, but found {3}.", count, selector, $"/{regex}/{flags}", result.Count);

            return new AndConstraint<PageAssertions>(this);
        }
    }
}
