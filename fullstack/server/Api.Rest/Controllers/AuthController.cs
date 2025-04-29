using Api.Rest.Extensions;
using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class AuthController(ISecurityService securityService) : ControllerBase
{
    public const string ControllerRoute = "api/auth/";

    public const string LoginRoute = ControllerRoute + nameof(Login);


    public const string RegisterRoute = ControllerRoute + nameof(Register);
    
    public const string VerifyEmailRoute = ControllerRoute + nameof(VerifyEmail);
    
    public const string ResendVerificationEmailRoute = ControllerRoute + nameof(ResendVerificationEmail);
    
    public const string SecuredRoute = ControllerRoute + nameof(Secured);


    [HttpPost]
    [Route(LoginRoute)]
    public ActionResult<AuthResponseDto> Login([FromBody] AuthLoginRequestDto dto)
    {
        return Ok(securityService.Login(dto));
    }

    [Route(RegisterRoute)]
    [HttpPost]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] AuthRegisterRequestDto dto)
    {
        return Ok(await securityService.Register(dto));
    }

    [HttpPost]
    [Route(VerifyEmailRoute)]
    public async Task<ActionResult<VerifyEmailResponseDto>> VerifyEmail([FromBody] VerifyEmailRequestDto dto)
    {
        return Ok(await securityService.VerifyEmail(dto));
    }

    [HttpPost]
    [Route(ResendVerificationEmailRoute)]
    public async Task<ActionResult<ResendVerificationEmailResponseDto>> ResendVerificationEmail(
        [FromBody] ResendVerificationEmailRequestDto dto)
    {
        return Ok(await securityService.ResendVerificationEmail(dto));
    }

    [HttpGet]
    [Route(SecuredRoute)]
    public ActionResult Secured()
    {
        securityService.VerifyJwtOrThrow(HttpContext.GetJwt());
        return Ok("You are authorized to see this message");
    }
}