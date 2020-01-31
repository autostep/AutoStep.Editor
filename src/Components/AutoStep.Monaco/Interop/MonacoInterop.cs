using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoStep.Monaco.Interop
{
    public class MonacoInterop
    {
        private readonly IJSRuntime jsRuntime;
        private readonly ILogger logger;

        private const string InteropPrefix = "monacoInterop.";

        public MonacoInterop(IJSRuntime jsRuntime, ILoggerFactory logFactory)
        {
            this.jsRuntime = jsRuntime;
            this.logger = logFactory.CreateLogger<MonacoInterop>();
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

        public async ValueTask SetModelContent(TextModel model, string newContent)
        {
            await InvokeVoidAsync("setModelContent", model.Uri, newContent);
        }

        public ValueTask<TResult> InvokeAsync<TResult>(string methodName, params object[] args)
        {
            var fullname = InteropPrefix + methodName;
            logger.LogDebug("InvokeAsync: {0}", fullname);
            return jsRuntime.InvokeAsync<TResult>(fullname, args);
        }

        public ValueTask InvokeVoidAsync(string methodName, params object[] args)
        {
            var fullname = InteropPrefix + methodName;
            logger.LogDebug("InvokeVoidAsync: {0}", fullname);
            return jsRuntime.InvokeVoidAsync(fullname, args);
        }
    }
}

