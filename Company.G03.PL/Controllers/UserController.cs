using AutoMapper;
using Company.G03.DAL.Model;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var users = await userManager.Users.Select(U=>new UserViewModel()
                {

                    Id = U.Id,
                    FName=U.FName,
                    LName=U.LName,
                    Email= U.Email,
                    Phone=U.PhoneNumber,
                    Roles =userManager.GetRolesAsync(U).Result,
                }).ToListAsync();
                return View(users);

            }
            else
            {
                var user = await userManager.FindByEmailAsync(searchValue);
                var MappedUser = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Roles =await userManager.GetRolesAsync(user),
                };
                return View(new List<UserViewModel> { MappedUser});
            }
        }

        public async Task<IActionResult> Details(string id,string viewName="Details")
            
        {
            if (id is  null)
            {
                return BadRequest();
            }
            var user = await  userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(user);
            var MappedUser = mapper.Map<ApplicationUser,UserViewModel>(user);

            return View(viewName,MappedUser); 
        }

        public async Task<IActionResult> Edit(string id)
        {
           return await Details(id,"Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user, [FromRoute] string id)
        {
            if (id != user.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                  var User= await userManager.FindByIdAsync(id);
                    User.FName= user.FName;
                    User.LName= user.LName;
                    User.PhoneNumber = user.Phone;
                   // var mappedUser = mapper.Map<UserViewModel, ApplicationUser>(user);
                    await userManager.UpdateAsync(User);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
