using AutoStep.Editor.Client.Store.App;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// <see cref="ChangeFileEffect"/>.
    /// </summary>
    internal class ChangeFileAction : ICodeWindowAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeFileAction"/> class.
        /// </summary>
        /// <param name="project">The Project the file is in.</param>
        /// <param name="newFile">The new file.</param>
        public ChangeFileAction(Project project, ProjectFileState newFile)
        {
            Project = project;
            NewFile = newFile;
        }

        /// <summary>
        /// Gets the Project the file is in.
        /// </summary>
        public Project Project { get; }

        /// <summary>
        /// Gets the new file to switch to.
        /// </summary>
        public ProjectFileState NewFile { get; }
    }
}
