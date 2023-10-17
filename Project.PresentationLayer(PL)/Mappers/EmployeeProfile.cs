using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Models;

namespace Project.PresentationLayer_PL_.Mappers
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {

            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();

            //// Or Use The Simple Way
            //CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
