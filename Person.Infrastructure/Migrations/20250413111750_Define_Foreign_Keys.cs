using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace BasePerson.Api.Migrations
{
    /// <inheritdoc />
    public partial class Define_Foreign_Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PhoneRelativePeople_PersonId",
                table: "PhoneRelativePeople",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneRelativePeople_PhoneId",
                table: "PhoneRelativePeople",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRelative_FirstPersonId",
                table: "PeopleRelative",
                column: "FirstPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRelative_SecondPersonId",
                table: "PeopleRelative",
                column: "SecondPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleRelative_People_FirstPersonId",
                table: "PeopleRelative",
                column: "FirstPersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleRelative_People_SecondPersonId",
                table: "PeopleRelative",
                column: "SecondPersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneRelativePeople_People_PersonId",
                table: "PhoneRelativePeople",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneRelativePeople_Phones_PhoneId",
                table: "PhoneRelativePeople",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeopleRelative_People_FirstPersonId",
                table: "PeopleRelative");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleRelative_People_SecondPersonId",
                table: "PeopleRelative");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneRelativePeople_People_PersonId",
                table: "PhoneRelativePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneRelativePeople_Phones_PhoneId",
                table: "PhoneRelativePeople");

            migrationBuilder.DropIndex(
                name: "IX_PhoneRelativePeople_PersonId",
                table: "PhoneRelativePeople");

            migrationBuilder.DropIndex(
                name: "IX_PhoneRelativePeople_PhoneId",
                table: "PhoneRelativePeople");

            migrationBuilder.DropIndex(
                name: "IX_PeopleRelative_FirstPersonId",
                table: "PeopleRelative");

            migrationBuilder.DropIndex(
                name: "IX_PeopleRelative_SecondPersonId",
                table: "PeopleRelative");
        }
    }
}
