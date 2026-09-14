using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kinova.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeDoctorPatientOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("02da218f-ed8f-4eee-8807-e5ffc25624a0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fe6e76a3-ccd1-4b82-9811-b2a33b52b204"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4babd578-4f54-452d-8d40-930d63d211e4"), null, "Patient", "PATIENT" },
                    { new Guid("a85b977f-b657-45c0-9b9e-ab1762f7bf75"), null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4babd578-4f54-452d-8d40-930d63d211e4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a85b977f-b657-45c0-9b9e-ab1762f7bf75"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("02da218f-ed8f-4eee-8807-e5ffc25624a0"), null, "Patient", "PATIENT" },
                    { new Guid("fe6e76a3-ccd1-4b82-9811-b2a33b52b204"), null, "Doctor", "DOCTOR" }
                });
        }
    }
}
