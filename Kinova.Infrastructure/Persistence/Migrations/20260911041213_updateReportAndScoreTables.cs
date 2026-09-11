using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kinova.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateReportAndScoreTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6059e3e-feb1-4432-a2d5-99a50cf2168e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d1102f02-ae85-4420-8858-b0a548a48bc1"));

            migrationBuilder.DropColumn(
                name: "AverageRangeOfMotion",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CorrectRepetitions",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "OverallScore",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "TotalRepetitions",
                table: "Reports");

            migrationBuilder.AddColumn<string>(
                name: "ErrorsJson",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ScoreId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1a95e1a8-9032-4a9f-b8fb-0191988f1746"), null, "Patient", "PATIENT" },
                    { new Guid("76f67d8a-3b6f-4ae1-b7cf-29789776548b"), null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ScoreId",
                table: "Reports",
                column: "ScoreId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Scores_ScoreId",
                table: "Reports",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Scores_ScoreId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ScoreId",
                table: "Reports");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a95e1a8-9032-4a9f-b8fb-0191988f1746"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("76f67d8a-3b6f-4ae1-b7cf-29789776548b"));

            migrationBuilder.DropColumn(
                name: "ErrorsJson",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Reports");

            migrationBuilder.AddColumn<double>(
                name: "AverageRangeOfMotion",
                table: "Reports",
                type: "float(10)",
                precision: 10,
                scale: 3,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectRepetitions",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OverallScore",
                table: "Reports",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalRepetitions",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a6059e3e-feb1-4432-a2d5-99a50cf2168e"), null, "Doctor", "DOCTOR" },
                    { new Guid("d1102f02-ae85-4420-8858-b0a548a48bc1"), null, "Patient", "PATIENT" }
                });
        }
    }
}
