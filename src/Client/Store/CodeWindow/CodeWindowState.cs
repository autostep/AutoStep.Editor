using AutoStep.Editor.Client.Store.App;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Represents the current state of a single code window.
    /// </summary>
    internal class CodeWindowState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeWindowState"/> class.
        /// </summary>
        public CodeWindowState()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeWindowState"/> class.
        /// </summary>
        /// <param name="file">The project file.</param>
        /// <param name="isLoading">Currently loading.</param>
        public CodeWindowState(ProjectFileState file, bool isLoading)
        {
            DisplayedFile = file;
            IsLoading = isLoading;
        }

        /// <summary>
        /// Gets the currently displayed project file.
        /// </summary>
        public ProjectFileState DisplayedFile { get; }

        /// <summary>
        /// Gets a value indicating whether content is currently loading.
        /// </summary>
        public bool IsLoading { get; }
    }
}
