using System.ComponentModel.DataAnnotations;
namespace PatientManagementSystem.API.Models;
public class Patient
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number cannot be longer than 10 digits")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
    public string PhoneNumber { get; set; }
    [Required]

    public int Age { get; set; }
 
   public Patient(){}

    public Patient(string name, string email, string phoneNumber, int age)
    {

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Age = age;
        }
    }
