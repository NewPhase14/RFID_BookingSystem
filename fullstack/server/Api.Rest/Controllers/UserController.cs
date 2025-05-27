using Application.Interfaces;
using Application.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class UserController(IUserService userService, ISecurityService security) : ControllerBase
{
    public const string ControllerRoute = "api/user/";

    public const string GetUserByEmailRoute = ControllerRoute + nameof(GetUserByEmail);

    public const string GetAllUsersRoute = ControllerRoute + nameof(GetAllUsers);

    public const string DeleteUserRoute = ControllerRoute + nameof(DeleteUser);

    public const string UpdateUserRoute = ControllerRoute + nameof(UpdateUser);

    [HttpGet]
    [Route(GetUserByEmailRoute)]
    public async Task<ActionResult<UserResponseDto>> GetUserByEmail(string email, [FromHeader] string authorization)
    {
        security.VerifyJwtOrThrow(authorization);
        return Ok(await userService.GetUserByEmail(email));
    }

    [HttpGet]
    [Route(GetAllUsersRoute)]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers([FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin") return Unauthorized();
        return Ok(await userService.GetAllUsers());
    }

    [HttpDelete]
    [Route(DeleteUserRoute)]
    public async Task<ActionResult<UserResponseDto>> DeleteUser(string id, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin") return Unauthorized();
        return Ok(await userService.DeleteUser(id));
    }

    [HttpPut]
    [Route(UpdateUserRoute)]
    public async Task<ActionResult<UserResponseDto>> UpdateUser([FromBody] UserUpdateRequestDto dto,
        [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin") return Unauthorized();

        return Ok(await userService.UpdateUser(dto));
    }
}