using Application.Interfaces;
using Application.Interfaces.Infrastructure.Websocket;
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
    
    [HttpGet]
    [Route(GetUserByEmailRoute)]
    public ActionResult<UserResponseDto> GetUserByEmail(string email)
    {
        return Ok(userService.GetUserByEmail(email));
    }
    
    [HttpGet]
    [Route(GetAllUsersRoute)]
    public ActionResult<List<UserResponseDto>> GetAllUsers([FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(userService.GetAllUsers());
    }
    
    [HttpDelete]
    [Route(DeleteUserRoute)]
    public ActionResult<UserResponseDto> DeleteUser(string id, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        return Ok(userService.DeleteUser(id));
    }
}