using AutoStep.Editor.Client.Store.App;
using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store
{
    public class ProjectCompiledAction : IAutoStepAction, ICodeWindowAction
    {
        public Project Project { get; }

        public ProjectCompiledAction(Project project)
        {
            Project = project;
        }
    }
}
