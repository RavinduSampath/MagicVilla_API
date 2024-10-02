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

        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //valled ekd kiyl bln empty ev yann den na



            //eka vage dekk en eka nathara karann yata eka

            if(Villastore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error Message", "Villa Name already exists");
                return BadRequest(ModelState);
            }


            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id=Villastore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;  
            Villastore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id=villaDTO.Id },villaDTO);



            //delete


        }
    }
}
