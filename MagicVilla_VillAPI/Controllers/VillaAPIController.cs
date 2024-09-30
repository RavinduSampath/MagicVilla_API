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
        //public IEnumerable<VillaDTO> GetVillas() 
        //{
        //    return Villastore.villaList;
        //}

        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(Villastore.villaList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = Villastore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
    }
}
