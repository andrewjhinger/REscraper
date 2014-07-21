namespace REscraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.REproperty",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddDateAndTime = c.DateTime(nullable: false),
                        CaseNumber = c.String(),
                        PropertyAddress = c.String(),
                        City = c.String(),
                        County = c.String(),
                        Zip = c.String(),
                        Plaintiff = c.String(),
                        Defendant = c.String(),
                        Notes = c.String(),
                        ForclosureType = c.String(),
                        ParcelNumber = c.String(),
                        PlantiffAtty = c.String(),
                        AppraisedValue = c.Int(nullable: false),
                        JudgmentValue = c.Int(nullable: false),
                        OpeningBid = c.Int(nullable: false),
                        Deposit = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ZestimateHigh = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ZestimateLow = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ZillowId = c.Int(nullable: false),
                        LinktoMap = c.String(),
                        LinktoHomeDetails = c.String(),
                        LinktoGraphsAndData = c.String(),
                        LinktoComparables = c.String(),
                        Estimate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValueChange = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValueChangeDurationInDays = c.Int(nullable: false),
                        ChartUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.REproperty");
        }
    }
}
