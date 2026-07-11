using ApiToolKit.Application.DTOs.Maps.DistanceMatrix;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsDistanceMatrixService
{
    Task<Result<DistanceMatrixResponse>> GetDistanceMatrixAsync(DistanceMatrixRequest request);
}
