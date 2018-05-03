using System.Data.Entity.Core.Metadata.Edm;

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE ID = 1");
            Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE ID = 2");
            Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE ID = 3");
            Sql("UPDATE MembershipTypes SET Name = 'Yearly' WHERE ID = 4");
        }
        
        public override void Down()
        {
        }
    }
}
