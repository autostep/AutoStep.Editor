using System.Collections.Generic;

namespace AutoStep.Monaco.Interop
{
    public class TokenizeResult
    {
        public int endState { get; }
        public IEnumerable<LanguageToken> tokens { get; }

        public TokenizeResult(int endState, IEnumerable<LanguageToken> tokens)
        {
            this.endState = endState;
            this.tokens = tokens;
        }
    }
}
