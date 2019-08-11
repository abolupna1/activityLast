using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using AActivity.Areas.Admin.ModelViews;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Core.Flash;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Owner")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
      
        private IFlasher _f;
        public UsersController(ApplicationDbContext context, IFlasher f,
            IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _f = f;
          
        }

        // GET: Admin/Users

          
        public async Task<IActionResult> Index()
        {
       
              var userList = await _context.Users.Include(r => r.UserRoles).ToListAsync();
            UsersView usersView = new UsersView()
            {
                Users = userList,
                Roles = await _context.Roles.ToListAsync()
            };

       
                return View(usersView);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
         
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }
            var userForDetails = _mapper.Map<AppUser,UserCreate>(user);
            userForDetails.UserRoles = await _context.UserRoles.Where(u => u.UserId == id).Include(r=>r.Role).ToListAsync();
            return View(userForDetails);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            UserCreate userCreate = new UserCreate()
            {
                Roles = _context.Roles.Where(r=>r.Id != 1).ToList()
             };
        
            return View(userCreate);
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreate userCreate,string[] roles)
        {
            if (roles.Count() == 0)
            {
                ViewBag.msg = "يجب اختيار دور على الاقل ";
                userCreate.Roles = _context.Roles.ToList();
                return View(userCreate);
            }
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserCreate,AppUser>(userCreate);
              
                var result = await _userManager.CreateAsync(user, user.PhoneNumber);
                if (result.Succeeded)
                {
                    foreach (var role in roles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                    _f.Flash("success", "تم الحفظ بنجاح");
                    return RedirectToAction(nameof(Index));
                }
               
            }
            return View(userCreate);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id.Value);

            if (user == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }
            var userForDetails = _mapper.Map<AppUser, UserCreate>(user);
            userForDetails.Roles = _context.Roles.ToList();
            userForDetails.UserRoles = await _context.UserRoles.Where(u => u.UserId == id).Include(r => r.Role).ToListAsync();
            return View(userForDetails);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserCreate userCreate, string[] roles)
        {
           
            if (id != userCreate.Id)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }
            if (roles.Count() == 0)
            {
                ViewBag.msg = "يجب اختيار دور على الاقل ";
                userCreate.Roles = _context.Roles.ToList();
                userCreate.UserRoles = await _context.UserRoles.Where(u => u.UserId == id).Include(r => r.Role).ToListAsync();
                return View(userCreate);
            }
            if (ModelState.IsValid)
            {
               var userForUpdate = await _context.Users.FirstOrDefaultAsync(u=>u.Id == userCreate.Id);
                userForUpdate.FullName = userCreate.FullName;
                userForUpdate.Email = userCreate.Email;
                userForUpdate.PhoneNumber = userCreate.PhoneNumber;
                _context.Update(userForUpdate);
                await _context.SaveChangesAsync();

                var rolesForDelete = await _context.UserRoles.Where(u => u.UserId == userForUpdate.Id && u.RoleId != 1).ToListAsync();
                    foreach (var roleD in await _context.Roles.ToListAsync())
                    {
                        await _userManager.RemoveFromRoleAsync(userForUpdate, roleD.Name);
                    }
                    foreach (var role in roles)
                    {
                        await _userManager.AddToRoleAsync(userForUpdate, role);
                    }
              
      
                _f.Flash("success", "تم التعديل بنجاح");
                return RedirectToAction(nameof(Index));
                
            
            }
            userCreate.Roles = _context.Roles.ToList();
            return View(userCreate);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound");
            }
         
         
            var userForDetails = _mapper.Map<AppUser, UserCreate>(user);
            userForDetails.UserRoles = await _context.UserRoles.Where(u => u.UserId == id).Include(r => r.Role).ToListAsync();
            return View(userForDetails);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(id != 1)
            {
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _f.Flash("success", "تم الحذف بنجاح");
                return RedirectToAction(nameof(Index));
            }
            else
            {


                _f.Flash("danger", "لايمكن حذف هذا المستخدم !!");
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
