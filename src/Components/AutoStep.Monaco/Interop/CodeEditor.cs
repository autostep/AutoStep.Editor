using System.Threading.Tasks;

namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Represents a Monaco Code Editor instance on the C# Side.
    /// Calling methods on this object will effect the corresponding editor.
    /// </summary>
    internal class CodeEditor
    {
        private readonly MonacoInterop interop;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeEditor"/> class.
        /// </summary>
        /// <param name="id">The editor ID.</param>
        /// <param name="interop">The shared monaco interop wrapper.</param>
        public CodeEditor(string id, MonacoInterop interop)
        {
            Id = id;
            this.interop = interop;
        }

        /// <summary>
        /// Gets the generated id of the editor.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Changes the model being displayed in the editor
        /// (effectively like changing the file).
        /// </summary>
        /// <param name="model">The text model to change to.</param>
        /// <returns>A completion task.</returns>
        public async ValueTask SetModel(TextModel model)
        {
            await interop.InvokeVoidAsync("setEditorModel", Id, model.Uri);
        }
    }
}
