using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store
{
    public interface IAutoStepAction
    {
    }


    public class AppState
    {
        public CodeWindowState CodeWindow { get; }

        public Project Project { get; }

        public AppState()
        {
            CodeWindow = new CodeWindowState(null, false);                        
            Project = null;
        }

        public AppState(Project project, CodeWindowState codeWindowState)
        {
            Project = project;
            CodeWindow = codeWindowState;
        }
    }
}
