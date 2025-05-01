
using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.API.Models;
using PatientManagementSystem.API.Services;

namespace PatientManagementSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PatientApiController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientApiController(IPatientService patientService)
    {
        this._patientService = patientService;
    }

    [HttpPost]
    [Route("add-patient")]
    public IActionResult AddPatient([FromBody] Patient patient)
    {
        if (ModelState.IsValid)
        {
            var response = _patientService.AddPatient(patient);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        return BadRequest(new ResponseDto { IsSuccess = false, Message = "Invalid model state" });
    }

    [HttpGet]
    [Route("get-all-patients")]
    public IEnumerable<Patient> GetAllPatients()
    {
        return _patientService.GetAllPatients();
    }

    [HttpPut]
    [Route("update-patient/{id}")]
    public IActionResult UpdatePatient([FromRoute] int id, [FromBody] Patient patient)
    {
        if (ModelState.IsValid)
        {
            var response = _patientService.UpdatePatient(id, patient);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        return BadRequest(new ResponseDto { IsSuccess = false, Message = "Invalid data" });
    }

    [HttpDelete]
    [Route("delete-patient/{id}")]
    public IActionResult DeletePatient([FromRoute] int id)
    {
        var response = _patientService.DeletePatient(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
}
