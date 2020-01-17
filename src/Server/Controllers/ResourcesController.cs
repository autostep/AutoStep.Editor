using AutoStep.Editor.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        [HttpGet("{sourceName}")]
        public CodeResource Get(string sourceName)
        {
            return new CodeResource
            {
                Name = sourceName,
                Body = TestResources.TestFile
            };
        }
    }
}
