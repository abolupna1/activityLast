using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class FinishWork
    {
        public int Id { get; set; }

        public int TripBookingId { get; set; }
        [ForeignKey("TripBookingId")]
        public TripBooking TripBooking { get; set; }

        public int TripDelegateId { get; set; }
        [ForeignKey("TripDelegateId")]
        public TripDelegate TripDelegate { get; set; }

        public string JopDegree { get; set; }
        public int DelegationNumber { get; set; }
        public DateTime DateDelgation { get; set; }
        public string DelegationBudy { get; set; }//[جهة الانتداب

        public int EndWorkDuration { get; set; }
        public bool TransportBuy { get; set; } 
        public bool LivingBuy { get; set; } 
        public bool FoodsBuy { get; set; } 
        public bool TransportToToWorkBuy { get; set; }
        public bool CashAdvance { get; set; }
        public float CashAdvanceAmont { get; set; }
    }
}
