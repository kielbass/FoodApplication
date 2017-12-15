using FoodApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Classes
{
    class ReadyMeal
    {
        
        public List<FoodIdWeightPair> FoodAndWeightList {get;set; } //pair of foodId and weight
        public Meal Origin { get; set; }


        //Summed
        public float Kcal { get; private set; }
        public float Proteins { get; private set; }
        public float Carbs { get; private set; }
        public float Fat { get; private set; }

        public ReadyMeal(Meal meal)
        {
            FoodAndWeightList = new List<FoodIdWeightPair>();
            Origin = meal;
            FoodAndWeightList = (List<FoodIdWeightPair>)JsonConvert.DeserializeObject<IEnumerable<FoodIdWeightPair>>(Origin.FoodsAndWeightJson);
        }
        public void Save()
        {
            Origin.FoodsAndWeightJson = CreateJson();
        }
        private string CreateJson()
        {
            return JsonConvert.SerializeObject(FoodAndWeightList);
        }
        public override string ToString()
        {
            string str = "";
            foreach(FoodIdWeightPair f in FoodAndWeightList)
            {
                str += "FoodId: ";
                str += f.FoodId.ToString();
                str += " Waga: ";
                str += f.FoodWeight.ToString();
            }
            return string.Format("{0} {1} \n{2}\n", Origin.Name, Origin.Date, str);
        }
    }
}
