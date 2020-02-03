using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Main interop class for communicating with the TypeScript components.
    /// </summary>
    public class MonacoInterop
    {
        private const string InteropPrefix = "monacoInterop.";

        private readonly IJSRuntime jsRuntime;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonacoInterop"/> class.
        /// </summary>
        /// <param name="jsRuntime">The JS runtime.</param>
        /// <param name="logFactory">The logger factory.</param>
        public MonacoInterop(IJSRuntime jsRuntime, ILoggerFactory logFactory)
        {
            this.jsRuntime = jsRuntime;
            this.logger = logFactory.CreateLogger<MonacoInterop>();
        }

        /// <summary>
        /// Create a new Monaco Code Editor, inside the specified HTML element.
        /// </summary>
        /// <param name="element">The HTML element.</param>
        /// <returns>A code editor.</returns>
        public async ValueTask<CodeEditor> CreateEditor(ElementReference element)
        {
            var editorId = Guid.NewGuid().ToString();

            var monacoEditor = new CodeEditor(editorId, this);

            await InvokeVoidAsync("createEditor", editorId, element, DotNetObjectReference.Create(monacoEditor));

            return monacoEditor;
        }

        /// <summary>
        /// Create a new text model (representing a file).
        /// </summary>
        /// <param name="uri">The URI of the model.</param>
        /// <param name="value">The value of the model (i.e. content of the file).</param>
        /// <param name="languageId">The language ID for the file.</param>
        /// <returns>A text model.</returns>
        public async ValueTask<TextModel> CreateTextModel(Uri uri, string value, string languageId = null)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var textModel = new TextModel(uri, value, this)
            {
                LanguageId = languageId,
            };

            await InvokeVoidAsync("createTextModel", textModel.Uri.ToString(), textModel.InitialValue, DotNetObjectReference.Create(textModel), textModel.LanguageId);

            return textModel;
        }

        /// <summary>
        /// Register a language tokenizer with Monaco.
        /// </summary>
        /// <param name="languageId">The ID of the language this tokenizer tokenizes.</param>
        /// <param name="extension">A file extension for the language.</param>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>Completion task.</returns>
        public async ValueTask RegisterLanguageTokenizer(string languageId, string extension, ILanguageTokenizer tokenizer)
        {
            await InvokeVoidAsync("registerLanguageTokenizer", languageId, extension, DotNetObjectReference.Create(tokenizer));
        }

        /// <summary>
        /// Set the model markers for a specific text model.
        /// </summary>
        /// <param name="model">The text model.</param>
        /// <param name="owner">The named owner of the markers.</param>
        /// <param name="markers">The markers.</param>
        /// <returns>Completion task.</returns>
        public async ValueTask SetModelMarkers(TextModel model, string owner, IEnumerable<MarkerData> markers)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await InvokeVoidAsync("setModelMarkers", model.Uri.ToString(), owner, markers);
        }

        /// <summary>
        /// Sets the content of a given model.
        /// </summary>
        /// <param name="model">The text model to update.</param>
        /// <param name="newContent">The new content of the file.</param>
        /// <returns>Completion task.</returns>
        public async ValueTask SetModelContent(TextModel model, string newContent)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await InvokeVoidAsync("setModelContent", model.Uri.ToString(), newContent);
        }

        /// <summary>
        /// Invoke a method in the monaco typescript layer.
        /// </summary>
        /// <typeparam name="TResult">Result of the method call.</typeparam>
        /// <param name="methodName">The name of the method in the MonacoInterop TypeScript class.</param>
        /// <param name="args">Arguments to the method.</param>
        /// <returns>The result.</returns>
        public ValueTask<TResult> InvokeAsync<TResult>(string methodName, params object[] args)
        {
            var fullname = InteropPrefix + methodName;
            logger.LogDebug(LogMessages.MonacoInterop_InvokeAsync, fullname);
            return jsRuntime.InvokeAsync<TResult>(fullname, args);
        }

        /// <summary>
        /// Invoke a method in the monaco typescript layer.
        /// </summary>
        /// <param name="methodName">The name of the method in the MonacoInterop TypeScript class.</param>
        /// <param name="args">Arguments to the method.</param>
        /// <returns>Completion task.</returns>
        public ValueTask InvokeVoidAsync(string methodName, params object[] args)
        {
            var fullname = InteropPrefix + methodName;
            logger.LogDebug(LogMessages.MonacoInterop_InvokeAsyncVoid, fullname);
            return jsRuntime.InvokeVoidAsync(fullname, args);
        }
    }
}