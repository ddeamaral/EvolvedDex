using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvolvedDex.Models.Entities
{
    public class Pokemon
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Range(1, 801, ErrorMessage = "")]
        public int PokedexNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double BaseAttack { get; set; }

        [Required]
        public double BaseDefense { get; set; }

        [Required]
        public double BaseSpeed { get; set; }

        [Required]
        public double BaseSpecialAttack { get; set; }

        [Required]
        public double BaseSpecialDefense { get; set; }

        //[Required]
        //public List<string> ImagePath { get; set; }

        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Description must be between {1} and {0} characters long.")]
        public string Description { get; set; }

        [Required]
        public bool isInMod { get; set; }

        [NotMapped]
        public string DateReleased { get; set; }


    }
}
