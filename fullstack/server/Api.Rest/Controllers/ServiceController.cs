using Application.Interfaces;
using Application.Models.Dtos.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers;

[ApiController]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    private const string ControllerRoute = "api/service/";

    private const string CreateServiceRoute = ControllerRoute + nameof(CreateService);

    private const string DeleteServiceRoute = ControllerRoute + nameof(DeleteService);

    private const string UpdateServiceRoute = ControllerRoute + nameof(UpdateService);

    [HttpPost]
    [Route(CreateServiceRoute)]
    public ActionResult<ServiceResponseDto> CreateService([FromBody] ServiceCreateRequestDto dto)
    {
        return Ok(serviceService.CreateService(dto));
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
}