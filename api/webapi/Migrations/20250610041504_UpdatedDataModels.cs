using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDataModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    event_description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    event_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_end_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.event_id);
                });

            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    park_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    park_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    geolocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    street_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    zipcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    number_of_pavillions = table.Column<int>(type: "int", nullable: false),
                    acres = table.Column<int>(type: "int", nullable: false),
                    play_structures = table.Column<int>(type: "int", nullable: false),
                    trails = table.Column<int>(type: "int", nullable: false),
                    baseball_fields = table.Column<int>(type: "int", nullable: false),
                    disc_golf_courses = table.Column<int>(type: "int", nullable: false),
                    volleyball_courts = table.Column<int>(type: "int", nullable: false),
                    fishing_spots = table.Column<int>(type: "int", nullable: false),
                    soccer_fields = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.park_id);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    registration_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    park_id = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<int>(type: "int", nullable: false),
                    requested_park = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    pavillion = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    waitlist = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.registration_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    is_admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Parks");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
