using AutoMapper;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Models;

namespace Project.PresentationLayer_PL_.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();

            //// Or Use The Simple Way
            //CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
