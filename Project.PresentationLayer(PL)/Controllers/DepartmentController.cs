using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BussinessLogicLayer_BLL_.Interfaces;
using Project.BussinessLogicLayer_BLL_.Repositories;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.PresentationLayer_PL_.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments =await unitOfWork.DepartmentRepository.GetAll();
            var mappeddepartment = mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(mappeddepartment);
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel )
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<Department>(departmentViewModel);
                await unitOfWork.DepartmentRepository.Add(mappedDepartment);
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }
        public async Task<IActionResult>  Details(int?id)
        {
            if (id is null)
                return NotFound();
            var department = await unitOfWork.DepartmentRepository.Get(id);
            var mappedDepartment = mapper.Map<DepartmentViewModel>(department);
            if (department is null)
                return NotFound();
            return View(mappedDepartment);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return NotFound();
            var department = await unitOfWork.DepartmentRepository.Get(id);
            var mappedDepartment = mapper.Map<DepartmentViewModel>(department);
            if (department is null)
                return NotFound();
            return View(mappedDepartment);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] int? id, DepartmentViewModel departmentViewModel)
        {
            if (id != departmentViewModel.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDepartment = mapper.Map<Department>(departmentViewModel);
                   await unitOfWork.DepartmentRepository.Update(mappedDepartment);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(departmentViewModel);
                }

            }
            return View(departmentViewModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var department = await unitOfWork.DepartmentRepository.Get(id);
            var mappedDepartment = mapper.Map<DepartmentViewModel>(department);
            if (department is null)
                return NotFound();
            return View(mappedDepartment);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int? id, DepartmentViewModel departmentViewModel)
        {
            if (id != departmentViewModel.Id)
                return NotFound();

            try
            {
                var mappedDepartment = mapper.Map<Department>(departmentViewModel);
                await unitOfWork.DepartmentRepository.Delete(mappedDepartment);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(departmentViewModel);
            }

        }

        #region Simple way To Delete Without Veiw
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id is null)
        //        return NotFound();
        //    var department = await departmentRepository.Get(id);
        //    if (department is null)
        //        return NotFound();
        //    await departmentRepository.Delete(department);
        //    return RedirectToAction("Index");
        //}
        #endregion
    }
}
