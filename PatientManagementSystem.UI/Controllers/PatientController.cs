

using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.API.Models;
namespace PatientManagementSystem.UI.Controllers;
public class PatientController : Controller
{

   public IActionResult Index()
    {
        return View();
    }


    public IActionResult UpdatePatient()
    {
        return View();
    }

  


}