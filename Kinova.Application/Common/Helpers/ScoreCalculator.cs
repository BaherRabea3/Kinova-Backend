
using Kinova.Application.Common.DTOs.JointAngleReadingDTOs;
using Kinova.Application.Common.DTOs.RepDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Entities.Scores;

namespace Kinova.Application.Common.Helpers
{
    public class ScoreCalculator : IScoreCalculator
    {
        public Score Calculate(List<RepUploadDto> reps)
        {
            var valid = reps.Count(r => r.IsCorrect);
            var invalid = reps.Count - valid;

            var accuracyScore = reps.Count == 0
                ? 0
                : Math.Round((decimal)valid / reps.Count * 100, 2);

            var rangeOfMotionScore = reps.Count == 0
                ? 0
                : Math.Round(reps.Average(r => AverageAngleRange(r.JointAngleReadings)), 2);

            var stabilityScore = reps.Count == 0
                ? 0
                : Math.Round((decimal)(100 - reps.Average(r => r.MovementErrors.Count) * 5), 2);

            return new Score
            {
                Id = Guid.NewGuid(),
                AccuracyScore = accuracyScore,
                RangeOfMotionScore = rangeOfMotionScore,
                StabilityScore = stabilityScore,
                OverallScore = (accuracyScore + rangeOfMotionScore + stabilityScore) / 3,
                ValidRepetitions = valid,
                InvalidRepetitions = invalid
            };
        }

        private decimal AverageAngleRange(List<JointAngleReadingDto> readings) =>
            readings.Count == 0
                ? 0
                : readings.Max(r => r.Angle) - readings.Min(r => r.Angle);
    }
}
