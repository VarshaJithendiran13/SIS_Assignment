using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Models;

namespace StudentInfoSys.Services.Interfaces
{
    public interface ICourseService
    {
        void AssignTeacher(Course course, Teacher teacher);
        void UpdateCourseInfo(Course course, int courseID, string courseName, int instructor);
        List<Enrollment> GetEnrollments(Course course);
        Teacher GetAssignedTeacher(Course course);
        Course GetCourseByName(string courseName);
        List<Course> GetAllCourses();
    }
}

