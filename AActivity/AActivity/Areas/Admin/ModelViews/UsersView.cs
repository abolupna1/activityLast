using AActivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.ModelViews
{
    public class UsersView
    {

        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<AppRole> Roles { get; set; }
    }
}
