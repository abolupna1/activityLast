using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.ModelViews
{
    public class UserCreate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب"),Display(Name ="الإسم")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب"), Display(Name = "رقم الموظف ")]

        public string UserName { get; set; }
        [ Display(Name = "الإيميل"),EmailAddress]

        public string Email { get; set; }

        [Display(Name = "رقم الجوال"), Required(ErrorMessage = "الحقل مطلوب")]

        public string PhoneNumber { get; set; }

        public IList<AppRole> Roles { get; set; }
        public IList<AppUserRole> UserRoles { get; set; }

    }
}
