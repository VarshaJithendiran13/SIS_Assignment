using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Models;

namespace StudentInfoSys.Services.Interfaces
{
    public interface ITeacherService
    {
        void AddTeacher(Teacher teacher);
        void UpdateTeacherInfo(Teacher teacher, int teacherID, string firstName, string lastName, string email);
        void AssignTeacherToCourse(Teacher teacher, Course course);
        List<Course> GetAssignedCourses(int teacherID);
        Teacher GetTeacherById(int teacherId);
        List<Teacher> GetAllTeachers();



    }
}

