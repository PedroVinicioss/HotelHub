using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220004)]
public class CreateChannelsTable : Migration
{
    public override void Up()
    {
        Create.Table("channels")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("hotel_id").AsGuid().NotNullable()
                .ForeignKey("FK_channels_hotels", "hotels", "id")
            .WithColumn("platform_type").AsString(50).NotNullable()
            .WithColumn("external_account_id").AsString(300).NotNullable()
            .WithColumn("access_token_encrypted").AsString(1000).NotNullable()
            .WithColumn("token_expires_at").AsDateTimeOffset().Nullable()
            .WithColumn("webhook_verify_token").AsString(500).Nullable()
            .WithColumn("status").AsString(50).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("channels");
    }
}
