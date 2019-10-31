using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using AActivity.Areas.Sociologist.ModelViews;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class TripReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _ihostingEnvironment;
        public TripReportsController(ApplicationDbContext context, IHostingEnvironment ihostingEnvironment)
        {
            _ihostingEnvironment=ihostingEnvironment;
            _context = context;
        }

     
        private bool TripBookingExists(int id)
        {
            return _context.TripBookings.Any(e => e.Id == id);
        }


        public  IActionResult Index()
        {
         
            return View();
        }


        [Route("Sociologist/TripReports/Create/{bokingId:int}")]

        public IActionResult Create(int bokingId)
        {
            if (!TripBookingExists(bokingId))
            {
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }
            var report = new TripReportCreateModelView()
            {
                TripBookingId= bokingId
            };
            return View(report);
        }

        [Route("Sociologist/TripReports/Edit/{reportId:int}")]
        public async Task<IActionResult> Edit(int reportId)
        {
            var report = await _context.TripReports
                .Include(i => i.TripReportImages)
                .Include(s => s.TripBooking.SchedulingTripDetail)
                .FirstOrDefaultAsync(r=>r.Id == reportId);

            var model = new TripReportEditModelView()
            {
                Id=report.Id,TripBookingId=report.TripBookingId,
                TextBody=report.TextBody,
                Images=report.TripReportImages.ToList(),
                DitailId=report.TripBooking.SchedulingTripDetailId


            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/TripReports/Edit/{reportId:int}")]
        public async Task<IActionResult> Edit(int reportId,TripReportEditModelView tripReport)
        {
            if (ModelState.IsValid)
            {

                if (tripReport.Photos != null)
                {
                    if (tripReport.Photos.Count() >= 5 || tripReport.Photos.Count() > 10)
                    {
                        foreach (var ph in tripReport.Photos)
                        {
                            if (!IsFileValidate(ph.FileName))
                            {
                                ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";
                                return View(tripReport);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.msg = "الحد الأقل المسموح به  5 صور والأعلى 10 صور";
                        return View(tripReport);
                    }

                }


                var repo = new TripReport()
                {
                    Id=tripReport.Id,
                    TripBookingId = tripReport.TripBookingId,
                    TextBody = tripReport.TextBody
                };
                _context.Update(repo);
                await _context.SaveChangesAsync();

                if (tripReport.Photos != null) {
                    string uniqFileName = null;
                    var modelImagelist = new List<TripReportImage>();
                    string sImage_Folder = "reports";
                    string sPath_WebRoot = _ihostingEnvironment.WebRootPath;
                    string sPath_of_Target_Folder = sPath_WebRoot + "\\img\\" + sImage_Folder + "\\";
                    string sFile_Target_Original = sPath_of_Target_Folder + "Original\\";
                    if (tripReport.Photos.Count > 0 && tripReport.Photos != null)
                    {
                        var imagesFoDelete = await _context.TripReportImages.Where(i => i.TripReportId == repo.Id).ToListAsync();
                        foreach (var img in imagesFoDelete)
                        {
                            string OriginalDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
                            "img/reports/Original", img.ImagePath);
                            System.IO.File.Delete(OriginalDelete);

                            string Scale400lDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
                           "img/reports/400", img.ImagePath);
                            System.IO.File.Delete(Scale400lDelete);
                            _context.Remove(img);

                        }
                        await _context.SaveChangesAsync();

                        foreach (IFormFile photo in tripReport.Photos)
                        {
                            string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/reports/Original");
                            uniqFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                photo.CopyTo(fileStream);
                            }

                            Image_resize(sFile_Target_Original + uniqFileName, sPath_of_Target_Folder + "\\400\\" + uniqFileName, 400);

                            var modelImage = new TripReportImage()
                            {
                                TripReportId = repo.Id,
                                ImagePath = uniqFileName
                            };
                            modelImagelist.Add(modelImage);


                        }
                        _context.AddRangeAsync(modelImagelist);
                        await _context.SaveChangesAsync();
                    }

               

                }
                return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = repo.TripBookingId });
            

        }

                return View(tripReport);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/TripReports/Create/{bokingId:int}")]
        public async Task<IActionResult> Create(int bokingId, TripReportCreateModelView tripReport)
        {
            if (ModelState.IsValid)
            {

                if (tripReport.Photos.Count() >= 5 || tripReport.Photos.Count() > 10)
                {
                    foreach (var ph in tripReport.Photos)
                    {
                        if (!IsFileValidate(ph.FileName))
                        {
                            ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";
                            return View(tripReport);
                        }
                    }
                }
                else
                {
                    ViewBag.msg = "الحد الأقل المسموح به  5 صور والأعلى 10 صور";
                    return View(tripReport);
                }

                var repo = new TripReport()
                {
                    TripBookingId=tripReport.TripBookingId,
                    TextBody=tripReport.TextBody
                };
                _context.Add(repo);
                await _context.SaveChangesAsync();
                string uniqFileName = null;
                var modelImagelist =new List<TripReportImage>();

                string sImage_Folder = "reports";
                string sPath_WebRoot = _ihostingEnvironment.WebRootPath;
                string sPath_of_Target_Folder = sPath_WebRoot + "\\img\\" + sImage_Folder + "\\";
                string sFile_Target_Original = sPath_of_Target_Folder + "Original\\" ;

                foreach (IFormFile photo in tripReport.Photos)
                {
                    string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/reports/Original");
                    uniqFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                  
                    Image_resize(sFile_Target_Original+ uniqFileName, sPath_of_Target_Folder + "\\400\\" + uniqFileName, 400);

                    var modelImage = new TripReportImage()
                    {
                        TripReportId= repo.Id,ImagePath=uniqFileName
                    };
                    modelImagelist.Add(modelImage);

                  
                }
                _context.AddRangeAsync(modelImagelist);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsMore", "SchedulingTripDetails",new { id=repo.TripBookingId});
            }
           
            return View(tripReport);
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


        private void Image_resize(string input_Image_Path, string output_Image_Path, int new_Width )

        {

            //---------------< Image_resize() >---------------

            //*Resizes an Image in Asp.Net MVC Core 2

            //*Using Nuget CoreCompat.System.Drawing

            //using System.IO

            //using System.Drawing;             //CoreCompat

            //using System.Drawing.Drawing2D;   //CoreCompat

            //using System.Drawing.Imaging;     //CoreCompat



            const long quality = 50L;

            Bitmap source_Bitmap = new Bitmap(input_Image_Path);



            double dblWidth_origial = source_Bitmap.Width;

            double dblHeigth_origial = source_Bitmap.Height;

            double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

            int new_Height = (int)(new_Width * relation_heigth_width);



            //< create Empty Drawarea >

            var new_DrawArea = new Bitmap(new_Width, new_Height);

            //</ create Empty Drawarea >



            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))

            {

                //< setup >

                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

                //</ setup >



                //< draw into placeholder >

                //*imports the image into the drawarea

                graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_Width, new_Height);

                //</ draw into placeholder >



                //--< Output as .Jpg >--

                using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))

                {

                    //< setup jpg >

                    var qualityParamId = Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    //</ setup jpg >



                    //< save Bitmap as Jpg >

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    new_DrawArea.Save(output, codec, encoderParameters);

                    //resized_Bitmap.Dispose();

                    output.Close();

                    //</ save Bitmap as Jpg >

                }

                //--</ Output as .Jpg >--

                graphic_of_DrawArea.Dispose();

            }

            source_Bitmap.Dispose();

            //---------------</ Image_resize() >---------------

        }

        private bool TripReportExists(int id)
        {
            return _context.TripReports.Any(e => e.Id == id);
        }

        [Route("Sociologist/TripReports/StatusUpdate")]
        [HttpPost]
        public async Task<IActionResult> StatusUpdate(int reportId,int Status, int bookingId)
        {
            if (Status > 0)
            {
                var report = await _context.TripReports.FindAsync(reportId);
                report.Status = Status;
                _context.Update(report);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.StatusMessage = "يجب اختيار حالة للتقرير";
            }
        
            return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = bookingId });

        }

    }
}
