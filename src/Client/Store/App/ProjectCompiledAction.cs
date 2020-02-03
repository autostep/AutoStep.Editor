using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Action occurs when a project has been compiled.
    /// </summary>
    internal class ProjectCompiledAction : IAutoStepAction, ICodeWindowAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCompiledAction"/> class.
        /// </summary>
        /// <param name="project">The relevant project.</param>
        public ProjectCompiledAction(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public Project Project { get; }
    }
}
