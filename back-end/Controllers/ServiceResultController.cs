using Efficiency.Data.DTO.Service;
using Efficiency.Data.DTO.ServiceResult;
using Efficiency.Services;
using Microsoft.AspNetCore.Mvc;

namespace Efficiency.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceResultController : ControllerBase
{
    private ServiceResultService _service { get; set; }

    public ServiceResultController(ServiceResultService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IActionResult result = NoContent();

        ICollection<GetServiceResultDTO>? DTO = _service.GetAll();

        if (DTO != null)
        {
            result = Ok(DTO);
        }

        return result;
    }

    [HttpGet("result/{resultID}/service/{serviceID}")]
    public IActionResult Get(int resultID, int serviceID)
    {
        IActionResult result = NotFound();

        GetServiceResultDTO? DTO = _service.Get(serviceID, resultID);

        if (DTO != null)
        {
            result = Ok(DTO);
        }

        return result;
    }

    [HttpGet("result/{resultID}/services")]
    public IActionResult GetResultServices(int resultID)
    {
        IActionResult result = NotFound();

        ICollection<GetServiceDTO>? DTO = _service.GetResultServices(resultID);

        if (DTO != null)
        {
            result = Ok(DTO);
        }

        return result;
    }

    [HttpPost]
    public IActionResult Post([FromBody] PostServiceResultDTO DTO)
    {
        IActionResult result = StatusCode(500);

        GetServiceResultDTO? serviceResult = _service.Post(DTO);

        if (serviceResult != null)
        {
            result = CreatedAtAction(
                nameof(Get),
                new
                {
                    resultID = serviceResult.Result?.ID,
                    serviceID = serviceResult.Service?.ID
                },
                serviceResult
            );
        }

        return result;
    }

    [HttpPut]
    public IActionResult Put([FromBody] PutServiceResultDTO DTO)
    {
        IActionResult result = NotFound();

        bool updateSucceeded = _service.Put(DTO);

        if (updateSucceeded)
        {
            result = NoContent();
        }

        return result;
    }

    [HttpDelete("result/{resultID}/service/{serviceID}")]
    public IActionResult Delete(int resultID, int serviceID)
    {

        IActionResult result = NotFound();

        bool deleteSucceeded = _service.Delete(serviceID, resultID);

        if (deleteSucceeded)
        {
            result = NoContent();
        }

        return result;
    }
}