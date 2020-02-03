using AutoStep.Editor.Client.Store.App;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Action that occurs when the content of a file has loaded.
    /// </summary>
    internal class LoadCodeCompleteAction : ICodeWindowAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCodeCompleteAction"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public LoadCodeCompleteAction(ProjectFileState file)
        {
            File = file;
        }

        /// <summary>
        /// Gets the relevant file.
        /// </summary>
        public ProjectFileState File { get; }
    }
}
