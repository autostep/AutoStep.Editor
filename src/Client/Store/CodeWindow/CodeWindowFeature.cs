using Blazor.Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class CodeWindowFeature : Feature<CodeWindowState>
    {
        public override string GetName() => "CodeWindow";

        protected override CodeWindowState GetInitialState() => new CodeWindowState();
    }
}
