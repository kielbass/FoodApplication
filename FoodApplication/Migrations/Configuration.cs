namespace FoodApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodApplication.Models.FoodContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FoodApplication.Models.FoodContext";
        }

        protected override void Seed(FoodApplication.Models.FoodContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if(context.Foods.Count()==0)
            {
                context.Foods.Add(new Models.Food
                {
                    Name = "filet z piersi kurczaka",
                    Kcal = 116f,
                    Fat = 3.2f,
                    Proteins = 21.8f,
                    Carbs = 0f,
                    Package = false,
                    Kind = "miêso"
                });
            }
        }
    }
}
