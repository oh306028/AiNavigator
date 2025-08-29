using AiNavigator.Api.Dtos;
using AiNavigator.Api.Models;
using AiNavigator.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AiNavigator.Api.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ModelsService _modelsService;

        public ModelsController(ModelsService modelsService)
        {
            _modelsService = modelsService;
        }

        [HttpPost]
        public async Task<ActionResult<PromptDetails>> Fetch([FromBody] PromptForm form)    
        {
            var result = await _modelsService.GetModelsAsync(form);
            return Ok(result);
        }

        //[HttpGet("{requestId}/details")]
        //public async Task<ActionResult<PromptDetails>> GetDetails([FromQuery]string requestId)
        //{
        //    var result = await _modelsService.GetDetails(requestId);       
        //    return Ok(result);
        //}

        [HttpGet("history")]
        public async Task<ActionResult<List<RequestGroupDto>>> History()
        {   
            var result = await _modelsService.GetHistory();    
            return Ok(result);
        }
    }
}
