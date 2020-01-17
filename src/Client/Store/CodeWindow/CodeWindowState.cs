using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class CodeWindowState
    {        
        public string CodeBody { get; }

        public string SourceName { get; }

        public bool IsLoading { get; }

        public CodeWindowState()
        {
        }

        public CodeWindowState(string codeBody, string sourceName, bool isLoading)
        {
            CodeBody = codeBody;
            SourceName = sourceName;
            IsLoading = isLoading;
        }
    }
}
