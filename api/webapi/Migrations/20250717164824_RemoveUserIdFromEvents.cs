using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class RemoveUserIdFromEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Drop the FK constraint on Events.user_id
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users",
                table: "Events");

            // 2) Drop the index on Events.user_id (if one was created)
            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT name FROM sys.indexes 
                WHERE name = 'IX_Events_user_id' AND object_id = OBJECT_ID('[dbo].[Events]')
            )
            BEGIN
                DROP INDEX [IX_Events_user_id] ON [dbo].[Events];
            END
            ");

            // 3) Finally drop the column itself
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add the column in case you ever roll back
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Events",
                type: "int",
                nullable: true);

            // Re-create the index
            migrationBuilder.CreateIndex(
                name: "IX_Events_user_id",
                table: "Events",
                column: "user_id");

            // Re-create the FK
            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_user_id",
                table: "Events",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");
        }
    }
}

