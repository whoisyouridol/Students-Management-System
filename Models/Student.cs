using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ManageStudentsSystem.Areas.Identity.Data;

namespace ManageStudentsSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentsId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ID number is required")]
        [Display(Name = "Id number")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [Display(Name = "Birth date")]
        [MinimumAgeAttribute(18)]
        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }


        public string StudentManagerId { get; set; }
        [ForeignKey("StudentManagerId")]
        public StudentManager StudentManager { get; set; }

    }
}
