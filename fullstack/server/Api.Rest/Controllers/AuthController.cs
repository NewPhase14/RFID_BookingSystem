using Api.Rest.Extensions;
using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Dtos.Auth;
using Application.Models.Dtos.Auth.Invite;
using Application.Models.Dtos.Auth.Password;
using Application.Models.Dtos.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class AuthController(
    ISecurityService securityService,
    IValidator<AccountActivationRequestDto> accountActivationValidator,
    IValidator<ForgotPasswordRequestDto> forgotPasswordValidator,
    IValidator<ResetPasswordRequestDto> resetPasswordValidator) : ControllerBase
{
    public const string ControllerRoute = "api/auth/";

    public const string LoginRoute = ControllerRoute + nameof(Login);

    public const string RegisterRoute = ControllerRoute + nameof(Register);

    public const string AccountActivationRoute = ControllerRoute + nameof(AccountActivation);

    public const string ResendInviteEmailRoute = ControllerRoute + nameof(ResendInviteEmail);

    public const string ForgotPasswordRoute = ControllerRoute + nameof(ForgotPassword);

    public const string ResetPasswordRoute = ControllerRoute + nameof(ResetPassword);

    public const string SecuredRoute = ControllerRoute + nameof(Secured);


    [HttpPost]
    [Route(LoginRoute)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] AuthLoginRequestDto dto)
    {
        return Ok(await securityService.Login(dto));
    }

    [Route(RegisterRoute)]
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] AuthRegisterRequestDto dto,
        [FromHeader] string authorization)
    {
        var jwt = securityService.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin") return Unauthorized();
        return Ok(await securityService.Register(dto));
    }

    [HttpPost]
    [Route(AccountActivationRoute)]
    public async Task<ActionResult<AccountActivationResponseDto>> AccountActivation(
        [FromBody] AccountActivationRequestDto dto)
    {
        var validationResult = await accountActivationValidator.ValidateAsync(dto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        return Ok(await securityService.AccountActivation(dto));
    }

    [HttpPost]
    [Route(ResendInviteEmailRoute)]
    public async Task<ActionResult<ResendInviteEmailResponseDto>> ResendInviteEmail(
        [FromBody] ResendInviteEmailRequestDto dto)
    {
        return Ok(await securityService.ResendInviteEmail(dto));
    }

    [HttpPost]
    [Route(ForgotPasswordRoute)]
    public async Task<ActionResult<ForgotPasswordResponseDto>> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
    {
        var validationResult = await forgotPasswordValidator.ValidateAsync(dto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        return Ok(await securityService.ForgotPassword(dto));
    }

    [HttpPost]
    [Route(ResetPasswordRoute)]
    public async Task<ActionResult<ResetPasswordResponseDto>> ResetPassword([FromBody] ResetPasswordRequestDto dto)
    {
        var validationResult = await resetPasswordValidator.ValidateAsync(dto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        return Ok(await securityService.ResetPassword(dto));
    }

    [HttpGet]
    [Route(SecuredRoute)]
    public ActionResult Secured()
    {
        securityService.VerifyJwtOrThrow(HttpContext.GetJwt());
        return Ok("You are authorized to see this message");
    }
}