using System.Data;
using AutoMapper;
using Company.G03.DAL.Model;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper )
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public async Task< IActionResult> Index(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var Roles =await roleManager.Roles.ToListAsync();
                var MappedRole =mapper.Map<IEnumerable<IdentityRole>,IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRole);

            }
            else
            {
                var role = await roleManager.FindByNameAsync(searchValue);
                var MappedRole = mapper.Map<IdentityRole, RoleViewModel>(role);
                return View(new List<RoleViewModel>() { MappedRole });
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Create(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = mapper.Map<RoleViewModel,IdentityRole>(role);
                await roleManager.CreateAsync(MappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")

        {
            if (id is null)
            {
                return BadRequest();
            }
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound(role);
            var MappedRole = mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(viewName, MappedRole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel role, [FromRoute] string id)
        {
            if (id != role.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await roleManager.FindByIdAsync(id);
                    Role.Name = role.RoleName;
                    // var mappedUser = mapper.Map<UserViewModel, ApplicationUser>(user);
                    await roleManager.UpdateAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
