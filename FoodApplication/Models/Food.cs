using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Models
{
    class Food
    {
        public long FoodId { get; set; }
        public float Kcal { get; set; }
        public string Name { get; set; }
        public float Proteins { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }

        public string Kind { get; set; }

    }
}
