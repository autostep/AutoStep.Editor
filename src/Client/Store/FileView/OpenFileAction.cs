using AutoStep.Editor.Client.Store.App;
using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.FileView
{
    /// <summary>
    /// <see cref="ChangeFileEffect"/>.
    /// </summary>
    internal class OpenFileAction : ICodeWindowAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileAction"/> class.
        /// </summary>
        /// <param name="project">The Project the file is in.</param>
        /// <param name="newFile">The new file.</param>
        public OpenFileAction(Project project, string fileId)
        {
            Project = project;
            FileId = fileId;
        }

        /// <summary>
        /// Gets the Project the file is in.
        /// </summary>
        public Project Project { get; }

        /// <summary>
        /// Gets the new file to switch to.
        /// </summary>
        public string FileId { get; }
    }
}
