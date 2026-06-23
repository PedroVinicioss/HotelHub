using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220010)]
public class CreateBookingsTable : Migration
{
    public override void Up()
    {
        Create.Table("bookings")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("hotel_id").AsGuid().NotNullable()
                .ForeignKey("FK_bookings_hotels", "hotels", "id")
            .WithColumn("contact_id").AsGuid().NotNullable()
                .ForeignKey("FK_bookings_contacts", "contacts", "id")
            .WithColumn("conversation_id").AsGuid().Nullable()
                .ForeignKey("FK_bookings_conversations", "conversations", "id")
            .WithColumn("check_in_date").AsDate().NotNullable()
            .WithColumn("check_out_date").AsDate().NotNullable()
            .WithColumn("value").AsDecimal(18, 2).NotNullable()
            .WithColumn("closed_at").AsDateTimeOffset().Nullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("bookings");
    }
}
