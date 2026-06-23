using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220006)]
public class CreateContactChannelsTable : Migration
{
    public override void Up()
    {
        Create.Table("contact_channels")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("contact_id").AsGuid().NotNullable()
                .ForeignKey("FK_contact_channels_contacts", "contacts", "id")
            .WithColumn("channel_id").AsGuid().NotNullable()
                .ForeignKey("FK_contact_channels_channels", "channels", "id")
            .WithColumn("external_contact_id").AsString(300).NotNullable()
            .WithColumn("last_seen_at").AsDateTimeOffset().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();

        Create.UniqueConstraint("UQ_contact_channels_channel_external")
            .OnTable("contact_channels")
            .Columns("channel_id", "external_contact_id");
    }

    public override void Down()
    {
        Delete.Table("contact_channels");
    }
}
