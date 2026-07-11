using ApiToolKit.Application.DTOs.Shopee.Voucher;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeVoucherService
{
    Task<Result<VoucherResponse>> CreateVoucherAsync(CreateVoucherRequest request);
    Task<Result<bool>> DeleteVoucherAsync(DeleteVoucherRequest request);
    Task<Result<VoucherResponse>> UpdateVoucherAsync(UpdateVoucherRequest request);
    Task<Result<bool>> EndVoucherAsync(EndVoucherRequest request);
    Task<Result<VoucherResponse>> GetVoucherAsync(GetVoucherRequest request);
    Task<Result<VoucherListResponse>> GetVoucherListAsync(GetVoucherListRequest request);
}
