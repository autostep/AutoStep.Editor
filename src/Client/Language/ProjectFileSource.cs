using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoStep.Compiler;
using AutoStep.Editor.Shared;
using Microsoft.AspNetCore.Components;

namespace AutoStep.Editor.Client.Language
{
    /// <summary>
    /// Defines an AutoStep content source that can load a remote resource,
    /// and maintain local state for files currently being edited.
    /// </summary>
    public class ProjectFileSource : IContentSource
    {
        private readonly HttpClient httpClient;
        private readonly string remoteReference;
        private DateTime lastModify;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFileSource"/> class.
        /// </summary>
        /// <param name="httpClient">An HTTP Client Instance.</param>
        /// <param name="remoteReference">The remote reference for the file.</param>
        public ProjectFileSource(HttpClient httpClient, string remoteReference)
        {
            this.httpClient = httpClient;
            this.remoteReference = remoteReference;
            lastModify = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the local file content (containing any local modifications).
        /// </summary>
        public string LocalFileBody { get; private set; }

        /// <summary>
        /// Gets the original file content last loaded from the server.
        /// </summary>
        public string OriginalBody { get; private set; }

        /// <summary>
        /// Gets the Source Name of the file.
        /// </summary>
        public string SourceName => remoteReference;

        /// <summary>
        /// Update the local body of the file (usually after a change in the code editor).
        /// </summary>
        /// <param name="body">The new local file content.</param>
        public void UpdateLocalBody(string body)
        {
            LocalFileBody = body;
            lastModify = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public async ValueTask<string> GetContentAsync(CancellationToken cancelToken = default)
        {
            // Either use the local file or the remote one.
            return LocalFileBody ?? (OriginalBody = (await httpClient.GetJsonAsync<CodeResource>($"api/resources/{remoteReference}")).Body);
        }

        /// <inheritdoc/>
        public DateTime GetLastContentModifyTime()
        {
            return lastModify;
        }
    }
}
