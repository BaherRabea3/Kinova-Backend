using Kinova.Application.Common.DTOs.RepDTOs;
using Kinova.Domain.Entities.Scores;

namespace Kinova.Application.Common.Interfaces
{
    public interface IScoreCalculator
    {
        Score Calculate(List<RepUploadDto> reps);
    }
}
