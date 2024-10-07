using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Models;

namespace StudentInfoSys.Services.Interfaces
{
    public interface IEnrollmentService
    {
        void EnrollInCourse(Student student, Course course);
    
        Enrollment GetEnrollment(Student student, Course course);
        Student GetStudentByEnrollment(Enrollment enrollment);
        Course GetCourseByEnrollment(Enrollment enrollment);
        
    }
}

