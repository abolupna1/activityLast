using AActivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace AActivity.Areas.Sociologist.ModelViews
{
    public class StatisticsModelView
    {
        public EducationalBody EducationBody { get; set; }
        public List<LetterTransport> letterTransport { get; set; }
        public int ExtirnalCount { get; set; }
        public int InternalCount { get; set; }
        public int ZiaraCount { get; set; }
        public int StudentCount { get; set; }
        public int DelegationCount { get; set; }
        public float AdvanceCosts { get; set; }
        public int BusesCount { get; set; }
        public int MealsCount { get; set; }




    }
}
