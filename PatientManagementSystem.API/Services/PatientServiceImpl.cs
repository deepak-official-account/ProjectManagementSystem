
using System.Data;
using Microsoft.Data.SqlClient;
using PatientManagementSystem.API.Models;

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
                    string query = "INSERT INTO Patients (Name, Email, PhoneNumber, Age) VALUES (@Name, @Email, @PhoneNumber, @Age)";
                    SqlCommand command = new SqlCommand(query, connection);
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
                SqlCommand command = new SqlCommand("AddPatient", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
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
                SqlCommand command = new SqlCommand("GetAllPatients", connection);
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
                SqlCommand command = new SqlCommand("GetPatientById", connection);
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
                SqlCommand command = new SqlCommand("UpdatePatient", connection);
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