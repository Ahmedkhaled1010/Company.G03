using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repository;
using Company.G03.DAL.Model;
using Company.G03.PL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        private readonly IMapper _mapper;

        {
            _employee = employee;
           _department = department;
            _mapper = mapper;
        }
        {
        }
            else
            {
                 Employee = _employee.GetEmployeesByName(search);
               
            }
            var EmployeeMapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employee);
            return View(EmployeeMapped);

        }
        public IActionResult Create()
        {
            ViewBag.departments = _department.GetAll();
            

            return View();
        }
        [HttpPost]
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int? id) 
        {
            if (id is null)
                return BadRequest();
            var emp = _employee.GetById(id.Value);
            if (emp is null)
            {
                return NotFound();
            }
        }
        public IActionResult Edit (int? id)
        {
            if (id is null)
                return BadRequest();
            var emp = _employee.GetById(id.Value);
            if (emp is null)
            {
                return NotFound();
            }
        }
        [HttpPost]
        {
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var emp =_employee.GetById(id.Value);
            if(emp is null)
                return NotFound();
        }
        [HttpPost]
        {
            {
                return BadRequest();
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}
