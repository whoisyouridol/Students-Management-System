using System;
using AutoMapper;
using ManageStudentsSystem.Areas.Identity.Data;
using ManageStudentsSystem.Models;
using ManageStudentsSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageStudentsSystem.Data;

namespace ManageStudentsSystem.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageStudentsController : Controller
    {
        private readonly UserManager<StudentManager> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ManageStudentsSystemContext _context;
        private readonly IMapper _mapper;
        public ManageStudentsController(
            UserManager<StudentManager> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            ManageStudentsSystemContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            if (await _roleManager.RoleExistsAsync("admin"))
            {
                return View();
            }

            #region If admin doesn`t exists I perform this code.
            await _roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            var currentUser = await _userManager.GetUserAsync(User);
            await _userManager.AddToRoleAsync(currentUser, "admin");
            await _userManager.UpdateAsync(currentUser);
            return View();
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(StudentViewModel newStudent)
        {
            if (!ModelState.IsValid)
            {
                return View(newStudent);
            }


            var currentUser = await _userManager.GetUserAsync(User);
            newStudent.StudentManagerId = currentUser.Id;
            var mappedStudent = _mapper.Map<Student>(newStudent);
            currentUser.Students = new List<Student> { mappedStudent };
            await _userManager.UpdateAsync(currentUser);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ViewStudents()
        {
            var students = _userManager.Users.Include(x => x.Students).SelectMany(x => x.Students).ToList();
            var viewModels = _mapper.Map<List<Student>, List<ViewStudentViewModel>>(students);
            foreach (var viewModel in viewModels)
            {
                viewModel.Age = ((DateTime.Now - viewModel.BirthDate).Days) / 365;
            }
            return View(viewModels);
        }

        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _userManager.Users
                .Include(x => x.Students)
                .SelectMany(x => x.Students)
                .FirstOrDefaultAsync(x => x.StudentsId == id);
            var viewModel = _mapper.Map<EditStudentViewModel>(student);
            return View(viewModel);
        }

        public async Task<IActionResult> SaveChanges(EditStudentViewModel studentToEdit)
        {
            if (!ModelState.IsValid) return Content("Model state is not valid!");

            var currentUser = await _userManager.GetUserAsync(User);

            var student = await _userManager.Users
                .Include(x => x.Students)
                .SelectMany(x => x.Students)
                .SingleOrDefaultAsync(x => x.StudentsId == studentToEdit.StudentsId);

            student.FirstName = studentToEdit.FirstName;
            student.LastName = studentToEdit.LastName;
            student.IdNumber = studentToEdit.IdNumber;
            student.BirthDate = studentToEdit.BirthDate;
            student.PhoneNumber = studentToEdit.PhoneNumber;
            student.Address = studentToEdit.Address;
            student.Email = studentToEdit.Email;




            //student = _mapper.Map<Student>(studentToEdit);


            await _userManager.UpdateAsync(currentUser);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var students = _userManager.Users.Include(x => x.Students).SelectMany(x => x.Students).ToList();
            var studentToRemove = students.SingleOrDefault(x => x.StudentsId == id);
            students.Remove(studentToRemove);

            if (studentToRemove != null)
            {
                _ = _context.Remove(studentToRemove);
                await _context.SaveChangesAsync();
            }

            currentUser.Students = students;
            await _userManager.UpdateAsync(currentUser);

           
            return RedirectToAction("Index", "Home");
        }
    }
}
