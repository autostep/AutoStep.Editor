using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoStep.Monaco.Interop
{
    public class TextModel
    {
        private readonly MonacoInterop interop;

        internal TextModel(string uri, string initialValue, MonacoInterop interop)
        {
            Uri = uri;
            this.interop = interop;
            InitialValue = initialValue;
            CurrentValue = initialValue;
        }

        public string Uri { get; }

        public string LanguageId { get; set; }

        public string InitialValue { get; set; }

        public string CurrentValue { get; set; }

        public EventHandler<string> OnModelChanged { get; set; }

        [JSInvokable]
        public void ModelUpdated(string newValue)
        {
            CurrentValue = newValue;

            if (OnModelChanged is object)
            {
                OnModelChanged(this, newValue);
            }
        }

        public async ValueTask SetMarkers(string owner, IEnumerable<MarkerData> markers)
        {
            await interop.SetModelMarkers(this, owner, markers);
        }
    }
}
