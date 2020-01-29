using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoStep.Monaco.Interop
{
    public class CodeEditor
    {
        private readonly MonacoInterop interop;

        public string Id { get; }

        public CodeEditor(string id, MonacoInterop interop)
        {
            Id = id;
            this.interop = interop;
        }

        public async ValueTask SetModel(TextModel model)
        {
            await interop.InvokeVoidAsync("setEditorModel", Id, model.Uri);
        }
    }
}
