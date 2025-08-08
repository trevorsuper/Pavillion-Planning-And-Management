using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class FixEventForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FK and index related to 'user_id1' if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.foreign_keys 
                    WHERE name = 'FK_Events_Users_user_id1' 
                      AND parent_object_id = OBJECT_ID('[dbo].[Events]')
                )
                ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Users_user_id1];
                
                IF EXISTS (
                    SELECT 1 FROM sys.indexes 
                    WHERE name = 'IX_Events_user_id1' 
                      AND object_id = OBJECT_ID('[dbo].[Events]')
                )
                DROP INDEX [IX_Events_user_id1] ON [dbo].[Events];
                
                IF EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'user_id1' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                )
                ALTER TABLE [dbo].[Events] DROP COLUMN [user_id1];
            ");

            // Drop unnecessary columns if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'is_public_event' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                )
                ALTER TABLE [dbo].[Events] DROP COLUMN [is_public_event];
                
                IF EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'num_of_attendees' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                )
                ALTER TABLE [dbo].[Events] DROP COLUMN [num_of_attendees];
            ");

            // Rename event_description to event_desc if needed
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'event_description' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                ) AND NOT EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'event_desc' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                )
                EXEC sp_rename 'dbo.Events.event_description', 'event_desc', 'COLUMN';
            ");

            // Add is_reviewed column to Registration if missing
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'is_reviewed' 
                      AND Object_ID = Object_ID(N'[dbo].[Registration]')
                )
                ALTER TABLE [dbo].[Registration] ADD [is_reviewed] bit NOT NULL DEFAULT(0);
            ");

            // Change pavillion type in Registration from int to tinyint safely
            migrationBuilder.AlterColumn<byte>(
                name: "pavillion",
                table: "Registration",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Alter end_time column type in Registration if column exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'end_time' 
                      AND Object_ID = Object_ID(N'[dbo].[Registration]')
                )
                ALTER TABLE [dbo].[Registration] ALTER COLUMN [end_time] time NOT NULL;
            ");

            // Add requested_park column to Registration if missing
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'requested_park' 
                      AND Object_ID = Object_ID(N'[dbo].[Registration]')
                )
                ALTER TABLE [dbo].[Registration] ADD [requested_park] nvarchar(50) NULL;
            ");

            // Alter event_start_time and event_end_time to time in Events
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "event_start_time",
                table: "Events",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "event_end_time",
                table: "Events",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            // Create index on registration_id if it doesn't exist
            migrationBuilder.CreateIndex(
                name: "IX_Events_registration_id",
                table: "Events",
                column: "registration_id");

            // Ensure user_id column exists before creating index and FK
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'user_id' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                )
                BEGIN
                    ALTER TABLE [dbo].[Events] ADD [user_id] int NOT NULL DEFAULT(0);
                    -- Remove default constraint after adding the column
                    DECLARE @ConstraintName sysname;
                    SELECT @ConstraintName = dc.name
                    FROM sys.default_constraints dc
                    JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
                    WHERE c.object_id = OBJECT_ID('[dbo].[Events]') AND c.name = 'user_id';
                    IF @ConstraintName IS NOT NULL
                        EXEC('ALTER TABLE [dbo].[Events] DROP CONSTRAINT [' + @ConstraintName + ']');
                END
            ");

            // Create index on user_id if missing
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes 
                    WHERE name = 'IX_Events_user_id' 
                      AND object_id = OBJECT_ID('[dbo].[Events]')
                )
                CREATE INDEX [IX_Events_user_id] ON [dbo].[Events] ([user_id]);
            ");

            // Make sure user_id is non-nullable int (if nullable, alter it)
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns 
                    WHERE Name = N'user_id' 
                      AND Object_ID = Object_ID(N'[dbo].[Events]')
                      AND is_nullable = 1
                )
                ALTER TABLE [dbo].[Events] ALTER COLUMN [user_id] int NOT NULL;
            ");

            // Drop FK if exists to avoid duplicates
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.foreign_keys 
                    WHERE name = 'FK_Events_Registration_registration_id' 
                      AND parent_object_id = OBJECT_ID('[dbo].[Events]')
                )
                ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Registration_registration_id];
                
                IF EXISTS (
                    SELECT 1 FROM sys.foreign_keys 
                    WHERE name = 'FK_Events_Users_user_id' 
                      AND parent_object_id = OBJECT_ID('[dbo].[Events]')
                )
                ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Users_user_id];
            ");

            // Add foreign keys with specified behaviors
            migrationBuilder.AddForeignKey(
                name: "FK_Events_Registration_registration_id",
                table: "Events",
                column: "registration_id",
                principalTable: "Registration",
                principalColumn: "registration_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_user_id",
                table: "Events",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Registration_registration_id",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_user_id",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_registration_id",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_user_id",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "is_reviewed",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "requested_park",
                table: "Registration");

            migrationBuilder.RenameColumn(
                name: "event_desc",
                table: "Events",
                newName: "event_description");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<int>(
                name: "pavillion",
                table: "Registration",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_time",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "event_start_time",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "event_end_time",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<bool>(
                name: "is_public_event",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "num_of_attendees",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id1",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_user_id1",
                table: "Events",
                column: "user_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_user_id",
                table: "Events",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

/*
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class FixEventForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 FROM sys.foreign_keys 
                WHERE name = 'FK_Events_Users_user_id1' 
                  AND parent_object_id = OBJECT_ID('[dbo].[Events]')
            )
            ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Users_user_id1];
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 FROM sys.indexes 
                WHERE name = 'IX_Events_user_id1' 
                  AND object_id = OBJECT_ID('[dbo].[Events]')
            )
            DROP INDEX [IX_Events_user_id1] ON [dbo].[Events];
            ");


            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 FROM sys.columns 
                WHERE Name = N'is_public_event' 
                  AND Object_ID = Object_ID(N'[dbo].[Events]')
            )
            ALTER TABLE [dbo].[Events] DROP COLUMN [is_public_event];
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 FROM sys.columns 
                WHERE Name = N'num_of_attendees' 
                  AND Object_ID = Object_ID(N'[dbo].[Events]')
            )
            ALTER TABLE [dbo].[Events] DROP COLUMN [num_of_attendees];
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 FROM sys.columns 
                WHERE Name = N'user_id1' 
                  AND Object_ID = Object_ID(N'[dbo].[Events]')
            )
            ALTER TABLE [dbo].[Events] DROP COLUMN [user_id1];
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 
                FROM sys.columns 
                WHERE Name = N'event_description' 
                  AND Object_ID = Object_ID(N'[dbo].[Events]')
            ) AND NOT EXISTS (
                SELECT 1 
                FROM sys.columns 
                WHERE Name = N'event_desc' 
                  AND Object_ID = Object_ID(N'[dbo].[Events]')
            )
            EXEC sp_rename 'dbo.Events.event_description', 'event_desc', 'COLUMN';
            ");

            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT 1 
                FROM sys.columns 
                WHERE Name = N'is_reviewed' 
                  AND Object_ID = Object_ID(N'[dbo].[Registration]')
            )
            ALTER TABLE [dbo].[Registration] ADD [is_reviewed] bit NOT NULL DEFAULT(0);
            ");

            migrationBuilder.AlterColumn<byte>(
                name: "pavillion",
                table: "Registration",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 
                FROM sys.columns 
                WHERE Name = N'end_time' 
                  AND Object_ID = Object_ID(N'[dbo].[Registration]')
            )
            ALTER TABLE [dbo].[Registration] ALTER COLUMN [end_time] time NOT NULL;
            ");

            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT 1 
                FROM sys.columns 
                WHERE Name = N'requested_park' 
                  AND Object_ID = Object_ID(N'[dbo].[Registration]')
            )
            ALTER TABLE [dbo].[Registration] ADD [requested_park] nvarchar(50) NULL;
            ");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "event_start_time",
                table: "Events",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "event_end_time",
                table: "Events",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Events_registration_id",
                table: "Events",
                column: "registration_id");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT 1 FROM sys.columns 
                WHERE Name = N'user_id' 
                  AND Object_ID = Object_ID(N'[dbo].[Events]')
            )
            IF NOT EXISTS (
                SELECT 1 FROM sys.indexes 
                WHERE name = 'IX_Events_user_id'
                  AND object_id = OBJECT_ID('[dbo].[Events]')
            )
            CREATE INDEX [IX_Events_user_id] ON [dbo].[Events] ([user_id]);
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Registration_registration_id",
                table: "Events",
                column: "registration_id",
                principalTable: "Registration",
                principalColumn: "registration_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Events",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_user_id",
                table: "Events",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Registration_registration_id",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_user_id",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_registration_id",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_user_id",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "is_reviewed",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "requested_park",
                table: "Registration");

            migrationBuilder.RenameColumn(
                name: "event_desc",
                table: "Events",
                newName: "event_description");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<int>(
                name: "pavillion",
                table: "Registration",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_time",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "event_start_time",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "event_end_time",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<bool>(
                name: "is_public_event",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "num_of_attendees",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id1",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_user_id1",
                table: "Events",
                column: "user_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_user_id1",
                table: "Events",
                column: "user_id1",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
*/