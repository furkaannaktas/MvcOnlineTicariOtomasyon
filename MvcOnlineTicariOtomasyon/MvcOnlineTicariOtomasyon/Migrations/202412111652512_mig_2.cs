namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personels", "Departman_Departmanid", "dbo.Departmen");
            DropIndex("dbo.Personels", new[] { "Departman_Departmanid" });
            RenameColumn(table: "dbo.Personels", name: "Departman_Departmanid", newName: "Departmanid");
            AlterColumn("dbo.Personels", "Departmanid", c => c.Int(nullable: false));
            CreateIndex("dbo.Personels", "Departmanid");
            AddForeignKey("dbo.Personels", "Departmanid", "dbo.Departmen", "Departmanid", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personels", "Departmanid", "dbo.Departmen");
            DropIndex("dbo.Personels", new[] { "Departmanid" });
            AlterColumn("dbo.Personels", "Departmanid", c => c.Int());
            RenameColumn(table: "dbo.Personels", name: "Departmanid", newName: "Departman_Departmanid");
            CreateIndex("dbo.Personels", "Departman_Departmanid");
            AddForeignKey("dbo.Personels", "Departman_Departmanid", "dbo.Departmen", "Departmanid");
        }
    }
}
