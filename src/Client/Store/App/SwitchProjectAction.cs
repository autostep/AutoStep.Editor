using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Dispatched when the application is changing project.
    /// </summary>
    internal class SwitchProjectAction : IAutoStepAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchProjectAction"/> class.
        /// </summary>
        /// <param name="proj">The project.</param>
        /// <param name="defaultFile">The default file to load.</param>
        public SwitchProjectAction(Project proj, ProjectFileState defaultFile)
        {
            NewProject = proj;
            DefaultFile = defaultFile;
        }

        /// <summary>
        /// Gets the project to switch to.
        /// </summary>
        public Project NewProject { get; }

        /// <summary>
        /// Gets the default project file (entirely temporary until we have a rendered project structure).
        /// </summary>
        public ProjectFileState DefaultFile { get; }
    }
}
