namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Defines an interface for a provider of language tokens.
    /// </summary>
    public interface ILanguageTokenizer
    {
        /// <summary>
        /// Get the initial state of the tokenizer.
        /// </summary>
        /// <returns>The initial state.</returns>
        int GetInitialState();

        /// <summary>
        /// Tokenizes a line.
        /// </summary>
        /// <param name="line">The line text.</param>
        /// <param name="state">The last state of the tokenizer, as per the last call to this method.</param>
        /// <returns>The result of tokenization.</returns>
        LineTokens Tokenize(string line, int state);
    }
}
