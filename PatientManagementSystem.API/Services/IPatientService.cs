using PatientManagementSystem.API.Models;
namespace PatientManagementSystem.API.Services;
public interface IPatientService
{
    ResponseDto AddPatient(Patient patient);
    IEnumerable<Patient> GetAllPatients();
    ResponseDto GetPatientById(int id);
    ResponseDto UpdatePatient(int id, Patient patient);
    ResponseDto DeletePatient(int id);
}