using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_EventItems_EventItemID",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Participants_ParticipantID",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "ParticipantID",
                table: "Registrations",
                newName: "ParticipantId");

            migrationBuilder.RenameColumn(
                name: "EventItemID",
                table: "Registrations",
                newName: "EventItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ParticipantID",
                table: "Registrations",
                newName: "IX_Registrations_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_EventItemID_ParticipantID",
                table: "Registrations",
                newName: "IX_Registrations_EventItemId_ParticipantId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "EventItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_CategoryID",
                table: "EventItems",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventItems_Categories_CategoryID",
                table: "EventItems",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_EventItems_EventItemId",
                table: "Registrations",
                column: "EventItemId",
                principalTable: "EventItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Participants_ParticipantId",
                table: "Registrations",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventItems_Categories_CategoryID",
                table: "EventItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_EventItems_EventItemId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Participants_ParticipantId",
                table: "Registrations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_EventItems_CategoryID",
                table: "EventItems");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "EventItems");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "Registrations",
                newName: "ParticipantID");

            migrationBuilder.RenameColumn(
                name: "EventItemId",
                table: "Registrations",
                newName: "EventItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ParticipantId",
                table: "Registrations",
                newName: "IX_Registrations_ParticipantID");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_EventItemId_ParticipantId",
                table: "Registrations",
                newName: "IX_Registrations_EventItemID_ParticipantID");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_EventItems_EventItemID",
                table: "Registrations",
                column: "EventItemID",
                principalTable: "EventItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Participants_ParticipantID",
                table: "Registrations",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
