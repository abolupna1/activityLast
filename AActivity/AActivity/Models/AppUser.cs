using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<EducationalBody> EducationalBodies { get; set; }
        public ICollection<SchedulingTripHead> SchedulingTripHeads { get; set; }
        public ICollection<Signature> Signatures { get; set; }
       // public IEnumerable<SignutreDelegate> SignutreDelegates { get; set; }
        public IEnumerable<LetterTransport> LetterTransports { get; set; }
        public IEnumerable<LetterFood> LetterFood { get; set; }
        public IEnumerable<NotificationLetter> NotificationLetters { get; set; }



    }
}
