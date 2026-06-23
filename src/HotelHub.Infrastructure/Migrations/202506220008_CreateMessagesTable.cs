using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220008)]
public class CreateMessagesTable : Migration
{
    public override void Up()
    {
        Create.Table("messages")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("conversation_id").AsGuid().NotNullable()
                .ForeignKey("FK_messages_conversations", "conversations", "id")
            .WithColumn("direction").AsString(20).NotNullable()
            .WithColumn("type").AsString(30).NotNullable()
            .WithColumn("content").AsString(int.MaxValue).Nullable()
            .WithColumn("media_url").AsString(2000).Nullable()
            .WithColumn("media_type").AsString(100).Nullable()
            .WithColumn("media_size").AsInt32().Nullable()
            .WithColumn("external_message_id").AsString(300).Nullable()
            .WithColumn("status").AsString(30).NotNullable()
            .WithColumn("sent_at").AsDateTimeOffset().NotNullable()
            .WithColumn("sender_user_id").AsGuid().Nullable()
                .ForeignKey("FK_messages_users", "users", "id")
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("messages");
    }
}
