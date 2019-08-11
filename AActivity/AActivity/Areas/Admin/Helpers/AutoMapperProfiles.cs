using AActivity.Areas.Admin.ModelViews;
using AActivity.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap< AppUser, UserCreate>().ReverseMap();

        }
    }
}
