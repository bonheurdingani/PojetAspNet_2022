using System;
using System.ComponentModel.DataAnnotations;

namespace Association.Models
{
    public class Activite
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Nom_Association")]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Nom_association { get; set; }

        [Required(ErrorMessage = "Please choose Date_activite")]
        public DateTime Date_activite { get; set; }

        [Required(ErrorMessage = "Please enter Libelle")]
        [Display(Name = "Libelle")]
        [StringLength(100)]
        public string Libelle { get; set; }

    }
}
