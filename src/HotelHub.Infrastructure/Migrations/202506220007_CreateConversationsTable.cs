using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220007)]
public class CreateConversationsTable : Migration
{
    public override void Up()
    {
        Create.Table("conversations")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("hotel_id").AsGuid().NotNullable()
                .ForeignKey("FK_conversations_hotels", "hotels", "id")
            .WithColumn("contact_id").AsGuid().NotNullable()
                .ForeignKey("FK_conversations_contacts", "contacts", "id")
            .WithColumn("channel_id").AsGuid().NotNullable()
                .ForeignKey("FK_conversations_channels", "channels", "id")
            .WithColumn("status").AsString(50).NotNullable()
            .WithColumn("assigned_user_id").AsGuid().Nullable()
                .ForeignKey("FK_conversations_users", "users", "id")
            .WithColumn("last_message_at").AsDateTimeOffset().NotNullable()
            .WithColumn("first_response_at").AsDateTimeOffset().Nullable()
            .WithColumn("window_opened_at").AsDateTimeOffset().Nullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("conversations");
    }
}
