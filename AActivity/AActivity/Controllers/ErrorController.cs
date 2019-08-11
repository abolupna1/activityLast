using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AActivity.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResault = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.errorMessage = "المصدري الذي تبحث عنه غير موجود";
                    ViewBag.Path = statusCodeResault.OriginalPath;
                    ViewBag.QS = statusCodeResault.OriginalQueryString;
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            ViewBag.errorMessage = "الرجاء الإتصال بالمسؤول ";
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.ExceptionStacktrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}
