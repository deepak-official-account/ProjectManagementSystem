

using Microsoft.AspNetCore.Mvc;
namespace PatientManagementSystem.UI.Controllers;
public class PatientController : Controller
{

   public IActionResult Index()
    {
        return View();
    }

    [HttpPost]

    public IActionResult AddPatient()
    {
        return View();
    }

    public IActionResult UpdatePatient()
    {
        return View();
    }

    public IActionResult DeletePatient()
    {
        return View();
    }

    public IActionResult GetAllPatients()
    {
        return View();
    }
    
}