using AutoStep.Editor.Client.Store.App;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Dispatched when a file's content changes.
    /// </summary>
    internal class CodeChangeAction : ICodeWindowAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeChangeAction"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="file">The file.</param>
        /// <param name="body">The new file content.</param>
        public CodeChangeAction(Project project, ProjectFileState file, string body)
        {
            Project = project;
            File = file;
            Body = body;
        }

        /// <summary>
        /// Gets the project the file is in.
        /// </summary>
        public Project Project { get; }

        /// <summary>
        /// Gets the file.
        /// </summary>
        public ProjectFileState File { get; }

        /// <summary>
        /// Gets the new content of the file.
        /// </summary>
        public string Body { get; }
    }
}
