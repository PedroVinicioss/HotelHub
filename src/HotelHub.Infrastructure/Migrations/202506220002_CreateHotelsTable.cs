using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220002)]
public class CreateHotelsTable : Migration
{
    public override void Up()
    {
        Create.Table("hotels")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString(300).NotNullable()
            .WithColumn("tax_id").AsString(20).NotNullable()
            .WithColumn("tax_type").AsString(10).NotNullable()
            .WithColumn("city").AsString(150).NotNullable()
            .WithColumn("state").AsString(100).NotNullable()
            .WithColumn("country").AsString(100).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("hotels");
    }
}
