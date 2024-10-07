using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Models;

namespace StudentInfoSys.Services.Interfaces
{
    public interface IStudentService
    {
        void EnrollInCourse(Student student, Course course);
        void UpdateStudentInfo(Student student, string firstName, string lastName, DateTime dob, string email, string phone);
        void MakePayment(Student student, decimal amount, DateTime paymentDate);
        List<Course> GetEnrolledCourses(Student student);
        List<Payment> GetPaymentHistory(Student student);
        Student GetStudentById(int studentId);
        void DeleteStudent(int studentId);
        List<Student> GetAllStudents();
    }
}
