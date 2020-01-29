using AutoStep.Compiler;
using AutoStep.Editor.Client.Language;
using AutoStep.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class CodeWindowState
    {   
        public ProjectFileState DisplayedFile { get; }

        public bool IsLoading { get; }

        public CodeWindowState()
        {
        }

        public CodeWindowState(ProjectFileState file, bool isLoading)
        {
            DisplayedFile = file;
            IsLoading = isLoading;
        }
    }
}
