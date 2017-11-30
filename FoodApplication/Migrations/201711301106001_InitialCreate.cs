namespace FoodApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Long(nullable: false, identity: true),
                        Kcal = c.Single(nullable: false),
                        Name = c.String(),
                        Proteins = c.Single(nullable: false),
                        Carbs = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Kind = c.String(),
                    })
                .PrimaryKey(t => t.FoodId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealId = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                        Food0Id = c.Long(nullable: false),
                        Food1Id = c.Long(nullable: false),
                        Food2Id = c.Long(nullable: false),
                        Food3Id = c.Long(nullable: false),
                        Food4Id = c.Long(nullable: false),
                        Food5Id = c.Long(nullable: false),
                        Food6Id = c.Long(nullable: false),
                        Food7Id = c.Long(nullable: false),
                        Food8Id = c.Long(nullable: false),
                        Food9Id = c.Long(nullable: false),
                        Weight0 = c.Single(nullable: false),
                        Weight1 = c.Single(nullable: false),
                        Weight2 = c.Single(nullable: false),
                        Weight3 = c.Single(nullable: false),
                        Weight4 = c.Single(nullable: false),
                        Weight5 = c.Single(nullable: false),
                        Weight6 = c.Single(nullable: false),
                        Weight7 = c.Single(nullable: false),
                        Weight8 = c.Single(nullable: false),
                        Weight9 = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.MealId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meals");
            DropTable("dbo.Foods");
        }
    }
}
