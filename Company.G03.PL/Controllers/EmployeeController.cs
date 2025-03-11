using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repository;
using Company.G03.DAL.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employee;

        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }
        public IActionResult Index()
        {
            var Employee =_employee.GetAll();
            return View(Employee);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employee.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
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
            return View(emp);
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
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id !=employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _employee.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }
            return View(employee);
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
            return View(emp);
        }
        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            try
            {
                _employee.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }
        }
    }
}
