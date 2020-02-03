using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Representation of a text model that can be displayed by the Code Editor.
    /// </summary>
    public class TextModel
    {
        private readonly MonacoInterop interop;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextModel"/> class.
        /// </summary>
        /// <param name="uri">The file URI.</param>
        /// <param name="initialValue">Initial value of the model.</param>
        /// <param name="interop">The interop instance.</param>
        internal TextModel(Uri uri, string initialValue, MonacoInterop interop)
        {
            Uri = uri;
            this.interop = interop;
            InitialValue = initialValue;
            CurrentValue = initialValue;
        }

        /// <summary>
        /// Gets the URI of the model.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Gets or sets the language ID of the text model.
        /// </summary>
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets the initial model value (as loaded from the store).
        /// </summary>
        public string InitialValue { get; private set; }

        /// <summary>
        /// Gets the current value of the model.
        /// </summary>
        public string CurrentValue { get; private set; }

        /// <summary>
        /// Gets or sets the OnModelChanged event handler.
        /// </summary>
        public EventHandler<string> OnModelChanged { get; set; }

        /// <summary>
        /// Invoked by JS interop when the content of the model has changed.
        /// </summary>
        /// <param name="newValue">The new content of the model.</param>
        [JSInvokable]
        public void ModelUpdated(string newValue)
        {
            CurrentValue = newValue;

            if (OnModelChanged is object)
            {
                OnModelChanged(this, newValue);
            }
        }

        /// <summary>
        /// Set the markers on the model.
        /// </summary>
        /// <param name="owner">The marker owner name.</param>
        /// <param name="markers">The set of markers.</param>
        /// <returns>Completion task.</returns>
        public async ValueTask SetMarkers(string owner, IEnumerable<MarkerData> markers)
        {
            await interop.SetModelMarkers(this, owner, markers);
        }

        /// <summary>
        /// Sets the content of the text model.
        /// </summary>
        /// <param name="content">The model content.</param>
        /// <returns>Completion task.</returns>
        public async ValueTask SetContent(string content)
        {
            InitialValue = content;
            await interop.SetModelContent(this, content);
            CurrentValue = content;
        }
    }
}
