using System;
using System.ComponentModel.DataAnnotations;

namespace Association.Models
{
    public class Depense
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Nom_Association")]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Type_depense { get; set; }

        [Required(ErrorMessage = "Please choose Date_activite")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter Libelle")]
        [Display(Name = "Libelle")]
        [StringLength(100)]
        public string Libelle { get; set; }
    }
}
