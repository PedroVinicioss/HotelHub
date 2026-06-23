using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506220005)]
public class CreateContactsTable : Migration
{
    public override void Up()
    {
        Create.Table("contacts")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("hotel_id").AsGuid().NotNullable()
                .ForeignKey("FK_contacts_hotels", "hotels", "id")
            .WithColumn("name").AsString(200).NotNullable()
            .WithColumn("phone").AsString(30).Nullable()
            .WithColumn("email").AsString(300).Nullable()
            .WithColumn("cpf").AsString(14).Nullable()
            .WithColumn("status").AsString(50).NotNullable()
            .WithColumn("first_contact_at").AsDateTimeOffset().NotNullable()
            .WithColumn("last_interaction_at").AsDateTimeOffset().NotNullable()
            .WithColumn("merged_into_contact_id").AsGuid().Nullable()
                .ForeignKey("FK_contacts_merged_contact", "contacts", "id")
            .WithColumn("anonymized_at").AsDateTimeOffset().Nullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("contacts");
    }
}
