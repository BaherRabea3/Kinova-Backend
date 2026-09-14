using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Exercises;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Plans;
using Kinova.Infrastructure.Identity;

namespace Kinova.Infrastructure.Persistence
{

    public static class DbSeeder
    {

        private static readonly Guid Doctor1Id = Guid.Parse("fe0691d2-30ad-f111-8f74-00155df0670e");
        private static readonly Guid Doctor1UserId = Guid.Parse("932fdec4-ba8a-4f81-78a8-08df0f536c78");
        private static readonly Guid Doctor2Id = Guid.Parse("eb420afe-30ad-f111-8f74-00155df0670e");
        private static readonly Guid Doctor2UserId = Guid.Parse("cb85c401-fa7b-4119-78a9-08df0f536c78");

        private static readonly Guid Patient1Id = Guid.Parse("643fbb85-2fad-f111-8f74-00155df0670e");
        private static readonly Guid Patient1UserId = Guid.Parse("9bbeb156-c98a-4fce-78a6-08df0f536c78");
        private static readonly Guid Patient2Id = Guid.Parse("d1fcede8-2fad-f111-8f74-00155df0670e");
        private static readonly Guid Patient2UserId = Guid.Parse("9557f4e2-0d2e-4ed6-78a7-08df0f536c78");

        private static readonly Guid ExShoulderFlexionId = Guid.Parse("33333333-0000-0000-0000-000000000001");
        private static readonly Guid ExKneeExtensionId = Guid.Parse("33333333-0000-0000-0000-000000000002");
        private static readonly Guid ExSquatId = Guid.Parse("33333333-0000-0000-0000-000000000003");

        private static readonly Guid AclPhase2PlanId = Guid.Parse("44444444-0000-0000-0000-000000000001");
        private static readonly Guid AclPhase1PlanId = Guid.Parse("44444444-0000-0000-0000-000000000002");
        private static readonly Guid ShoulderPlanId = Guid.Parse("44444444-0000-0000-0000-000000000003");
        private static readonly Guid AdjunctPlanId = Guid.Parse("44444444-0000-0000-0000-000000000004");

        private static readonly Guid Item1Id = Guid.Parse("55555555-0000-0000-0000-000000000001");
        private static readonly Guid Item2Id = Guid.Parse("55555555-0000-0000-0000-000000000002");
        private static readonly Guid Item3Id = Guid.Parse("55555555-0000-0000-0000-000000000003");
        private static readonly Guid Item4Id = Guid.Parse("55555555-0000-0000-0000-000000000004");
        private static readonly Guid Item5Id = Guid.Parse("55555555-0000-0000-0000-000000000005");

        public static List<Exercise> SeedExercises()
        {
            return new List<Exercise>
            {
                new Exercise
                {
                    Id = ExShoulderFlexionId,
                    Name = "Shoulder Flexion",
                    Category = "Range of Motion",
                    BodyPart = "Shoulder",
                    TargetJoints = "Glenohumeral Joint",
                    Instructions = "Raise the arm forward and upward to shoulder height, then slowly lower it back down.",
                    VideoUrl = "https://videos.kinova-health.com/exercises/shoulder-flexion.mp4",
                    DifficultyLevel = "Beginner",
                    Defaults = "{\"sets\":3,\"reps\":10,\"holdSeconds\":2}"
                },
                new Exercise
                {
                    Id = ExKneeExtensionId,
                    Name = "Seated Knee Extension",
                    Category = "Strength",
                    BodyPart = "Knee",
                    TargetJoints = "Knee Joint",
                    Instructions = "While seated, extend the knee until the leg is straight, then slowly return to the starting position.",
                    VideoUrl = "https://videos.kinova-health.com/exercises/knee-extension.mp4",
                    DifficultyLevel = "Beginner",
                    Defaults = "{\"sets\":3,\"reps\":12,\"holdSeconds\":3}"
                },
                new Exercise
                {
                    Id = ExSquatId,
                    Name = "Bodyweight Squat",
                    Category = "Functional Strength",
                    BodyPart = "Lower Body",
                    TargetJoints = "Hip, Knee, Ankle",
                    Instructions = "Stand with feet shoulder-width apart, lower the hips back and down, then return to standing.",
                    VideoUrl = "https://videos.kinova-health.com/exercises/bodyweight-squat.mp4",
                    DifficultyLevel = "Intermediate",
                    Defaults = "{\"sets\":3,\"reps\":15,\"holdSeconds\":0}"
                }
            };
        }

