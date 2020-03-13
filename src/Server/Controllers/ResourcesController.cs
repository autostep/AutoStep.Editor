using AutoStep.Editor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;

namespace AutoStep.Editor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IFileProvider files;

        public ResourcesController(IFileProvider files)
        {
            this.files = files;
        }

        [HttpGet("{**sourceName}")]
        public async Task<CodeResource> Get(string sourceName)
        {
            var file = files.GetFileInfo(Path.Combine("Files", sourceName));

            using var streamReader = new StreamReader(file.CreateReadStream());

            return new CodeResource
            {
                Name = sourceName,
                Body = await streamReader.ReadToEndAsync(),
            };
        }
    }
}
