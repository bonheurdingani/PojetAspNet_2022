using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Association.Models
{
    public class Membre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Prenom")]
        [Display(Name = "Prenom")]
        [StringLength(100)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Please enter Sexe")]
        public string Sexe { get; set; }

        [Required(ErrorMessage = "Please choose Data_Naiss")]
        public DateTime Data_Naiss { get; set; }

        [Required(ErrorMessage = "Please enter Lieu_Naiss")]
        [Display(Name = "Lieu_Naiss")]
        [StringLength(100)]
        public string Lieu_Naiss { get; set; }

        [Required(ErrorMessage = "Please enter Nationalite")]
        [Display(Name = "Nationalite")]
        [StringLength(100)]
        public string Nationalite { get; set; }

        [Required(ErrorMessage = "Please enter Num_telephone")]
        public string Num_telephone { get; set; }

        [Required(ErrorMessage = "Please enter Adresse")]
        [Display(Name = "Adresse")]
        [StringLength(100)]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Please enter Profession")]
        [Display(Name = "Profession")]
        [StringLength(100)]
        public string Profession { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        public string ProfilePicture { get; set; }
    }
}
