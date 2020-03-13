using AutoStep.Editor.Client.Store.App;
using System;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Represents the current state of a single code window.
    /// </summary>
    internal class FileViewState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileViewState"/> class.
        /// </summary>
        public FileViewState(Guid viewGuid, string fileId)
        {
            Id = viewGuid;
            FileId = fileId;
        }

        public Guid Id { get; }

        /// <summary>
        /// Gets the currently displayed project file.
        /// </summary>
        public string FileId { get; }
    }
}
