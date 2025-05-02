
using System.Data;
using Microsoft.Data.SqlClient;
using PatientManagementSystem.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PatientManagementSystem.API.Services
{
    public class PatientServiceImpl : IPatientService
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public PatientServiceImpl(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = this._configuration.GetConnectionString("DefaultConnection");
        }
        public ResponseDto AddPatient(Patient patient)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    // string query = "INSERT INTO Patients (Name, Email, PhoneNumber, Age) VALUES (@Name, @Email, @PhoneNumber, @Age)";
                    SqlCommand command = new SqlCommand("dbo.AddPatient", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Name", patient.Name);
                    command.Parameters.AddWithValue("@Email", patient.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                    command.Parameters.AddWithValue("@Age", patient.Age);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Patient added successfully.";
                        response.Data = patient;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Failed to add patient.";
                        response.Data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public ResponseDto DeletePatient(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand deleteCommand = new SqlCommand("dbo.DeletePatientById", connection);
                SqlCommand checkUserCommand = new SqlCommand("dbo.DoesPatientExist", connection);
                
                deleteCommand.CommandType = CommandType.StoredProcedure;
                checkUserCommand.CommandType = CommandType.StoredProcedure;
                checkUserCommand.Parameters.AddWithValue("@Id", id);
                SqlParameter outputParam = new SqlParameter("@Exists", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                checkUserCommand.Parameters.Add(outputParam);
                connection.Open();
                checkUserCommand.ExecuteNonQuery();
                if (!Convert.ToBoolean(outputParam.Value))
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "No patient found with the given ID.",
                        Data = null
                    };
                }

                deleteCommand.Parameters.AddWithValue("@Id", id);
               
                int rowsAffected = deleteCommand.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Patient deleted successfully.",
                        Data = null
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Failed to delete patient.",
                        Data = null
                    };
                }
            }
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.GetAllPatients", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Patient patient = new Patient
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Age = (int)reader["Age"]

                    };
                    patients.Add(patient);
                }

                connection.Close();
                return patients;

            }
        }

        public ResponseDto GetPatientById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.GetPatientById", connection);
  
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Patient patient = new Patient
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Age = (int)reader["Age"]
                    };
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Patient found.",
                        Data = patient
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Patient not found.",
                        Data = null
                    };
                }

            }

        }

        public ResponseDto UpdatePatient(int id, Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.UpdatePatientById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", patient.Name);
                command.Parameters.AddWithValue("@Email", patient.Email);
                command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                command.Parameters.AddWithValue("@Age", patient.Age);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Patient updated successfully.",
                        Data = null
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Failed to update patient.",
                        Data = null
                    };
                }
            }
        }
    }
}