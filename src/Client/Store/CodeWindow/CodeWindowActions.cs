using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// <see cref="CodeWindowReducer"/>
    /// </summary>
    public interface ICodeWindowAction
    {
    }

    /// <summary>
    /// <see cref="LoadCodeEffect"/>
    /// </summary>
    public struct LoadCodeAction : ICodeWindowAction
    {
        public string SourceName { get; }

        public LoadCodeAction(string sourceName)
        {
            SourceName = sourceName;
        }
    }
    
    public struct LoadCodeCompleteAction : ICodeWindowAction
    {
        public string Body { get; }

        public LoadCodeCompleteAction(string body)
        {
            Body = body;
        }
    }
}
