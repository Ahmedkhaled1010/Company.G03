using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var department= await unitOfWork.DepartmentRepository.GetAllAsync();
            return View(department);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
               await unitOfWork.DepartmentRepository.AddAsync(department);
                int res = await unitOfWork.CompleteAsync();
                if (res>0)
                {
                    TempData["message"] = "Department Is Created";
                }
                return RedirectToAction(nameof(Index));
                
            }
            return View(department);
        }
        public async Task<IActionResult> Details(int? Id,string viewname="Details")
        {
            if (Id is null)
                return BadRequest();
            var dept =await unitOfWork.DepartmentRepository.GetByIdAsync(Id.Value);
            if (dept is null)
                return NotFound();
            return View(viewname, dept);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            //if (Id is null)
            //    return BadRequest();
            //var dept = unitOfWork.DepartmentRepository.GetById(Id.Value);
            //if (dept is null)
            //    return NotFound();
            //return View(dept);
            return await Details( Id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department,[FromRoute]int id)
        {
            if (id !=department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.DepartmentRepository.Update(department);
                   await unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(department);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();
            var dept = await unitOfWork.DepartmentRepository.GetByIdAsync(Id.Value);
            if (dept is null)
                return NotFound();
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id !=department.Id)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.DepartmentRepository.Delete(department);
               await unitOfWork.CompleteAsync();
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
