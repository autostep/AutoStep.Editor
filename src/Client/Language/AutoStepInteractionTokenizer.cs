using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoStep.Monaco.Interop;
using AutoStep.Projects;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace AutoStep.Editor.Client.Language
{
    /// <summary>
    /// Call-back endpoint for the TypeScript AutoStepTokenProvider, that in turn calls
    /// the AutoStep line tokeniser to generate a set of tokens. We then convert those into scope tags
    /// before passing them back to the TypeScript.
    /// </summary>
    public class AutoStepInteractionTokenizer : ILanguageTokenizer
    {
        private readonly IProjectCompiler projectCompiler;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoStepInteractionTokenizer"/> class.
        /// </summary>
        /// <param name="projectCompiler">The current project compiler.</param>
        /// <param name="logFactory">The logger factory.</param>
        public AutoStepInteractionTokenizer(IProjectCompiler projectCompiler, ILoggerFactory logFactory)
        {
            this.projectCompiler = projectCompiler;
            this.logger = logFactory.CreateLogger<AutoStepTokenizer>();
        }

        /// <summary>
        /// Retrieves the initial state of the tokeniser.
        /// </summary>
        /// <returns>The initial state.</returns>
        [JSInvokable]
        public int GetInitialState()
        {
            return 0;
        }

        /// <summary>
        /// Tokenise a line.
        /// </summary>
        /// <param name="line">The line of text to tokenise.</param>
        /// <param name="state">The previous state of the tokeniser, as returned by the last call to this method.</param>
        /// <returns>The result of tokenisation.</returns>
        [JSInvokable]
        public LineTokens Tokenize(string line, int state)
        {
            try
            {
                logger.LogTrace(LogMessages.AutoStepTokenizer_TokenizeStart, state, line);

                // Use the project compiler (in the core library) to tokenise.
                var tokenised = projectCompiler.TokeniseInteractionLine(line, state);

                var tokenArray = tokenised.Tokens.Select(x =>
                    new LanguageToken(x.StartPosition, TokenScopes.GetScopeText(x.Category, x.SubCategory)));

                return new LineTokens((int)tokenised.EndState, tokenArray);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, LogMessages.AutoStepTokenizer_TokenizeError);
                throw;
            }
        }
    }
}
