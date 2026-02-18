using CheckInCloud.Api.DTOs.Auth;
using CheckInCloud.Api.Results;

namespace CheckInCloud.Api.Contracts;

public interface IUsersService
{
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
    Task<Result<string>> LoginAsync(LoginUserDto loginUserDto);
}