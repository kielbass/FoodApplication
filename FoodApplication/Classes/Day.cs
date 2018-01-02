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

        public string KcalText
        {
            get
            {
                return String.Format("{0} / {1}", Kcal, KcalPlan);
            }
        }
        public string ProteinsText
        {
            get
            {
                return String.Format("{0} / {1}", Proteins, ProteinsPlan);
            }
        }
        public string CarbsText
        {
            get
            {
                return String.Format("{0} / {1}", Carbs, CarbsPlan);
            }
        }
        public string FatText
        {
            get
            {
                return String.Format("{0} / {1}", Fat, FatPlan);
            }
        }
        public Models.Person Person {get;set;}

        public DateTime? Date { get; set; }
        public List<ReadyMeal> ReadyMeals { get; set; }
        
        public Day(DateTime? d)
        {
            Date = d;
            ReadyMeals = new List<ReadyMeal>();
        }

        public override string ToString()
        {
            return string.Format("{0}", Date.ToString());
        }
        public void CalculateByProteins(float factor)
        {
            Reset();
            KcalPlan = (Person.Weight * 24f * factor) - Person.Bilans;
            ProteinsPlan = Person.Weight * Person.Proteins;
            FatPlan = (0.25f * KcalPlan)/9f;
            CarbsPlan = (KcalPlan - ((FatPlan * 9f) + (ProteinsPlan * 4f))) / 4f;
        }
        private void Reset()
        {
                Kcal = 0;
                Proteins = 0;
                Carbs = 0;
                Fat = 0;

                KcalPlan = 0;
                ProteinsPlan = 0;
                CarbsPlan = 0;
                FatPlan = 0;
            foreach (ReadyMeal r in ReadyMeals)
            {
                Kcal += r.Kcal;
                Proteins += r.Proteins;
                Carbs += r.Carbs;
                Fat += r.Fat;
            }
        }
    }
}
