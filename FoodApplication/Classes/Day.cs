using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Classes
{
    class Day
    {
        public float Kcal { get; private set; }
        public float Proteins { get; private set; }
        public float Carbs { get; private set; }
        public float Fat { get; private set; }

        public float KcalPlan { get; private set; }
        public float ProteinsPlan { get; private set; }
        public float CarbsPlan { get; private set; }
        public float FatPlan { get; private set; }

        public Models.Person Person {get;set;}

        public List<ReadyMeal> ReadyMeals { get; set; }


    }
}
