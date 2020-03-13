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
        public SwitchProjectAction(Project proj)
        {
            NewProject = proj;
        }

        /// <summary>
        /// Gets the project to switch to.
        /// </summary>
        public Project NewProject { get; }
    }
}
