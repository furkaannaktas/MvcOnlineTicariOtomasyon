namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_departman : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Personels", "Departman_Departmanid", c => c.Int());
            CreateIndex("dbo.Personels", "Departman_Departmanid");
            AddForeignKey("dbo.Personels", "Departman_Departmanid", "dbo.Departmen", "Departmanid");
            DropColumn("dbo.Personels", "Departmanid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personels", "Departmanid", c => c.Int(nullable: false));
            DropForeignKey("dbo.Personels", "Departman_Departmanid", "dbo.Departmen");
            DropIndex("dbo.Personels", new[] { "Departman_Departmanid" });
            DropColumn("dbo.Personels", "Departman_Departmanid");
            DropTable("dbo.Departmen");
        }
    }
}
