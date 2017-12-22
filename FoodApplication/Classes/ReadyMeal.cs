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
        private static FoodContext _db;

        //Summed
        public float Kcal { get; private set; }
        public float Proteins { get; private set; }
        public float Carbs { get; private set; }
        public float Fat { get; private set; }
        /// <summary>
        /// Constructor from Database
        /// </summary>
        /// <param name="meal"></param>
        public ReadyMeal(Meal meal, FoodContext foodContext)
        {
            _db = foodContext;
            FoodAndWeightList = new List<FoodIdWeightPair>();
            Origin = meal;
            FoodAndWeightList = (List<FoodIdWeightPair>)JsonConvert.DeserializeObject<IEnumerable<FoodIdWeightPair>>(Origin.FoodsAndWeightJson);
            Reset();
        }
        /// <summary>
        /// Constructor from meal creating
        /// </summary>
        public ReadyMeal(string n,DateTime? d, List<FoodIdWeightPair> list,FoodContext foodContext)
        {
            Origin = new Meal();
            _db = foodContext;
            Origin.Name = n;
            Origin.Date = d;
            FoodAndWeightList = list;
            Reset();
        }
        private void Reset()
        {
            Kcal = 0;
            Proteins = 0;
            Carbs = 0;
            Fat = 0;
            foreach (FoodIdWeightPair fp in FoodAndWeightList)
            {
                Food[] temp = _db.Foods.Select(x => x).Where(x => x.FoodId == fp.FoodId).ToArray();
                if(temp[0].Package)
                {
                    Kcal += ((temp[0].PackageWeight * temp[0].Kcal) / 100)*fp.FoodWeight;
                    Proteins += ((temp[0].PackageWeight * temp[0].Proteins) / 100) * fp.FoodWeight;
                    Fat += ((temp[0].PackageWeight * temp[0].Fat) / 100) * fp.FoodWeight;
                    Carbs += ((temp[0].PackageWeight * temp[0].Carbs) / 100) * fp.FoodWeight;
                }
                else
                {
                    Kcal += (temp[0].Kcal / 100) * fp.FoodWeight;
                    Proteins += (temp[0].Proteins / 100) * fp.FoodWeight;
                    Fat += (temp[0].Fat / 100) * fp.FoodWeight;
                    Carbs += (temp[0].Carbs / 100) * fp.FoodWeight;
                }
            }
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
