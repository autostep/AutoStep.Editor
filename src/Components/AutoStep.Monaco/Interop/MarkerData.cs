using System.Diagnostics.CodeAnalysis;

namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Implementation of the Monaco IMarkerData interface, https://microsoft.github.io/monaco-editor/api/interfaces/monaco.editor.imarkerdata.html.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Mapping to JS Object")]
    public class MarkerData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerData"/> class.
        /// </summary>
        /// <param name="code">The message code.</param>
        /// <param name="message">The body of the message.</param>
        /// <param name="severity">Severity level.</param>
        /// <param name="startColumn">The start column number.</param>
        /// <param name="startLineNumber">The start line number.</param>
        /// <param name="endColumn">The end column number.</param>
        /// <param name="endLineNumber">The end line number.</param>
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

        /// <summary>
        /// Gets the marker code.
        /// </summary>
        public string code { get; }

        /// <summary>
        /// Gets the message contnet.
        /// </summary>
        public string message { get; }

        /// <summary>
        /// Gets the marker severity.
        /// </summary>
        public MarkerSeverity severity { get; }

        /// <summary>
        /// Gets the start column.
        /// </summary>
        public int startColumn { get; }

        /// <summary>
        /// Gets the start line number.
        /// </summary>
        public int startLineNumber { get; }

        /// <summary>
        /// Gets the end column.
        /// </summary>
        public int endColumn { get; }

        /// <summary>
        /// Gets the end line number.
        /// </summary>
        public int endLineNumber { get; }
    }
}
