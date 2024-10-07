using System;
using System.Collections.Generic;
using StudentInfoSys.Exceptions;  
using StudentInfoSys.Repository;  
using StudentInfoSys.Services.Interfaces;
using StudentInfoSys.Models;


namespace StudentInfoSys.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public StudentService(IStudentRepository studentRepository,
                              ICourseRepository courseRepository,
                              IPaymentRepository paymentRepository,
                              IEnrollmentRepository enrollmentRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _paymentRepository = paymentRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public StudentService(StudentRepository studentRepository)
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

        public void UpdateStudentInfo(Student student, string firstName, string lastName, DateTime dob, string email, string phone)
        {
            // Validate student existence
            var existingStudent = _studentRepository.GetStudentById(student.StudentID);
            if (existingStudent == null)
            {
                throw new StudentNotFoundException("Student not found.");
            }

            // Validate updated data
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                dob > DateTime.Now || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
            {
                throw new InvalidStudentDataException("Invalid student data provided.");
            }

            // Update student information
            existingStudent.FirstName = firstName;
            existingStudent.LastName = lastName;
            existingStudent.DateOfBirth = dob;
            existingStudent.Email = email;
            existingStudent.PhoneNumber = phone;

            _studentRepository.UpdateStudent(existingStudent);
            Console.WriteLine("Student information updated successfully.");
        }
        public void DeleteStudent(int studentId)
        {
            // Validate student existence
            var existingStudent = _studentRepository.GetStudentById(studentId);
            if (existingStudent == null)
            {
                throw new StudentNotFoundException("Student not found.");
            }

            // Call the repository method to delete the student
            _studentRepository.DeleteStudent(studentId);
            Console.WriteLine("Student deleted successfully.");
        }

        // Method to get a student by their ID
        public Student GetStudentById(int studentId)
        {
            // Check if the repository is initialized
            if (_studentRepository == null)
            {
                throw new InvalidOperationException("Student repository is not initialized.");
            }

            // Call the repository method to retrieve the student
            var student = _studentRepository.GetStudentById(studentId);

            // Check if the student exists
            if (student == null)
            {
                throw new StudentNotFoundException("Student not found.");
            }

            return student;
        }

        public List<Student> GetAllStudents()
        {
            // Retrieve all students
            var students = _studentRepository.GetAllStudents();
            if (students == null || !students.Any())
            {
                Console.WriteLine("No students found.");
            }
            return students;
        }

        public void MakePayment(Student student, decimal amount, DateTime paymentDate)
        {
            // Validate student existence
            var existingStudent = _studentRepository.GetStudentById(student.StudentID);
            if (existingStudent == null)
            {
                throw new StudentNotFoundException("Student not found.");
            }

            // Validate payment details
            if (amount <= 0)
            {
                throw new PaymentValidationException("Payment amount must be greater than zero.");
            }

            // Record the payment
            var payment = new Payment
            {
                StudentID = student.StudentID,
                Amount = amount,
                PaymentDate = paymentDate
            };
            _paymentRepository.AddPayment(payment);
            Console.WriteLine("Payment recorded successfully.");
        }

        //public List<Course> GetEnrolledCourses(Student student)
        //{
        //    // Validate student existence
        //    var existingStudent = _studentRepository.GetStudentById(student.StudentID);
        //    if (existingStudent == null)
        //    {
        //        throw new StudentNotFoundException("Student not found.");
        //    }

        //    // Retrieve and return enrolled courses
        //    return _enrollmentRepository.GetEnrolledCourses(student.StudentID);
        //}
        public List<Course> GetEnrolledCourses(Student student)
        {
            // Check if the student exists (you might want to implement a method to verify this)
            if (student == null || student.StudentID <= 0)
            {
                throw new StudentNotFoundException("Student not found.");
            }

            // Get all enrollments for the student
            var enrollments = _enrollmentRepository.GetEnrollmentsByStudent(student.StudentID);

            List<Course> enrolledCourses = new List<Course>();

            // Retrieve the corresponding courses based on the enrollments
            foreach (var enrollment in enrollments)
            {
                var course = _courseRepository.GetCourseById(enrollment.CourseID); // Assume this method retrieves a course by its ID
                if (course != null)
                {
                    enrolledCourses.Add(course);
                }
            }

            return enrolledCourses;
        }
        public List<Payment> GetPaymentHistory(Student student)
        {
            // Validate student existence
            var existingStudent = _studentRepository.GetStudentById(student.StudentID);
            if (existingStudent == null)
            {
                throw new StudentNotFoundException("Student not found.");
            }

            // Retrieve and return payment history
            return _paymentRepository.GetPaymentsByStudent(student.StudentID);
        }
 

    }
}
