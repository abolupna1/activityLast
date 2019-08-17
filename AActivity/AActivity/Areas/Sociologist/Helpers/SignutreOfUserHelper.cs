using AActivity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Models;

namespace AActivity.Areas.Sociologist.Helpers
{
    public class SignutreOfUserHelper
    {
       
       

        public static string signatureRoleOfUser(int userid)
        {
          
            return "rdtfghjk";
        }

        public static int   getUserSignutre(int userId, ApplicationDbContext context)
        {
          var signutre =   context.Signatures.FirstOrDefault(u => u.UserId == userId);
            return signutre == null ? 0 : signutre.Id;
        }


        public static bool getUserStatusSignutre(int userId, ApplicationDbContext context)
        {
            if (getUserSignutre(userId, context) > 0)
                return true;
            return false;
        }

        public static bool getUserHasSignutre(int userId,int signatureId, ApplicationDbContext context)
        {
            return context.SignutreDelegates.Any(s => s.SignatureId == signatureId && s.UserId == userId);
          
        }

        public static bool getUserHasToEditSignutre(int id,int userId, int signatureId, ApplicationDbContext context)
        {
            return context.SignutreDelegates.Any(s =>s.Id==id && s.SignatureId == signatureId && s.UserId == userId);

        }

        public static async Task<IEnumerable<Signature>> getUsersHasSignutre(ApplicationDbContext context)
        {
            var bb = await context.Signatures.Include(u => u.User).ToListAsync();
            return bb;
        }
    }
}
