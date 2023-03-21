namespace TNAI.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RenterName = c.String(),
                        RentingDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RentalProducts",
                c => new
                    {
                        Rental_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rental_Id, t.Product_Id })
                .ForeignKey("dbo.Rentals", t => t.Rental_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Rental_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentalProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.RentalProducts", "Rental_Id", "dbo.Rentals");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.RentalProducts", new[] { "Product_Id" });
            DropIndex("dbo.RentalProducts", new[] { "Rental_Id" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.RentalProducts");
            DropTable("dbo.Rentals");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
