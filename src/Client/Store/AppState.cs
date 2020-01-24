using AutoStep.Editor.Client.Store.CodeWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store
{
    public class AppState
    {
        public CodeWindowState CodeWindow { get; }

        public AppState()
        {
            CodeWindow = new CodeWindowState();
        }

        public AppState(CodeWindowState codeWindowState)
        {
            CodeWindow = codeWindowState;
        }
    }
}
