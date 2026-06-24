using FluentMigrator;

namespace HotelHub.Infrastructure.Migrations;

[Migration(202506240011)]
public sealed class CreateRefreshTokensTable : Migration
{
    public override void Up()
    {
        Create.Table("RefreshTokens")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("UserId").AsGuid().NotNullable()
                .ForeignKey("FK_RefreshTokens_Users", "Users", "Id").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("Token").AsString(512).NotNullable().Unique()
            .WithColumn("IpAddress").AsString(45).Nullable()
            .WithColumn("ExpiresAt").AsDateTime2().NotNullable()
            .WithColumn("IsRevoked").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("RevokedAt").AsDateTime2().Nullable()
            .WithColumn("CreatedAt").AsDateTime2().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime2().Nullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true);

        Create.Index("IX_RefreshTokens_UserId").OnTable("RefreshTokens").OnColumn("UserId");
    }

    public override void Down()
    {
        Delete.Table("RefreshTokens");
    }
}
