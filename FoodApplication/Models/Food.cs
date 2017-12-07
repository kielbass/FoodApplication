using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Models
{
    public class Food
    {
        public long FoodId { get; set; }
        public float Kcal { get; set; }
        public string Name { get; set; }
        public float Proteins { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
        public bool IsUsed { get; set; }
        public bool Package { get; set; } //czy jest zapakowane
        public float PackageWeight { get; set; } // wagao pakowania

        public string Kind { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}/100g Rodzaj:{2}", Name, Kcal.ToString(), Kind);
        }

    }
}
