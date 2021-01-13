using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageStudentsSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace ManageStudentsSystem.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the StudentManager class
    public class StudentManager : IdentityUser
    {
        public IEnumerable<Student> Students { get; set; }
    }
}
