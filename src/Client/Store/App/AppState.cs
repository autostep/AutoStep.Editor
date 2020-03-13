using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            FileViews = ImmutableDictionary<Guid, FileViewState>.Empty;
            Files = ImmutableDictionary<string, ProjectFileState>.Empty;
            Project = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppState"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="fileViews">The set of file views.</param>
        public AppState(Project project, ImmutableDictionary<string, ProjectFileState> fileState, ImmutableDictionary<Guid, FileViewState> fileViews)
        {
            FileViews = fileViews;
            Files = fileState;
            Project = project;
        }

        /// <summary>
        /// Gets the code window.
        /// </summary>
        public ImmutableDictionary<Guid, FileViewState> FileViews { get; }

        public ImmutableDictionary<string, ProjectFileState> Files { get; }

        /// <summary>
        /// Gets the currently active project.
        /// </summary>
        public Project Project { get; }
    }
}
