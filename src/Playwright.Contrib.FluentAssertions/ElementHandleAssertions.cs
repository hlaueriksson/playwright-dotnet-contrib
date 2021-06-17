using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright.Contrib.Extensions;

namespace Microsoft.Playwright.Contrib.FluentAssertions
{
    /// <summary>
    /// Contains a number of methods to assert that an <see cref="IElementHandle"/> is in the expected state.
    /// </summary>
    public class ElementHandleAssertions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementHandleAssertions"/> class.
        /// </summary>
        /// <param name="subject">The <see cref="IElementHandle"/> to assert.</param>
        public ElementHandleAssertions(IElementHandle subject) => Subject = subject;

        /// <summary>
        /// Gets the object which value is being asserted.
        /// </summary>
        public IElementHandle Subject { get; }

        // Exist

        /// <summary>
        /// Asserts that the element exists.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public AndConstraint<ElementHandleAssertions> Exist(string because = "", params object[] becauseArgs)
        {
            var result = Subject.Exists();

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to exist{reason}, but it did not.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not exist.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public AndConstraint<ElementHandleAssertions> NotExist(string because = "", params object[] becauseArgs)
        {
            var result = Subject.Exists();

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to exist{reason}, but it did.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Value

        /// <summary>
        /// Asserts that the element has the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveValueAsync(string value, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.ValueAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result == value)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have value {0}{reason}, but found {1}.", value, result);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> NotHaveValueAsync(string value, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.ValueAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != value)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to have value {0}{reason}.", value);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Attribute

        /// <summary>
        /// Asserts that the element has the specified attribute.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveAttributeAsync(string name, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasAttributeAsync(name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have attribute {0}{reason}.", name);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have the specified attribute.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> NotHaveAttributeAsync(string name, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasAttributeAsync(name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to have attribute {0}{reason}.", name);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the specified attribute value.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveAttributeValueAsync(string name, string value, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GetAttributeOrDefaultAsync(name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result == value)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have attribute {0} with value {1}{reason}.", name, value);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have the specified attribute value.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> NotHaveAttributeValueAsync(string name, string value, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GetAttributeOrDefaultAsync(name).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != value)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to have attribute {0} with value {1}{reason}.", name, value);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Content

        /// <summary>
        /// Asserts that the element has the specified content.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveContentAsync(string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasContentAsync(regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have content {0}{reason}.", $"/{regex}/{flags}");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have the specified content.
        /// </summary>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/RegExp"/>
        public async Task<AndConstraint<ElementHandleAssertions>> NotHaveContentAsync(string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasContentAsync(regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to have content {0}{reason}.", $"/{regex}/{flags}");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Class

        /// <summary>
        /// Asserts that the element has the specified class.
        /// </summary>
        /// <param name="className">The class name.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveClassAsync(string className, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasClassAsync(className).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have class {0}{reason}.", className);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have the specified class.
        /// </summary>
        /// <param name="className">The class name.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> NotHaveClassAsync(string className, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasClassAsync(className).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to have class {0}{reason}.", className);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Visible

        /// <summary>
        /// Asserts that the element is visible.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> BeVisibleAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsVisibleAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be visible{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is hidden.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> BeHiddenAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsHiddenAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be hidden{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Selected

        /// <summary>
        /// Asserts that the element is selected.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <option>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> BeSelectedAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.IsSelectedAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be selected{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is not selected.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <option>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> NotBeSelectedAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.IsSelectedAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to be selected{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Checked

        /// <summary>
        /// Asserts that the element is checked.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <command>, <input>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> BeCheckedAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsCheckedAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be checked{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is not checked.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <command>, <input>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> NotBeCheckedAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsCheckedAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to be checked{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Disabled

        /// <summary>
        /// Asserts that the element is disabled.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <button>, <command>, <fieldset>, <input>, <keygen>, <optgroup>, <option>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> BeDisabledAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsDisabledAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be disabled{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is enabled.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <button>, <command>, <fieldset>, <input>, <keygen>, <optgroup>, <option>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> BeEnabledAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsEnabledAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be enabled{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // ReadOnly

        /// <summary>
        /// Asserts that the element is read-only.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <input>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> BeReadOnlyAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.IsReadOnlyAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be read-only{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is not read-only.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <input>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> NotBeReadOnlyAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.IsReadOnlyAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to be read-only{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Required

        /// <summary>
        /// Asserts that the element is required.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <input>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> BeRequiredAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.IsRequiredAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be required{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is not required.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <input>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> NotBeRequiredAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.IsRequiredAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to be required{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Focus

        /// <summary>
        /// Asserts that the element has focus.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <button>, <input>, <keygen>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveFocusAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasFocusAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have focus{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have focus.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        /// <remarks><![CDATA[Elements: <button>, <input>, <keygen>, <select>, <textarea>]]></remarks>
        public async Task<AndConstraint<ElementHandleAssertions>> NotHaveFocusAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.HasFocusAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to have focus{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Editable

        /// <summary>
        /// Asserts that the element is editable.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> BeEditableAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsEditableAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to be editable{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is not editable.
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> NotBeEditableAsync(string because = "", params object[] becauseArgs)
        {
            var result = await Subject.GuardFromNull().IsEditableAsync().ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(!result)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} not to be editable{reason}.");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        // Element

        /// <summary>
        /// Asserts that the element has the specified selector.
        /// </summary>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveElementAsync(string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.QuerySelectorAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have element matching {0}{reason}.", selector);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the expected number of elements matching the specified selector.
        /// </summary>
        /// <param name="count">The expected number of elements matching the specified selector.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveElementCountAsync(int count, string selector, string because = "", params object[] becauseArgs)
        {
            var result = await Subject.QuerySelectorAllAsync(selector).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result.Count == count)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have {0} element(s) matching {1}{reason}, but found {2}.", count, selector, result.Count);

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the specified selector with the specified content.
        /// </summary>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveElementWithContentAsync(string selector, string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.QuerySelectorWithContentAsync(selector, regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result != null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have element matching {0} with content {1}{reason}.", selector, $"/{regex}/{flags}");

            return new AndConstraint<ElementHandleAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the expected number of elements matching the specified selector with the specified content.
        /// </summary>
        /// <param name="count">The expected number of elements matching the specified selector.</param>
        /// <param name="selector">A selector to query for.</param>
        /// <param name="regex">A regular expression to test against <c>element.textContent</c>.</param>
        /// <param name="flags">A set of flags for the regular expression.</param>
        /// <param name="because">A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because" />.</param>
        /// <returns>An <see cref="AndConstraint{ElementHandleAssertions}"/> which can be used to chain assertions.</returns>
        public async Task<AndConstraint<ElementHandleAssertions>> HaveElementWithContentCountAsync(int count, string selector, string regex, string flags = "", string because = "", params object[] becauseArgs)
        {
            var result = await Subject.QuerySelectorAllWithContentAsync(selector, regex, flags).ConfigureAwait(false);

            Execute.Assertion
                .ForCondition(result.Count == count)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:element} to have {0} element(s) matching {1} with content {2}{reason}, but found {3}.", count, selector, $"/{regex}/{flags}", result.Count);

            return new AndConstraint<ElementHandleAssertions>(this);
        }
    }
}
