using ApiToolKit.Application.DTOs.Firebase.FirebaseAuth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Firebase;

public interface IFirebaseUserService
{
    Task<Result<FirebaseAuthUser>> SignUpAsync(FirebaseSignUpRequest request);
    Task<Result<FirebaseAuthUser>> SignInAsync(FirebaseSignInRequest request);
    Task<Result<GetFirebaseUserResponse>> GetUserAsync(GetFirebaseUserRequest request);
    Task<Result<FirebaseAuthUser>> UpdateUserAsync(UpdateFirebaseUserRequest request);
    Task<Result<AdminListUsersResponse>> AdminListUsersAsync(AdminListUsersRequest request);
    Task<Result<FirebaseUserRecord>> AdminCreateUserAsync(AdminCreateUserRequest request);
    Task<Result<FirebaseUserRecord>> AdminUpdateUserAsync(AdminUpdateUserRequest request);
    Task<Result<bool>> AdminDeleteUserAsync(AdminDeleteUserRequest request);
    Task<Result<Dictionary<string, object>>> VerifyIdTokenAsync(VerifyIdTokenRequest request);
    Task<Result<CustomTokenResponse>> CreateCustomTokenAsync(CreateCustomTokenRequest request);
}
