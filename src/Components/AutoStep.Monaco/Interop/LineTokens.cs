using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Implementation of the Monaco ILineTokens interface, https://microsoft.github.io/monaco-editor/api/interfaces/monaco.languages.ilinetokens.html.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Mapping to JS Object")]
    public class LineTokens
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineTokens"/> class.
        /// </summary>
        /// <param name="endState">The end state of the tokenize operation.</param>
        /// <param name="tokens">The set of tokens.</param>
        public LineTokens(int endState, IEnumerable<LanguageToken> tokens)
        {
            this.endState = endState;
            this.tokens = tokens;
        }

        /// <summary>
        /// Gets the end state of the operation.
        /// </summary>
        public int endState { get; }

        /// <summary>
        /// Gets the set of tokens.
        /// </summary>
        public IEnumerable<LanguageToken> tokens { get; }
    }
}