        public static List<Plan> SeedPlans()
        {
            return new List<Plan>
            {

                new Plan
                {
                    Id = AclPhase2PlanId,
                    Name = "ACL Recovery - Phase 2",
                    Description = "Progressive strengthening plan following ACL reconstruction, weeks 6-12.",
                    Source = PlanSource.Doctor,
                    StartDate = new DateTime(2026, 7, 1),
                    EndDate = new DateTime(2026, 9, 1),
                    IsActive = true,
                    DoctorId = Doctor1Id,
                    PatientId = Patient1Id
                },

                new Plan
                {
                    Id = AclPhase1PlanId,
                    Name = "ACL Recovery - Phase 1",
                    Description = "Initial post-op mobility plan, weeks 0-6.",
                    Source = PlanSource.Doctor,
                    StartDate = new DateTime(2026, 5, 1),
                    EndDate = new DateTime(2026, 6, 30),
                    IsActive = false,
                    DoctorId = Doctor1Id,
                    PatientId = Patient1Id
                },

                new Plan
                {
                    Id = ShoulderPlanId,
                    Name = "Shoulder Mobility Restoration",
                    Description = "Range-of-motion focused plan for rotator cuff recovery.",
                    Source = PlanSource.Doctor,
                    StartDate = new DateTime(2026, 8, 1),
                    EndDate = new DateTime(2026, 10, 15),
                    IsActive = true,
                    DoctorId = Doctor2Id,
                    PatientId = Patient2Id
                },

                new Plan
                {
                    Id = AdjunctPlanId,
                    Name = "Adjunct Mobility Boost",
                    Description = "AI-recommended supplementary plan to accelerate shoulder ROM gains.",
                    Source = PlanSource.AIRecommendation,
                    StartDate = new DateTime(2026, 9, 1),
                    EndDate = new DateTime(2026, 10, 15),
                    IsActive = true,
                    DoctorId = Doctor2Id,
                    PatientId = Patient2Id
                }
            };
        }

        public static List<PlanExerciseItem> SeedPlanExerciseItems()
        {
            return new List<PlanExerciseItem>
            {
                new PlanExerciseItem
                {
                    Id = Item1Id,
                    Sets = 3,
                    Repetitions = 15,
                    FrequencyPerWeek = 5,
                    PlanId = AclPhase2PlanId,
                    ExerciseId = ExSquatId
                },
                new PlanExerciseItem
                {
                    Id = Item2Id,
                    Sets = 3,
                    Repetitions = 12,
                    FrequencyPerWeek = 4,
                    PlanId = AclPhase2PlanId,
                    ExerciseId = ExKneeExtensionId
                },
                new PlanExerciseItem
                {
                    Id = Item3Id,
                    Sets = 2,
                    Repetitions = 10,
                    FrequencyPerWeek = 3,
                    PlanId = AclPhase1PlanId,
                    ExerciseId = ExKneeExtensionId
                },
                new PlanExerciseItem
                {
                    Id = Item4Id,
                    Sets = 3,
                    Repetitions = 10,
                    FrequencyPerWeek = 6,
                    PlanId = ShoulderPlanId,
                    ExerciseId = ExShoulderFlexionId
                },
                new PlanExerciseItem
                {
                    Id = Item5Id,
                    Sets = 2,
                    Repetitions = 8,
                    FrequencyPerWeek = 4,
                    PlanId = AdjunctPlanId,
                    ExerciseId = ExShoulderFlexionId
                }
            };
        }
    }
}