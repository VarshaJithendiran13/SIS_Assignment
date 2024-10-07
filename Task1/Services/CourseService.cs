using System;
using System.Collections.Generic;
using StudentInfoSys.Models;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;
using StudentInfoSys.Exceptions; 

namespace StudentInfoSys.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        public CourseService(CourseRepository courseRepository)
        {
        }

        public void AssignTeacher(Course course, Teacher teacher)
        {
            // Check if the course exists
            if (course == null)
            {
                throw new CourseNotFoundException("The specified course does not exist.");
            }

            // Check if the teacher exists
            if (teacher == null)
            {
                throw new TeacherNotFoundException("The specified teacher does not exist.");
            }

            course.TeacherID = teacher.TeacherID;
            _courseRepository.UpdateCourse(course);
        }

        public void UpdateCourseInfo(Course course, int courseID, string courseName, int instructor)
        {
            // Check if the course exists
            if (course == null)
            {
                throw new CourseNotFoundException("The specified course does not exist.");
            }

            // Validate input data
            if (string.IsNullOrWhiteSpace(courseName) || instructor <= 0)
            {
                throw new InvalidCourseDataException("Course name or instructor ID is invalid.");
            }

            course.CourseID = courseID;
            course.CourseName = courseName;
            course.TeacherID = instructor;
            _courseRepository.UpdateCourse(course);
        }

        public List<Enrollment> GetEnrollments(Course course)
        {
            // Check if the course exists
            if (course == null)
            {
                throw new CourseNotFoundException("The specified course does not exist.");
            }

            return _courseRepository.GetEnrollments(course.CourseID);
        }

        public Course GetCourseByName(string courseName)
        {
            var course = _courseRepository.GetCourseByName(courseName);
            if (course == null)
            {
                throw new Exception($"Course with name '{courseName}' not found.");
            }
            return course;
        }
        public List<Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }
        public Teacher GetAssignedTeacher(Course course)
        {
            // Check if the course exists
            if (course == null)
            {
                throw new CourseNotFoundException("The specified course does not exist.");
            }

            Teacher teacher = _courseRepository.GetAssignedTeacher(course.CourseID);
            if (teacher == null)
            {
                throw new TeacherNotFoundException("No teacher assigned to this course.");
            }

            return teacher;
        }
    }
}
