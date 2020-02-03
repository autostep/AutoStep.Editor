using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Represents the top-level application state.
    /// </summary>
    internal class AppState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppState"/> class.
        /// </summary>
        public AppState()
        {
            CodeWindow = new CodeWindowState(null, false);
            Project = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppState"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="codeWindowState">The code window state.</param>
        public AppState(Project project, CodeWindowState codeWindowState)
        {
            Project = project;
            CodeWindow = codeWindowState;
        }

        /// <summary>
        /// Gets the code window.
        /// </summary>
        public CodeWindowState CodeWindow { get; }

        /// <summary>
        /// Gets the currently active project.
        /// </summary>
        public Project Project { get; }
    }
}
