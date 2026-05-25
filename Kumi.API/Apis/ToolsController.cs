using System;
using Kumi.API.Application.Tools;
using Kumi.API.Application.Tools.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Kumi.API.Apis 
{
    public class ToolsController(ToolService toolService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ToolDto>>> GetAllTools()
        {
            return HandleResult(await toolService.GetAllTools());
        }

        [HttpPost]
        public async Task<ActionResult<ToolDto>> AddTool(ToolDto toolDto)
        {
            return HandleResult(await toolService.AddTool(toolDto));
        }

        [HttpDelete("{toolId}")]
        public async Task<ActionResult<Unit>> DeleteTool(string toolId)
        {
            return HandleResult(await toolService.DeleteTool(toolId));
        }
    }
}
