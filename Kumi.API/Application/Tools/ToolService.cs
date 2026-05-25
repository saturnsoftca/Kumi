using System;
using Kumi.Domain.Tools;
using Kumi.Core.Tools.Interfaces;
using Kumi.API.Application.Tools.Mappings;
using Kumi.API.Application.Tools.Dtos;

namespace Kumi.API.Application.Tools
{
    
    public class ToolService(IToolCommandActions toolCommandActions,
                             IToolQueryActions toolQueryActions,
                             ToolMapper toolMapper)
    {
        
        public async Task<Result<List<ToolDto>>> GetAllTools()
        {
            return Result<List<ToolDto>>.Success(
                toolMapper.ToDtoList(await toolQueryActions.ListAllTools())
            );
        }
        
        public async Task<Result<ToolDto>> AddTool(ToolDto tool) 
        {
            return Result<ToolDto>.Success(
                toolMapper.ToDto(await toolCommandActions.AddTool(toolMapper.ToEntity(tool)))
            );
        }

        public async Task<Result<Unit>> DeleteTool(string toolId)
        {
            Tool? tool = await toolQueryActions.FindTool(Guid.Parse(toolId));
            await toolCommandActions.DeleteTool(tool!);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
