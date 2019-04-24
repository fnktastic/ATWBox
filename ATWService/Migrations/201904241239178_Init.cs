namespace ATWService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LastSeenLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadingId = c.Guid(nullable: false),
                        LastSeenAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Host = c.String(),
                        Port = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Readings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReaderNumber = c.String(),
                        IPAddress = c.String(),
                        TimingPoint = c.String(),
                        TotalReads = c.Int(nullable: false),
                        FileName = c.String(),
                        StartedDateTime = c.DateTime(),
                        EndedDateTime = c.DateTime(),
                        RaceName = c.String(),
                        UserName = c.String(),
                        ReaderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Readers", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.ReaderId);
            
            CreateTable(
                "dbo.Reads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EPC = c.String(),
                        Time = c.DateTime(nullable: false),
                        Signal = c.String(),
                        AntennaNumber = c.String(),
                        ReadingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Readings", t => t.ReadingId, cascadeDelete: true)
                .Index(t => t.ReadingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reads", "ReadingId", "dbo.Readings");
            DropForeignKey("dbo.Readings", "ReaderId", "dbo.Readers");
            DropIndex("dbo.Reads", new[] { "ReadingId" });
            DropIndex("dbo.Readings", new[] { "ReaderId" });
            DropTable("dbo.Reads");
            DropTable("dbo.Readings");
            DropTable("dbo.Readers");
            DropTable("dbo.LastSeenLogs");
        }
    }
}
