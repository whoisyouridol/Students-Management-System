using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageStudentsSystem.Models;
using ManageStudentsSystem.ViewModels;

namespace ManageStudentsSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();

            CreateMap<Student, ViewStudentViewModel>();
            CreateMap<ViewStudentViewModel, Student>(); 

            CreateMap<Student, EditStudentViewModel>();
            CreateMap<EditStudentViewModel, Student>();
            
        }
    }
}
