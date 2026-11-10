using Student_Management_System.Enums;
namespace Student_Management_System.Models
{
    public class StudentDataModel
    {
        public List<StudentInfo> Students { get; } =
        [
            new StudentInfo { Name = "Aarav Sharma", Age = 18, Email = "aarav.sharma@example.com", Department = StudentDep.Engineering },
            new StudentInfo { Name = "Ananya Patel", Age = 19, Email = "ananya.patel@example.com", Department = StudentDep.Science },
            new StudentInfo { Name = "Rohan Verma", Age = 20, Email = "rohan.verma@example.com", Department = StudentDep.Commerce },
            new StudentInfo { Name = "Isha Gupta", Age = 21, Email = "isha.gupta@example.com", Department = StudentDep.Arts },
            new StudentInfo { Name = "Vivaan Nair", Age = 22, Email = "vivaan.nair@example.com", Department = StudentDep.Medicine },
            new StudentInfo { Name = "Meera Joshi", Age = 18, Email = "meera.joshi@example.com", Department = StudentDep.Science },
            new StudentInfo { Name = "Aditya Rao", Age = 23, Email = "aditya.rao@example.com", Department = StudentDep.Engineering },
            new StudentInfo { Name = "Diya Kulkarni", Age = 19, Email = "diya.kulkarni@example.com", Department = StudentDep.Arts },
            new StudentInfo { Name = "Karan Singh", Age = 20, Email = "karan.singh@example.com", Department = StudentDep.Commerce },
            new StudentInfo { Name = "Sneha Iyer", Age = 21, Email = "sneha.iyer@example.com", Department = StudentDep.Medicine },
            new StudentInfo { Name = "Arjun Desai", Age = 22, Email = "arjun.desai@example.com", Department = StudentDep.Engineering },
            new StudentInfo { Name = "Priya Menon", Age = 18, Email = "priya.menon@example.com", Department = StudentDep.Science },
            new StudentInfo { Name = "Rahul Chawla", Age = 24, Email = "rahul.chawla@example.com", Department = StudentDep.Commerce },
            new StudentInfo { Name = "Nisha Kapoor", Age = 20, Email = "nisha.kapoor@example.com", Department = StudentDep.Arts },
            new StudentInfo { Name = "Yash Malhotra", Age = 23, Email = "yash.malhotra@example.com", Department = StudentDep.Medicine }
        ];
    }
}