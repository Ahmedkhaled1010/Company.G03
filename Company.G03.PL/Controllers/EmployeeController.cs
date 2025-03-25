using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repository;
using Company.G03.DAL.Model;
using Company.G03.PL.Helper;
using Company.G03.PL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
          
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<Employee> Employee;
            if (string.IsNullOrEmpty(search))
            {
                 Employee =await unitOfWork.EmployeeRepository.GetAllAsync();
              
        }
            else
            {
                 Employee = unitOfWork.EmployeeRepository.GetEmployeesByName(search);
               
            }
            var EmployeeMapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employee);
            return View(EmployeeMapped);

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
               await unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
               await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id) 
        {
            if (id is null)
                return BadRequest();
            var emp = await unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (emp is null)
            {
                return NotFound();
            }
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(MappedEmployee);
        }
        public async Task<IActionResult> Edit (int? id)
        {
            if (id is null)
                return BadRequest();
            var emp =await unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (emp is null)
            {
                return NotFound();
            }
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(MappedEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id !=employeeVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVm.ImageName is not null)
                    {
                        employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");

                    }

                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                    unitOfWork.EmployeeRepository.Update(MappedEmployee);
                   await unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }
            return View(employeeVm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var emp =await unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if(emp is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(emp);

            return View(MappedEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                unitOfWork.EmployeeRepository.Delete(MappedEmployee);
               var res=await unitOfWork.CompleteAsync();
                if (res >0 && employeeVM.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                }
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
