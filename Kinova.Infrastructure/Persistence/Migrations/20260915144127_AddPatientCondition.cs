using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kinova.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a95e1a8-9032-4a9f-b8fb-0191988f1746"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("76f67d8a-3b6f-4ae1-b7cf-29789776548b"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConditionId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DiagnosedDate",
                table: "Patients",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a3d9703-a141-4fdb-947b-9210d6d3c381"), null, "Patient", "PATIENT" },
                    { new Guid("3f0b45c3-9693-4a7a-aaf5-22256ec407c3"), null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "Conditions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("66666666-0000-0000-0000-000000000001"), "Degenerative knee joint disease (OARSI guideline-based)", "Knee Osteoarthritis" },
                    { new Guid("66666666-0000-0000-0000-000000000002"), "Post-surgical anterior cruciate ligament rehabilitation", "ACL Reconstruction (Post-op)" },
                    { new Guid("66666666-0000-0000-0000-000000000003"), "Subacromial impingement or post-surgical shoulder rehab", "Shoulder Impingement / Post-op Shoulder" },
                    { new Guid("66666666-0000-0000-0000-000000000004"), "Anterior knee pain from patellar tracking dysfunction", "Patellofemoral Pain Syndrome" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ConditionId",
                table: "Patients",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Conditions_ConditionId",
                table: "Patients",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Conditions_ConditionId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ConditionId",
                table: "Patients");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a3d9703-a141-4fdb-947b-9210d6d3c381"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f0b45c3-9693-4a7a-aaf5-22256ec407c3"));

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DiagnosedDate",
                table: "Patients");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1a95e1a8-9032-4a9f-b8fb-0191988f1746"), null, "Patient", "PATIENT" },
                    { new Guid("76f67d8a-3b6f-4ae1-b7cf-29789776548b"), null, "Doctor", "DOCTOR" }
                });
        }
    }
}
