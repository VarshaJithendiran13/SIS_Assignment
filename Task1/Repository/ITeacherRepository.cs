using StudentInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys.Repository
{
    public interface ITeacherRepository
    {
        Teacher GetTeacherById(int teacherID);
        
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int teacherID);
        List<Course> GetAssignedCourses(int teacherID);
        List<Teacher> GetAllTeachers();
    }
}
