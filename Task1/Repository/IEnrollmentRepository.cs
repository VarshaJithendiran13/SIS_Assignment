using StudentInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Repository
{
    public interface IEnrollmentRepository
    {
        Enrollment GetEnrollmentById(int enrollmentID);
        void AddEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int enrollmentID);
        Student GetStudentByEnrollment(int enrollmentID);
        Course GetCourseByEnrollment(int enrollmentID);
        public bool IsStudentEnrolledInCourse(int studentId, int courseId);
        List<Enrollment> GetEnrollmentsByStudent(int studentId);
    }
}

