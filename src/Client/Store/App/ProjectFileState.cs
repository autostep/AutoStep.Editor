using System;
using AutoStep.Editor.Client.Language;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Encapsulates the relevant state of a single file.
    /// </summary>
    public class ProjectFileState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFileState"/> class.
        /// </summary>
        /// <param name="fileUri">The URI of the file.</param>
        /// <param name="file">The file.</param>
        /// <param name="source">The file source.</param>
        public ProjectFileState(Uri fileUri, ProjectFile file, ProjectFileSource source)
        {
            File = file;
            Source = source;
            FileUri = fileUri;
        }

        /// <summary>
        /// Gets the AutoStep Project File.
        /// </summary>
        public ProjectFile File { get; }

        /// <summary>
        /// Gets the Project File Source (allows us to update the content of the file).
        /// </summary>
        public ProjectFileSource Source { get; }

        /// <summary>
        /// Gets the URI for the file.
        /// </summary>
        public Uri FileUri { get; }
    }
}
