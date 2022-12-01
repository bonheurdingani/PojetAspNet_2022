using Association.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Association.ViewModel
{
    public class AssociationViewModel 
    {
        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Please enter Code")]
        [Display(Name = "Code")]
        [StringLength(100)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please choose Annee_creation")]
        public DateTime Annee_creation { get; set; }

        [Required(ErrorMessage = "Please enter Telephone1")]
        public string Telephone1 { get; set; }

        [Required(ErrorMessage = "Please enter Telephone2")]
        public string Telephone2 { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Adresse")]
        [Display(Name = "Adresse")]
        [StringLength(100)]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
    }
}
