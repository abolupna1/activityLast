using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AActivity.Areas.Admin.ModelViews;
using AActivity.Areas.Admin.Helpers;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class SignaturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _ihostingEnvironment;

        public SignaturesController(ApplicationDbContext context, IHostingEnvironment ihostingEnvironment)
        {
            _context = context;
            _ihostingEnvironment = ihostingEnvironment;
        }

        // GET: Admin/Signatures
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Signatures.Include(s => s.User)
                .Include(j => j.JobsSignatorie)
                .Include(j=>j.TypesOfLettersAndSignatures).ThenInclude(t=>t.TypesOfletter);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Admin/Signatures/Details/5


        // GET: Admin/Signatures/Create
        public IActionResult Create()
        {
            ViewData["JobsSignatorieId"] = new SelectList( _context.JobsSignatories, "Id", "Name");
            ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name");
            ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        // POST: Admin/Signatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( SignatureCreateViewModel signature)
        {
            if(SignatureExists(signature.UserId))
            {
               
                ViewData["SignatureExists"] = "لايمكن اضافة توقيع آخر / يملك توقيع مسبقا!!";
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
                ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name");
                ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name");
                ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name");

                return View(signature);
            }
            if (ModelState.IsValid)
            {
                string uniqFileName = null;
                if (signature.SignaturePhoto != null && signature.SignaturePhoto.Length > 0)
                {
                    if (IsFileValidate(signature.SignaturePhoto.FileName))
                    {
                        string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/signatures");
                        uniqFileName = Guid.NewGuid().ToString() + "_" + signature.SignaturePhoto.FileName;
                        string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            signature.SignaturePhoto.CopyTo(fileStream);
                        }


                    }
                    else
                    {
                        ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";
                        ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name", signature.Degree);
                        ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name");
                        ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name");

                        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
                        return View(signature);

                    }


                 
                }
                Signature signatureforSave = new Signature()
                {
                   
                    UserId = signature.UserId,
                    SignaturePhoto = uniqFileName,
                    Degree=signature.Degree,
                    JobsSignatorieId =signature.JobsSignatorieId,
                    Status=signature.Status
                };
                _context.Signatures.Add(signatureforSave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name");
            ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name");

            ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name");

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
            return View(signature);
        }

        // GET: Admin/Signatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SignaturesNotFound");
            }

            var signature = await _context.Signatures.FindAsync(id);
            if (signature == null)
            {
                Response.StatusCode = 404;
                return View("SignaturesNotFound");
            }
            SignatureEditViewModel models = new SignatureEditViewModel()
            {
                Id = signature.Id,
                SignaturePhotoExists = signature.SignaturePhoto,
                UserId=signature.UserId,
                Degree=signature.Degree,
                JobsSignatorieId = signature.JobsSignatorieId,
                Status =signature.Status
            };
            ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name",signature.Degree);
            ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name",signature.JobsSignatorieId);
            ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name", signature.Status);

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
            return View(models);
        }

        // POST: Admin/Signatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SignatureEditViewModel signature)
        {
            if (id != signature.Id)
            {
                Response.StatusCode = 404;
                return View("SignaturesNotFound");
            }

            if (ModelState.IsValid)
            {
                string filenameForEdit = null;
                string uniqFileName = null;

                if (signature.SignaturePhoto != null && signature.SignaturePhoto.Length > 0)
                {
                    string filePathForDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
                "img/signatures", signature.SignaturePhotoExists);
                    System.IO.File.Delete(filePathForDelete);

                    if (IsFileValidate(signature.SignaturePhoto.FileName))
                        {
                            string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/signatures");
                            uniqFileName = Guid.NewGuid().ToString() + "_" + signature.SignaturePhoto.FileName;
                            string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                signature.SignaturePhoto.CopyTo(fileStream);
                            }


                        }
                        else
                        {
                            ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";
                        ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name", signature.Degree);
                        ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name", signature.JobsSignatorieId);

                        ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name", signature.Status);

                        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
                        return View(signature);

                    }

                }
                filenameForEdit = uniqFileName == null ? signature.SignaturePhotoExists : uniqFileName;

                Signature signatureforSave = new Signature()
                {

                    UserId = signature.UserId,
                    SignaturePhoto = filenameForEdit,
                    Id=id,
                    Degree = signature.Degree,
                 JobsSignatorieId= signature.JobsSignatorieId,
                    Status = signature.Status

                };
                if (!SignatureExists(id,signature.UserId))
                {
                    if (SignatureExists(signature.UserId))
                    {
                        ViewData["SignatureExists"] = "لايمكن اضافة توقيع آخر / يملك توقيع مسبقا!!";
                        ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name", signature.Degree);
                        ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name", signature.JobsSignatorieId);

                        ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name", signature.Status);

                        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
                        return View(signature);
                       
                    }
                }
                _context.Update(signatureforSave);
                await _context.SaveChangesAsync();
                
              
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeList"] = new SelectList(DegreeList(), "Name", "Name", signature.Degree);
            ViewData["JobsSignatorieId"] = new SelectList(_context.JobsSignatories, "Id", "Name", signature.JobsSignatorieId);

            ViewData["ValidUnValidList"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name", signature.Status);

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", signature.UserId);
            return View(signature);
        }

        // GET: Admin/Signatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SignaturesNotFound");
            }

            var signature = await _context.Signatures
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signature == null)
            {
                Response.StatusCode = 404;
                return View("SignaturesNotFound");
            }

            return View(signature);
        }

        // POST: Admin/Signatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signature = await _context.Signatures.FindAsync(id);
           
            try
            {
               
                _context.Signatures.Remove(signature);
                await _context.SaveChangesAsync();
                string filePathForDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
               "img/signatures", signature.SignaturePhoto);
                System.IO.File.Delete(filePathForDelete);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var user = await _context.Users.FindAsync(signature.UserId);
                ViewBag.ErrorTitle = $"توقيع ( {user.FullName}  ) مستخدم";
                ViewBag.ErrorMessage = $"( {user.FullName}  )  توقيعه مستخدم لبعض الخطابات يجب حذف جميع الخطابات الموقع منه";
                return View("Error");
            }
        }

        private bool SignatureExists(int id,int userId)
        {
            return _context.Signatures.Any(e => e.Id == id && e.UserId == userId);
        }
        private bool SignatureExists(int userId)
        {
            return _context.Signatures.Any(e => e.UserId == userId);
        }
        private bool IsFileValidate(string filename)
        {
            string extintion = Path.GetExtension(filename);
            if (extintion.Contains(".png")) { return true; }
            if (extintion.Contains(".PNG")) { return true; }
            if (extintion.Contains(".jpeg")) { return true; }
            if (extintion.Contains(".jpg")) { return true; }
            if (extintion.Contains(".gif")) { return true; }
            if (extintion.Contains(".bmp")) { return true; }
            return false;
        }

        private IList<DegreeHelper> DegreeList()
        {
            IList<DegreeHelper> degreesHelper = new List<DegreeHelper>()
            {
                new DegreeHelper(){Name = "أ." },
                new DegreeHelper(){Name = "د." },
                new DegreeHelper(){Name = "أ د." },
            };
            return degreesHelper;
        }

        private IList<SignaturesRolesHelpr> SignaturesRolesList()
        {
            IList<SignaturesRolesHelpr> SignaturesRole = new List<SignaturesRolesHelpr>()
            {
                new SignaturesRolesHelpr(){ Name = "رئيس قسم النشاط الاجتماعي" },
                new SignaturesRolesHelpr(){Name = "مدير إدارة النشاط" },
                new SignaturesRolesHelpr(){Name = "وكيل العمادة لشؤون النشاط" },
                new SignaturesRolesHelpr(){Name = "عميد شؤون الطلاب" },
                new SignaturesRolesHelpr(){Name = "مفوض" },

            };
            return SignaturesRole;
        }

 

        
    }
}
