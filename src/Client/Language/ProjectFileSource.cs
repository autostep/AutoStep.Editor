using AutoStep.Compiler;
using AutoStep.Editor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Language
{
    public class ProjectFileSource : IContentSource
    {
        private DateTime lastModify;
        private readonly HttpClient httpClient;
        private readonly string remoteReference;

        public ProjectFileSource(HttpClient httpClient, string remoteReference)
        {
            this.httpClient = httpClient;
            this.remoteReference = remoteReference;
            lastModify = DateTime.UtcNow;
        }

        public string LocalFileBody { get; private set; }

        public string OriginalBody { get; private set; }

        public void UpdateLocalBody(string body)
        {
            LocalFileBody = body;
            lastModify = DateTime.UtcNow;
        }

        public string SourceName => remoteReference;

        public async ValueTask<string> GetContentAsync(CancellationToken cancelToken = default)
        {
            // Either use the local file or the remote one.
            return LocalFileBody ?? (OriginalBody = (await httpClient.GetJsonAsync<CodeResource>($"api/resources/{remoteReference}")).Body);
        }

        public DateTime GetLastContentModifyTime()
        {
            return lastModify;
        }
    }
}
