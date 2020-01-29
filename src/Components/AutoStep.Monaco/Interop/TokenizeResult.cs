namespace AutoStep.Monaco.Interop
{
    public class TokenizeResult
    {
        public int endState { get; }
        public LanguageToken[] tokens { get; }

        public TokenizeResult(int endState, LanguageToken[] tokens)
        {
            this.endState = endState;
            this.tokens = tokens;
        }
    }
}
