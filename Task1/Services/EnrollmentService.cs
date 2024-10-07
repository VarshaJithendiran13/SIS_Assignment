using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Exceptions;
using StudentInfoSys.Models;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;

namespace StudentInfoSys.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public EnrollmentService(EnrollmentRepository enrollmentRepository)
        {
        }

            public void EnrollInCourse(Student student, Course course)
            {
                // Validate student existence
                var existingStudent = _studentRepository.GetStudentById(student.StudentID);
                if (existingStudent == null)
                {
                    throw new StudentNotFoundException("Student not found.");
                }

                // Validate course existence
                var existingCourse = _courseRepository.GetCourseById(course.CourseID);
                if (existingCourse == null)
                {
                    throw new CourseNotFoundException("Course not found.");
                }

                // Check for duplicate enrollment
                var isEnrolled = _enrollmentRepository.IsStudentEnrolledInCourse(student.StudentID, course.CourseID);
                if (isEnrolled)
                {
                    throw new DuplicateEnrollmentException("Student is already enrolled in this course.");
                }

                // Enroll the student
                var enrollment = new Enrollment
                {
                    StudentID = student.StudentID,
                    CourseID = course.CourseID,
                    EnrollmentDate = DateTime.Now
                };
                _enrollmentRepository.AddEnrollment(enrollment);
                Console.WriteLine("Student enrolled in the course successfully.");
            }

            
            public Enrollment GetEnrollment(Student student, Course course)
            {
                // Validate student existence
                var existingStudent = _studentRepository.GetStudentById(student.StudentID);
                if (existingStudent == null)
                {
                    throw new StudentNotFoundException("Student not found.");
                }

                // Validate course existence
                var existingCourse = _courseRepository.GetCourseById(course.CourseID);
                if (existingCourse == null)
                {
                    throw new CourseNotFoundException("Course not found.");
                }

                // Retrieve specific enrollment by student and course
                var enrollment = _enrollmentRepository.GetEnrollmentsByStudent(student.StudentID);
            if (enrollment == null)
                {
                    throw new InvalidEnrollmentDataException("No enrollment found for this student in the specified course.");
                }

                return null;
            }

            public Student GetStudentByEnrollment(Enrollment enrollment)
            {
                // Validate enrollment
                if (enrollment == null)
                {
                    throw new InvalidOperationException("Enrollment cannot be null.");
                }

                return _studentRepository.GetStudentById(enrollment.StudentID);
            }

            public Course GetCourseByEnrollment(Enrollment enrollment)
            {
                // Validate enrollment
                if (enrollment == null)
                {
                    throw new InvalidOperationException("Enrollment cannot be null.");
                }

                return _courseRepository.GetCourseById(enrollment.CourseID);
            }

    }
}

