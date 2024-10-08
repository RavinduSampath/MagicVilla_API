using MagicVilla_VillAPI.Data;
using MagicVilla_VillAPI.Models;
using MagicVilla_VillAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace MagicVilla_VillAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly ILogger<VillaAPIController> _logger;

        // Single constructor to resolve dependencies
        public VillaAPIController(ApplicationDbContext db, ILogger<VillaAPIController> logger)
        {
            _db = db;
            _logger = logger;
        }



        [HttpGet]
        //public IEnumerable<VillaDTO> GetVillas() 
        //{
        //    return Villastore.villaList;
        //}

        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("GetVillas method called");
            return Ok(_db.Villas.ToList());
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
                _logger.LogError("Get villa Error with ID "+id);
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
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

            if(_db.Villas.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
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

            Villa model = new ()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Sqft = villaDTO.Id,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Rate = villaDTO.Rate,
                Amenity = villaDTO.Amenity,
            };  

            _db.Villas.Add(model);
            _db.SaveChanges();


            return CreatedAtRoute("GetVilla", new { id=villaDTO.Id },villaDTO);



            
        }
        //delete


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id:int}", Name = "DeleteVilla")]

        //IActionResult is a base class for all action results in ASP.NET Core
        //eka use karanne return type rkk dan on nathuv inna..delete karam return ekk nahne
        public IActionResult DeleteVilla(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);   
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();

        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id,[FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null|| id!= villaDTO.Id)
            {
                return BadRequest(villaDTO);
            }

            //var villa = _db.Villas.FirstOrDefault(u => u.Id == villaDTO.Id);
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;
            //villa.Sqft = villaDTO.Sqft;

            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Sqft = villaDTO.Id,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Rate = villaDTO.Rate,
                Amenity = villaDTO.Amenity,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent() ;


            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) 
        {
            if (patchDTO == null || id== 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
            

            VillaDTO VillaDTO  = new()
            {
                Id=villa.Id,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Sqft = villa.Id,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Rate = villa.Rate,
                Amenity = villa.Amenity,
            };





            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(VillaDTO, ModelState);
            Villa model = new Villa()
            {
                Id=VillaDTO.Id,
                Name = VillaDTO.Name,
                Occupancy = VillaDTO.Occupancy,
                Sqft = VillaDTO.Id,
                Details = VillaDTO.Details,
                ImageUrl = VillaDTO.ImageUrl,
                Rate = VillaDTO.Rate,
                Amenity = VillaDTO.Amenity,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
