using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kinova.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlanQueryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4babd578-4f54-452d-8d40-930d63d211e4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a85b977f-b657-45c0-9b9e-ab1762f7bf75"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("53e76e8d-f30b-4366-9621-0f5e7174affb"), null, "Patient", "PATIENT" },
                    { new Guid("8cb15d34-82f5-4e95-bfb9-e474a5934c4c"), null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "BodyPart", "Category", "Defaults", "DifficultyLevel", "Instructions", "Name", "TargetJoints", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("33333333-0000-0000-0000-000000000001"), "Shoulder", "Range of Motion", "{\"sets\":3,\"reps\":10,\"holdSeconds\":2}", "Beginner", "Raise the arm forward and upward to shoulder height, then slowly lower it back down.", "Shoulder Flexion", "Glenohumeral Joint", "https://videos.kinova-health.com/exercises/shoulder-flexion.mp4" },
                    { new Guid("33333333-0000-0000-0000-000000000002"), "Knee", "Strength", "{\"sets\":3,\"reps\":12,\"holdSeconds\":3}", "Beginner", "While seated, extend the knee until the leg is straight, then slowly return to the starting position.", "Seated Knee Extension", "Knee Joint", "https://videos.kinova-health.com/exercises/knee-extension.mp4" },
                    { new Guid("33333333-0000-0000-0000-000000000003"), "Lower Body", "Functional Strength", "{\"sets\":3,\"reps\":15,\"holdSeconds\":0}", "Intermediate", "Stand with feet shoulder-width apart, lower the hips back and down, then return to standing.", "Bodyweight Squat", "Hip, Knee, Ankle", "https://videos.kinova-health.com/exercises/bodyweight-squat.mp4" }
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Description", "DoctorId", "EndDate", "IsActive", "Name", "PatientId", "Source", "StartDate" },
                values: new object[,]
                {
                    { new Guid("44444444-0000-0000-0000-000000000001"), "Progressive strengthening plan following ACL reconstruction, weeks 6-12.", new Guid("e5a685a3-a6ac-f111-a08c-a44cc84061dc"), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ACL Recovery - Phase 2", new Guid("6b935f96-a4ac-f111-a08c-a44cc84061dc"), "Doctor", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-0000-0000-0000-000000000002"), "Initial post-op mobility plan, weeks 0-6.", new Guid("e5a685a3-a6ac-f111-a08c-a44cc84061dc"), new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ACL Recovery - Phase 1", new Guid("6b935f96-a4ac-f111-a08c-a44cc84061dc"), "Doctor", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-0000-0000-0000-000000000003"), "Range-of-motion focused plan for rotator cuff recovery.", new Guid("de7dece8-a6ac-f111-a08c-a44cc84061dc"), new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Shoulder Mobility Restoration", new Guid("5041b5af-a4ac-f111-a08c-a44cc84061dc"), "Doctor", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-0000-0000-0000-000000000004"), "AI-recommended supplementary plan to accelerate shoulder ROM gains.", new Guid("de7dece8-a6ac-f111-a08c-a44cc84061dc"), new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Adjunct Mobility Boost", new Guid("5041b5af-a4ac-f111-a08c-a44cc84061dc"), "AIRecommendation", new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PlanExercises",
                columns: new[] { "Id", "ExerciseId", "FrequencyPerWeek", "PlanId", "Repetitions", "Sets" },
                values: new object[,]
                {
                    { new Guid("55555555-0000-0000-0000-000000000001"), new Guid("33333333-0000-0000-0000-000000000003"), 5, new Guid("44444444-0000-0000-0000-000000000001"), 15, 3 },
                    { new Guid("55555555-0000-0000-0000-000000000002"), new Guid("33333333-0000-0000-0000-000000000002"), 4, new Guid("44444444-0000-0000-0000-000000000001"), 12, 3 },
                    { new Guid("55555555-0000-0000-0000-000000000003"), new Guid("33333333-0000-0000-0000-000000000002"), 3, new Guid("44444444-0000-0000-0000-000000000002"), 10, 2 },
                    { new Guid("55555555-0000-0000-0000-000000000004"), new Guid("33333333-0000-0000-0000-000000000001"), 6, new Guid("44444444-0000-0000-0000-000000000003"), 10, 3 },
                    { new Guid("55555555-0000-0000-0000-000000000005"), new Guid("33333333-0000-0000-0000-000000000001"), 4, new Guid("44444444-0000-0000-0000-000000000004"), 8, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("53e76e8d-f30b-4366-9621-0f5e7174affb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8cb15d34-82f5-4e95-bfb9-e474a5934c4c"));

            migrationBuilder.DeleteData(
                table: "PlanExercises",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "PlanExercises",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "PlanExercises",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "PlanExercises",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "PlanExercises",
                keyColumn: "Id",
                keyValue: new Guid("55555555-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-0000-0000-0000-000000000004"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4babd578-4f54-452d-8d40-930d63d211e4"), null, "Patient", "PATIENT" },
                    { new Guid("a85b977f-b657-45c0-9b9e-ab1762f7bf75"), null, "Doctor", "DOCTOR" }
                });
        }
    }
}
