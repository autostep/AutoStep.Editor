using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoStep.Monaco.Interop
{
    public class MonacoInterop
    {
        private readonly IJSRuntime jsRuntime;

        private const string InteropPrefix = "monacoInterop.";

        public MonacoInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async ValueTask<CodeEditor> CreateEditor(ElementReference element)
        {
            var editorId = Guid.NewGuid().ToString();

            var monacoEditor = new CodeEditor(editorId, this);

            await InvokeVoidAsync("createEditor", editorId, element, DotNetObjectReference.Create(monacoEditor));

            return monacoEditor;
        }

        public async ValueTask<TextModel> CreateTextModel(string uri, string value, string languageId = null)
        {
            var textModel = new TextModel(uri, value, this)
            {
                LanguageId = languageId
            };

            await InvokeVoidAsync("createTextModel", textModel.Uri, textModel.InitialValue, DotNetObjectReference.Create(textModel), textModel.LanguageId);

            return textModel;
        }

        public async ValueTask RegisterLanguageTokenizer(string languageId, string extension, ILanguageTokenizer tokenizer)
        {
            await InvokeVoidAsync("registerLanguage", languageId, extension, DotNetObjectReference.Create(tokenizer));
        }

        public async ValueTask SetModelMarkers(TextModel model, string owner, IEnumerable<MarkerData> markers)
        {
            await InvokeVoidAsync("setModelMarkers", model.Uri, owner, markers);
        }

        public ValueTask<TResult> InvokeAsync<TResult>(string methodName, params object[] args)
        {
            return jsRuntime.InvokeAsync<TResult>(InteropPrefix + methodName, args);
        }

        public ValueTask InvokeVoidAsync(string methodName, params object[] args)
        {
            return jsRuntime.InvokeVoidAsync(InteropPrefix + methodName, args);
        }
    }
}

