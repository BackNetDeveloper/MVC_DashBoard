using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BussinessLogicLayer_BLL_.Repositories;
using Project.DataAccessLayer_DAL_.Entities;
using Project.PresentationLayer_PL_.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.PresentationLayer_PL_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string SearchValue="")
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = userManager.Users.ToList();
                return View(users);
            }
            else 
            {
                var user = await userManager.FindByEmailAsync(SearchValue);
                return View(new List<ApplicationUser> {user});
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            return View(user);
        }
        public async Task<IActionResult> Update(string id)
        {

            if (id is null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
                return BadRequest();
            if (ModelState.IsValid) 
            {
               
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();
                    user.PhoneNumber = applicationUser.PhoneNumber;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");

                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty,Error.Description);

                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return View();
        }
        
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            if (id is null)
                return NotFound();
            
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    var result = await userManager.DeleteAsync(user);
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
    }
}
