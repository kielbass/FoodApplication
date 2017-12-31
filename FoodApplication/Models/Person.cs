using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Models
{
    public class Person
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public float Weight { get; set; }
        public float Bilans { get; set; }
        public float Proteins { get; set; }
    }
}
