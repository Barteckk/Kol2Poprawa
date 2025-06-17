using Kol2Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kol2Poprawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _characterService.GetCharacterInfo(id);
            return Ok(character);

        }

        [HttpPost("{id}/backpacks")]
        public async Task<IActionResult> AddBackpack(int id, List<int> items)
        {
            if (items == null || items.Count == 0)
            {
                return BadRequest("Invalid item data provided");
            }

            var result = await _characterService.AddToBackpack(id, items);
            switch (result)
            {
                case -100:
                    return NotFound("No items found with this id");
                case -200:
                    return NotFound("No character found with this id");
                case -300:
                    return BadRequest("Exceeds max weight limit");
                default:
                    return Ok("Items added");
            }
        }
    }
}
