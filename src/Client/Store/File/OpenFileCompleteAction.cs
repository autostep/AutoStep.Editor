using AutoStep.Editor.Client.Store.App;
using AutoStep.Monaco.Interop;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Action that occurs when the content of a file has loaded.
    /// </summary>
    internal class OpenFileCompleteAction : IAutoStepAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileCompleteAction"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public OpenFileCompleteAction(string fileId, TextModel loadedModel)
        {
            FileId = fileId;
            TextModel = loadedModel;
        }

        /// <summary>
        /// Gets the relevant file.
        /// </summary>
        public string FileId { get; }

        public TextModel TextModel { get; }
    }
}
