using InlämningSalonn.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatGPT _chatGPTService;

        public ChatController(IChatGPT chatGPTService)
        {
            _chatGPTService = chatGPTService;
        }

        [HttpPost("ChatGPT")]
        public async Task<IActionResult> Post([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message) || string.IsNullOrWhiteSpace(request.Language))
            {
                return BadRequest("Meddelande och språk krävs.");
            }

            var response = await _chatGPTService.GetChatGPTResponse(request.Message, request.Language);
            return Ok(response);
           
        }
    }

}

public class ChatRequest
{
    public string Message { get; set; }
    public string Language { get; set; }
}
