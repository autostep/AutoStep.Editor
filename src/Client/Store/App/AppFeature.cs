using Blazor.Fluxor;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Top-level Application Feature.
    /// </summary>
    internal class AppFeature : Feature<AppState>
    {
        /// <inheritdoc/>
        public override string GetName() => "App";

        /// <inheritdoc/>
        protected override AppState GetInitialState() => new AppState();
    }
}
