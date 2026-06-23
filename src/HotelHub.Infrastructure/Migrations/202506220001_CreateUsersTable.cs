using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220001)]
public class CreateUsersTable : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString(200).NotNullable()
            .WithColumn("email").AsString(300).NotNullable().Unique()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("role").AsString(50).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}
