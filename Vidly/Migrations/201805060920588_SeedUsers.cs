namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'17528e14-7ae2-4cd7-9e6e-17f468cb4c67', N'admin@vidly.com', 0, N'AC0yZ780zR2dK/5bDYJm7sh+8pLWacngrVMG4uzQcBos8dcEd2uP2M62ExmIr0pmRQ==', N'f5076e22-2a5d-43f3-8bca-ebaae609e8b5', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6df7c121-8f5a-45d4-b81d-5a1ce7979a29', N'guest@vidly.com', 0, N'ALx89XVy1OMcsElp+Bzd0kb3iS3LZrbf6lCWl8wFd5+S+abtxZ3Q5GqAj/tKkqcH8w==', N'978b7271-9ff3-423d-8fd5-e6326dfe15fa', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f4d551f0-5795-45c5-8462-6bffe4690d07', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'17528e14-7ae2-4cd7-9e6e-17f468cb4c67', N'f4d551f0-5795-45c5-8462-6bffe4690d07')
");
        }
        
        public override void Down()
        {
        }
    }
}
