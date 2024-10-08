﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Occupancy { get; set; }  

        public int Sqft { get; set; }
        public String ImageUrl { get; set; }

        public String Amenity { get; set; }

        public string Details { get; set; }
        [Required]

        public double Rate { get; set; }
    }
} 
