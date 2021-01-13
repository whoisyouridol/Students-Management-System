using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ManageStudentsSystem.ViewModels;

namespace ManageStudentsSystem.Models
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public MinimumAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var student = validationContext.ObjectInstance as StudentViewModel;


        //    var age = DateTime.Today.Year - student.BirthDate.Year;

        //    return (age >= _minAge)
        //        ? ValidationResult.Success
        //        : new ValidationResult("Student should be at least 18 years old.");
        //}
        public override bool IsValid(object dateInputed)
        {
            DateTime date;
            bool IsValid = DateTime.TryParse(dateInputed.ToString(), out date);

            if (IsValid)
            {
                return date.AddYears(_minAge) < DateTime.Now;
            }
            else
            {
                return false;
            }
        }
    }
}
