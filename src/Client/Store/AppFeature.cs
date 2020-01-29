﻿using AutoStep.Editor.Client.Language;
using Blazor.Fluxor;

namespace AutoStep.Editor.Client.Store
{
    public class AppFeature : Feature<AppState>
    {
        public override string GetName() => "App";

        protected override AppState GetInitialState() => new AppState();
    }
}
