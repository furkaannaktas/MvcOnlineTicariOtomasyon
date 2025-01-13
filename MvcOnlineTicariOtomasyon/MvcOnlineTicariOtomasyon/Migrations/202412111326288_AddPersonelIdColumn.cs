namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonelIdColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personels", "Departmanid", "dbo.Departmen");
            DropIndex("dbo.Personels", new[] { "Departmanid" });
            DropTable("dbo.Departmen");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Departmen",
                c => new
                    {
                        Departmanid = c.Int(nullable: false, identity: true),
                        DepartmanAd = c.String(maxLength: 30, unicode: false),
                        Durum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Departmanid);
            
            CreateIndex("dbo.Personels", "Departmanid");
            AddForeignKey("dbo.Personels", "Departmanid", "dbo.Departmen", "Departmanid", cascadeDelete: true);
        }
    }
}
