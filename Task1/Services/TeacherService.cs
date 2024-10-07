using System;
using System.Collections.Generic;
using StudentInfoSys.Models;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;
using StudentInfoSys.Exceptions; 

namespace StudentInfoSys.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        public TeacherService(ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
        }

        public TeacherService(TeacherRepository teacherRepository)
        {
        }

        public void AddTeacher(Teacher teacher)
        {
            // Validate teacher data
            if (string.IsNullOrWhiteSpace(teacher.FirstName) || string.IsNullOrWhiteSpace(teacher.LastName) || string.IsNullOrWhiteSpace(teacher.Email))
            {
                throw new InvalidTeacherDataException("Teacher's first name, last name, or email cannot be empty.");
            }

            _teacherRepository.AddTeacher(teacher);
        }

        public void UpdateTeacherInfo(Teacher teacher, int teacherID, string firstName, string lastName, string email)
        {
            // Check if the teacher exists
            if (teacher == null)
            {
                throw new TeacherNotFoundException("The specified teacher does not exist.");
            }

            // Validate input data
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidTeacherDataException("First name, last name, or email cannot be empty.");
            }

            teacher.TeacherID = teacherID;
            teacher.FirstName = firstName;
            teacher.LastName = lastName;
            teacher.Email = email;

            _teacherRepository.UpdateTeacher(teacher);
        }

        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            // Check if the teacher exists
            if (teacher == null)
            {
                throw new TeacherNotFoundException("The specified teacher does not exist.");
            }

            // Check if the course exists
            if (course == null)
            {
                throw new CourseNotFoundException("The specified course does not exist.");
            }

            course.TeacherID = teacher.TeacherID;
            _courseRepository.UpdateCourse(course);
        }

        public List<Course> GetAssignedCourses(int teacherId)
        {
            // Check if the teacher exists by calling the repository
            Teacher teacher = _teacherRepository.GetTeacherById(teacherId);
            if (teacher == null)
            {
                throw new TeacherNotFoundException("The specified teacher does not exist.");
            }

            // Return the list of assigned courses
            return _teacherRepository.GetAssignedCourses(teacherId);
        }
        public List<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAllTeachers();
        }
        public Teacher GetTeacherById(int teacherId)
        {
            // Retrieve the teacher from the repository
            var teacher = _teacherRepository.GetTeacherById(teacherId);
            if (teacher == null)
            {
                throw new Exception($"Teacher with ID {teacherId} not found.");
            }
            return teacher;
        }
    }

}

