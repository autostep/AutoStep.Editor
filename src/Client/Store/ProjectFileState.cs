using AutoStep.Editor.Client.Language;
using AutoStep.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store
{
    public class ProjectFileState
    {
        public ProjectFile File { get; }

        public ProjectFileSource Source { get; }

        public Uri FileUri { get; }

        public ProjectFileState(Uri fileUri, ProjectFile file, ProjectFileSource source)
        {
            File = file;
            Source = source;
            FileUri = fileUri;
        }
    }
}
