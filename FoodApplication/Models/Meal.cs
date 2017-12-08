using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Models
{
    partial class Meal
    {
        public long MealId { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }

        public string FoodsAndWeightJson { get; set; }


        //public long Food0 { get; set; }
        //public long Food1 { get; set; }
        //public long Food2 { get; set; }
        //public long Food3 { get; set; }
        //public long Food4 { get; set; }
        //public long Food5 { get; set; }
        //public long Food6 { get; set; }
        //public long Food7 { get; set; }
        //public long Food8 { get; set; }
        //public long Food9 { get; set; }

        //public float Weight0 { get; set; }
        //public float Weight1 { get; set; }
        //public float Weight2 { get; set; }
        //public float Weight3 { get; set; }
        //public float Weight4 { get; set; }
        //public float Weight5 { get; set; }
        //public float Weight6 { get; set; }
        //public float Weight7 { get; set; }
        //public float Weight8 { get; set; }
        //public float Weight9 { get; set; }



    }
}
