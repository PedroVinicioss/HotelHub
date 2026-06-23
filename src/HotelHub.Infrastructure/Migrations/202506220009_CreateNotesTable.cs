using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220009)]
public class CreateNotesTable : Migration
{
    public override void Up()
    {
        Create.Table("notes")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("conversation_id").AsGuid().NotNullable()
                .ForeignKey("FK_notes_conversations", "conversations", "id")
            .WithColumn("user_id").AsGuid().NotNullable()
                .ForeignKey("FK_notes_users", "users", "id")
            .WithColumn("content").AsString(int.MaxValue).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("notes");
    }
}
