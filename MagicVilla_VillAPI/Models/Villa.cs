using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

namespace MagicVilla_VillAPI.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }

        public int Sqft { get; set; }

        public int Occupancy { get; set; }

        public String ImageUrl { get; set; }

        public String Amenity { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
