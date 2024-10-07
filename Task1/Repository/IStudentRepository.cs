using StudentInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Repository

{
    public interface IStudentRepository
    {
        Student GetStudentById(int studentID);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentID);
        List<Student> GetAllStudents();
        List<Course> GetEnrolledCourses(int studentID);
        List<Payment> GetPaymentHistory(int studentID);
        
    }
}
