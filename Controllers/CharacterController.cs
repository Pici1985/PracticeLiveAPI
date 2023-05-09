using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Interfaces;


namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        internal readonly ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [Route("/characters")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetAllCharacters()    
        {
            return Ok(_characterRepository.GetAll());
        }
        
        [Route("/characters/{id}")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetCharacter(int id)    
        {
            var character = _characterRepository.GetByID(id);

            if (character != null) 
            { 
                return Ok(character);
            }
            return NotFound(new { Message = "Character not found!" });
        }
        
        [Route("/characters/")]
        [HttpPost]
        [EnableCors]
        public async Task<IActionResult> InsertCharacter([FromBody]CharactersALT character)    
        {
            _characterRepository.Insert(character);

            return Ok(new { Message = "Character saved!"});
        }
        
        [Route("/characters/{id}")]
        [HttpDelete]
        [EnableCors]
        public async Task<IActionResult> DeleteCharacter(int id)    
        {
            if (_characterRepository.Delete(id)) 
            { 
                return Ok(new { Message = "Character Removed!"});            
            }
            return BadRequest(new { Message = "Character not found!" });
        }
        
        [Route("/characters/")]
        [HttpPut]
        [EnableCors]
        public async Task<IActionResult> UpdateCharacter([FromBody] CharactersALT character)    
        {
            if (_characterRepository.Update(character)) 
            { 
                return Ok(new { Message = "Character Updated!"});            
            }
            return BadRequest(new { Message = "Character not found!" });
        }
        
        //[Route("/testendpoint/")]
        //[HttpGet]
        //[EnableCors]
        //public async Task<IActionResult> TestEndpoint()    
        //{
        //    var characterList = _characterRepository.GetAllCharactersALT();


        //    return Ok(characterList);
        //}
    }
}
