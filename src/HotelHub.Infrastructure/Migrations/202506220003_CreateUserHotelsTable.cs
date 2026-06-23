using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220003)]
public class CreateUserHotelsTable : Migration
{
    public override void Up()
    {
        Create.Table("user_hotels")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("user_id").AsGuid().NotNullable()
                .ForeignKey("FK_user_hotels_users", "users", "id")
            .WithColumn("hotel_id").AsGuid().NotNullable()
                .ForeignKey("FK_user_hotels_hotels", "hotels", "id")
            .WithColumn("hotel_role").AsString(50).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("user_hotels");
    }
}
