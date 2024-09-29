using MagicVilla_VillAPI.Data;
using MagicVilla_VillAPI.Models;
using MagicVilla_VillAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas() 
        {
            return Villastore.villaList;
        }
        [HttpGet("{id:int}")]
        public IEnumerable<VillaDTO> GetVillas(int id)
        {
            yield return Villastore.villaList.FirstOrDefault(u => u.Id == id);
        }
    }
}
