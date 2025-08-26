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
        public async Task<ActionResult<PromptDetails>> Get([FromBody] PromptForm form)
        {
            var result = await _modelsService.GetModelsAsync(form);
            return Ok(result);
        }
    }
}
