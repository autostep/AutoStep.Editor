namespace AutoStep.Monaco.Interop
{
    public class MarkerData
    {
        public MarkerData(string code, string message, MarkerSeverity severity, int startColumn, int startLineNumber, int endColumn, int endLineNumber)
        {
            this.code = code;
            this.message = message;
            this.severity = severity;
            this.startColumn = startColumn;
            this.startLineNumber = startLineNumber;
            this.endColumn = endColumn;
            this.endLineNumber = endLineNumber;
        }

        public string code { get; }

        public string message { get; }

        public MarkerSeverity severity { get; }

        public int startColumn { get; }

        public int startLineNumber { get; }

        public int endColumn { get; }

        public int endLineNumber { get; }
    }
}
