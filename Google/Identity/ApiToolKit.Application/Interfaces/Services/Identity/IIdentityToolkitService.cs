using ApiToolKit.Application.DTOs.Identity.Toolkit;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Identity;

public interface IIdentityToolkitService
{
    Task<Result<AuthUser>> SignUpAsync(SignUpRequest request);
    Task<Result<AuthUser>> SignInAsync(SignInRequest request);
    Task<Result<GetAccountInfoResponse>> GetAccountInfoAsync(GetAccountInfoRequest request);
    Task<Result<AuthUser>> UpdateProfileAsync(UpdateProfileRequest request);
    Task<Result<AuthUser>> ChangePasswordAsync(ChangePasswordRequest request);
    Task<Result<AuthUser>> ChangeEmailAsync(ChangeEmailRequest request);
    Task<Result<OobCodeResponse>> SendVerificationEmailAsync(VerifyEmailRequest request);
    Task<Result<OobCodeResponse>> SendPasswordResetAsync(ResetPasswordRequest request);
    Task<Result<bool>> DeleteAccountAsync(DeleteAccountRequest request);
}
