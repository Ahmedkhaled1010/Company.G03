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

        public EmployeeController(IEmployeeRepository employee,IDepartmentRepository department,IMapper mapper)
        {
            _employee = employee;
           _department = department;
            _mapper = mapper;
        }
        public IActionResult Index(string search)
        {
            IEnumerable<Employee> Employee;
            if (string.IsNullOrEmpty(search))
            {
                 Employee = _employee.GetAll();
              
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
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _employee.Add(MappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
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
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(MappedEmployee);
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
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(MappedEmployee);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id !=employeeVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                    _employee.Update(MappedEmployee);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }
            return View(employeeVm);
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
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(MappedEmployee);
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employee.Delete(MappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }
    }
}
