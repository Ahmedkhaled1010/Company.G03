using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _DepartmentRepository;
        public DepartmentController(IDepartmentRepository DepartmentRepository) 
        {
        
            _DepartmentRepository = DepartmentRepository;
        }
        public IActionResult Index()
        {
            var department=_DepartmentRepository.GetAll();
            return View(department);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _DepartmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
                
            }
            return View(department);
        }
        public IActionResult Details(int? Id,string viewname="Details")
        {
            if (Id is null)
                return BadRequest();
            var dept = _DepartmentRepository.GetById(Id.Value);
            if (dept is null)
                return NotFound();
            return View(viewname, dept);
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            //if (Id is null)
            //    return BadRequest();
            //var dept = _DepartmentRepository.GetById(Id.Value);
            //if (dept is null)
            //    return NotFound();
            //return View(dept);
            return Details( Id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department,[FromRoute]int id)
        {
            if (id !=department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _DepartmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(department);
        }
        public IActionResult Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();
            var dept = _DepartmentRepository.GetById(Id.Value);
            if (dept is null)
                return NotFound();
            return View(dept);
        }
        [HttpPost]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id !=department.Id)
            {
                return BadRequest();
            }
            try
            {
                _DepartmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch(System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }
        }
    }
}
