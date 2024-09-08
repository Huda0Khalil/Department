using AutoMapper;
using Departments_Project.Entities;
using Departments_Project.Entities.DTO;
namespace Departments_Project.Service
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDataFromCSVfile, Employee>();
        }
    }
}
