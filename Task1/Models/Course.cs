using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Repository;

namespace StudentInfoSys.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int TeacherID { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public Course(int courseID, string courseName, int credits, int teacherID)
        {
            CourseID = courseID;
            CourseName = courseName;
            Credits = credits;
            TeacherID = teacherID;
        }
        public Course() { }

    }
}