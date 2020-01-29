namespace AutoStep.Monaco.Interop
{
    public readonly struct LanguageToken
    {
        public int startIndex { get; }
        public string scopes { get; }

        public LanguageToken(int startIndex, string scopes)
        {
            this.startIndex = startIndex;
            this.scopes = scopes;
        }
    }
}
