﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class CleanEventSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Skip Parks table creation - assumed to exist

            // Conditionally create Users
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
                BEGIN
                    CREATE TABLE [Users] (
                        [user_id] int NOT NULL IDENTITY PRIMARY KEY,
                        [first_name] nvarchar(255) NOT NULL,
                        [last_name] nvarchar(255) NOT NULL,
                        [username] nvarchar(255) NOT NULL,
                        [password_hash] nvarchar(60) NOT NULL,
                        [email] nvarchar(50) NOT NULL,
                        [phone_number] nvarchar(50) NOT NULL,
                        [is_admin] bit NOT NULL
                    )
                END
            ");

            // Conditionally create Events
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Events')
                BEGIN
                    CREATE TABLE [Events] (
                        [event_id] int NOT NULL IDENTITY PRIMARY KEY,
                        [event_name] nvarchar(255) NOT NULL,
                        [event_description] nvarchar(4000) NOT NULL,
                        [event_start_date] datetime2 NOT NULL,
                        [event_end_date] datetime2 NOT NULL,
                        [event_start_time] datetime2 NOT NULL,
                        [event_end_time] datetime2 NOT NULL,
                        [user_id] int NULL,
                        CONSTRAINT [FK_Events_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id])
                    )
                END
            ");

            // Conditionally create Registration
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Registration')
                BEGIN
                    CREATE TABLE [Registration] (
                        [registration_id] int NOT NULL IDENTITY PRIMARY KEY,
                        [user_id] int NOT NULL,
                        [park_id] int NOT NULL,
                        [event_id] int NULL,
                        [pavillion] int NOT NULL,
                        [start_time] datetime2 NOT NULL,
                        [end_time] datetime2 NOT NULL,
                        [is_approved] bit NOT NULL,
                        CONSTRAINT [FK_Registration_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE,
                        CONSTRAINT [FK_Registration_Parks_park_id] FOREIGN KEY ([park_id]) REFERENCES [Parks] ([park_id]) ON DELETE CASCADE,
                        CONSTRAINT [FK_Registration_Events_event_id] FOREIGN KEY ([event_id]) REFERENCES [Events] ([event_id])
                    );

                    CREATE INDEX [IX_Registration_user_id] ON [Registration] ([user_id]);
                    CREATE INDEX [IX_Registration_park_id] ON [Registration] ([park_id]);
                    CREATE INDEX [IX_Registration_event_id] ON [Registration] ([event_id]);
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS Registration");
            migrationBuilder.Sql("DROP TABLE IF EXISTS Events");
            migrationBuilder.Sql("DROP TABLE IF EXISTS Users");
            // Parks table remains untouched
        }
    }
}
