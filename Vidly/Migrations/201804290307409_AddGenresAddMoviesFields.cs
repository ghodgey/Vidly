namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenresAddMoviesFields : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Movies");
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "GenresId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Movies", "Id");
            CreateIndex("dbo.Movies", "GenresId");
            AddForeignKey("dbo.Movies", "GenresId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenresId", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "GenresId" });
            DropPrimaryKey("dbo.Movies");
            AlterColumn("dbo.Movies", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "GenresId");
            DropTable("dbo.Genres");
            AddPrimaryKey("dbo.Movies", "Id");
        }
    }
}
