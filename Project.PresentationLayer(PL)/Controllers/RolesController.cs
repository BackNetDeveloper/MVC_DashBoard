using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BussinessLogicLayer_BLL_.Repositories;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Models;
using Project.PresentationLayer_PL_.Models.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.PresentationLayer_PL_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager ,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var Error in result.Errors)
                    ModelState.AddModelError(string.Empty, Error.Description);
                return View(identityRole);
            }
            return View(identityRole);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return NotFound();
            var Role = await roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();
            return View(Role);
        }

        public async Task<IActionResult> Update(string id)
        {

            if (id is null)
                return NotFound();
            var user = await roleManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id,IdentityRole identityRole)
        {
            if (id != identityRole.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    var Role = await roleManager.FindByIdAsync(id);
                    Role.Name = identityRole.Name;
                    Role.NormalizedName = identityRole.Name.ToUpper();

                    var result = await roleManager.UpdateAsync(Role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");

                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);

                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (id is null)
                return NotFound();

            try
            {
                var Role = await roleManager.FindByIdAsync(id);
                var result = await roleManager.DeleteAsync(Role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var Error in result.Errors)
                    ModelState.AddModelError(string.Empty, Error.Description);
            }
            catch (System.Exception)
            {

                throw;
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddOrRemoveUsers( string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();
            ViewBag.RoleId = RoleId;
            ViewBag.RoleName = role.Name;
            var users = new List<UserInRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var UserInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                var check = await userManager.IsInRoleAsync(user, role.Name);
                if (check)
                    UserInRole.IsSelected = true;
                else
                    UserInRole.IsSelected = false;
                users.Add(UserInRole);
            }
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> models , string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await userManager.FindByIdAsync(item.UserId);
                    if (user is not null)
                    {
                        if (item.IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                            await userManager.AddToRoleAsync(user, role.Name);
                        else if (!item.IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                                await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return RedirectToAction("Update",new {Id = RoleId });// We Must Send The RoleId Back To The Update Action
            }
            return View(models);
        }
    } 
}
