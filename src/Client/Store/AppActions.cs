using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store
{
    public class SwitchProjectAction : IAutoStepAction
    { 
        public Project NewProject { get; }

        /// <summary>
        /// Temporary
        /// </summary>
        public ProjectFileState DefaultFile { get; }

        public SwitchProjectAction(Project proj, ProjectFileState defaultFile)
        {
            NewProject = proj;
            DefaultFile = defaultFile;
        }
    }
}
