using Student_Management_System.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Student_Management_System.Models
{
    public class StudentInfo
    {
        public Guid studentId { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Student name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Age is required")]
        [Range(5, 100, ErrorMessage = "Age must be between 5 and 100")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Department is required")]
        [EnumDataType(typeof(StudentDep), ErrorMessage = "Invalid department selected")]
        public StudentDep Department { get; set; } = StudentDep.Science;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = string.Empty;
    }
}
