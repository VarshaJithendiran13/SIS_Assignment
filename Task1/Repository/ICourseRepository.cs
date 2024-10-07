using StudentInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Repository
{
    public interface ICourseRepository
    {
        Course GetCourseById(int courseID);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseID);
        List<Enrollment> GetEnrollments(int courseID);
        Teacher GetAssignedTeacher(int courseID);
        Course GetCourseByName(string courseName);
        List<Course> GetAllCourses();
    }
}
