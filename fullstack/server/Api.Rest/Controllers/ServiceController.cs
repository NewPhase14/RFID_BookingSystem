using Application.Interfaces;
using Application.Models.Dtos.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class ServiceController(IServiceService serviceService, ISecurityService security) : ControllerBase
{
    private const string ControllerRoute = "api/service/";

    private const string CreateServiceRoute = ControllerRoute + nameof(CreateService);

    private const string DeleteServiceRoute = ControllerRoute + nameof(DeleteService);

    private const string UpdateServiceRoute = ControllerRoute + nameof(UpdateService);
    
    private const string GetAllServicesRoute = ControllerRoute + nameof(GetAllServices);
    
    private const string GetServiceByIdRoute = ControllerRoute + nameof(GetServiceById);

    [HttpPost]
    [Route(CreateServiceRoute)]
    public async Task<ActionResult<ServiceResponseDto>> CreateService([FromBody] ServiceCreateRequestDto dto, [FromHeader] string authorization)
    {
        var jwt = security.VerifyJwtOrThrow(authorization);
        if (jwt.Role != "Admin")
        {
            return Unauthorized();
        }
        var result = await serviceService.CreateService(dto);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route(DeleteServiceRoute)]
    public ActionResult<ServiceResponseDto> DeleteService(string id)
    {
        return Ok(serviceService.DeleteService(id));
    }
    
    [HttpPut]
    [Route(UpdateServiceRoute)]
    public ActionResult<ServiceResponseDto> UpdateService([FromBody] ServiceUpdateRequestDto dto)
    {
        return Ok(serviceService.UpdateService(dto));
    }
    
    [HttpGet]
    [Route(GetAllServicesRoute)]
    public ActionResult<List<GetServiceResponseDto>> GetAllServices()
    {
        return Ok(serviceService.GetAllServices());
    }
    
    [HttpGet]
    [Route(GetServiceByIdRoute)]
    public ActionResult<GetServiceResponseDto> GetServiceById(string id)
    {
        return Ok(serviceService.GetServiceById(id));
    }
}