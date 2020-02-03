using AutoStep.Editor.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AutoStep.Editor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        [HttpGet("{**sourceName}")]
        public CodeResource Get(string sourceName)
        {
            return new CodeResource
            {
                Name = sourceName,
                Body = TestResources.TestFile,
            };
        }
    }
}
