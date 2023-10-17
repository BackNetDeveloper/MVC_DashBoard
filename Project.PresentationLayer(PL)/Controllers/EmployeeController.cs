using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BussinessLogicLayer_BLL_.Interfaces;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Helper;
using Project.PresentationLayer_PL_.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.PresentationLayer_PL_.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue = "")
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await unitOfWork.EmployeeRepository.GetAll();
            }

            else
            {
                employees = await unitOfWork.EmployeeRepository.Search(SearchValue);
            }
            var Mappedemployees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            return View(Mappedemployees);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.departments =await unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeviewmodel)
        {
            if (ModelState.IsValid)
            {
                employeeviewmodel.ImageUrl = DocumentSettings.UploadFile(employeeviewmodel.Image, "Imgs");
                var mappedemployee =  mapper.Map<Employee>(employeeviewmodel);
                await unitOfWork.EmployeeRepository.Add(mappedemployee);
                return RedirectToAction("Index");
            }
            return View(employeeviewmodel);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();
            var employee = await unitOfWork.EmployeeRepository.Get(id);
            var departmentName = await unitOfWork.EmployeeRepository.GetDepartmentByEmployeeId(id);
            employee.Department.Name = departmentName;
            var mappedemployee = mapper.Map<EmployeeViewModel>(employee);
            if (employee is null)
                return NotFound();
            return View(mappedemployee);
        }
        public async Task<IActionResult> Update( int? id)
        {
            if (id is null)
                return NotFound();
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAll();
            var employee = await unitOfWork.EmployeeRepository.Get(id);
            var mappedemployee = mapper.Map<EmployeeViewModel>(employee);
            if (employee is null)
                return NotFound();
           
            return View(mappedemployee);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute]int? id, EmployeeViewModel employeeviewmodel)
        {
            if (id != employeeviewmodel.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeviewmodel.ImageUrl = DocumentSettings.UploadFile(employeeviewmodel.Image, "Imgs");
                    var mappedemployee = mapper.Map<Employee>(employeeviewmodel);
                    await unitOfWork.EmployeeRepository.Update(mappedemployee);
                    ViewBag.departments = await unitOfWork.DepartmentRepository.GetAll();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(employeeviewmodel);
                }

            }
            return View(employeeviewmodel);
        }
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var employee = await unitOfWork.EmployeeRepository.Get(id);
            if (employee is null)
                return NotFound();
            DocumentSettings.DeleteFile("Imgs",employee.ImageUrl);
            await unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
