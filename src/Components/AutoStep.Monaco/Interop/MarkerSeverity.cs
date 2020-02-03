namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Implementation of the Monaco MarkerSeverity enum: https://microsoft.github.io/monaco-editor/api/enums/monaco.markerseverity.html.
    /// </summary>
    public enum MarkerSeverity
    {
        /// <summary>
        /// Hint.
        /// </summary>
        Hint = 1,

        /// <summary>
        /// Information.
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 4,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 8,
    }
}
