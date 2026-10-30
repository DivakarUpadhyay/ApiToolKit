using ApiToolKit.Application.DTOs.Merchant.Program;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantProgramService
{
    Task<Result<object>> GetProgramAsync(GetProgramRequest request);
    Task<Result<object>> EnableProgramAsync(EnableProgramRequest request);
    Task<Result<object>> DisableProgramAsync(DisableProgramRequest request);
    Task<Result<object>> ListProgramsAsync(ListProgramsRequest request);
}
