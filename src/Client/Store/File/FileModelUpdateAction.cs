using AutoStep.Editor.Client.Store.App;
using AutoStep.Monaco.Interop;
using System;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    internal class FileModelUpdateAction : IAutoStepAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileModelUpdateAction"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public FileModelUpdateAction(string fileId, DateTime newUpdatedStamp)
        {
            FileId = fileId;
            UpdatedTime = newUpdatedStamp;
        }

        /// <summary>
        /// Gets the relevant file.
        /// </summary>
        public string FileId { get; }

        public DateTime UpdatedTime { get; }
    }
}
