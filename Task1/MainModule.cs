using StudentInfoSys.Models;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;
using StudentInfoSys.Services;
using StudentInfoSys.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSys
{
    public class MainModule
        {

            private static IStudentService _studentService;
            private static ICourseService _courseService;
            private static ITeacherService _teacherService;
            private static IPaymentService _paymentService;
            private static IEnrollmentService _enrollmentService;

            static void Main(string[] args)
            {
                // Initialize services (these would typically be injected via dependency injection)
                _studentService = new StudentService(new StudentRepository(), new CourseRepository(), new PaymentRepository(), new EnrollmentRepository());
                _courseService = new CourseService(new CourseRepository(), new TeacherRepository());
                _teacherService = new TeacherService(new TeacherRepository(), new CourseRepository());
                _paymentService = new PaymentService(new PaymentRepository());
                _enrollmentService = new EnrollmentService(new EnrollmentRepository());


            while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Welcome to the Student Information System");
                    Console.ResetColor();

                    Console.WriteLine("Select your role:");
                    Console.WriteLine("1. Student");
                    Console.WriteLine("2. Teacher");
                    Console.WriteLine("3. Management");
                    Console.WriteLine("4. Exit");
                    Console.Write("Enter your choice: ");

                    int roleChoice = Convert.ToInt32(Console.ReadLine());

                    switch (roleChoice)
                    {
                        case 1:
                            StudentMenu();
                            break;
                        case 2:
                            TeacherMenu();
                            break;
                        case 3:
                            ManagementMenu();
                            break;
                        case 4:
                            Console.WriteLine("Exiting the system...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            private static void StudentMenu()
            {
                Console.Clear();
                Console.WriteLine("Student Menu");

                Console.Write("Enter your Student ID: ");
                int studentId = Convert.ToInt32(Console.ReadLine());
                var student = _studentService.GetStudentById(studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, {student.FirstName} {student.LastName}");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Enroll in a course");
                    Console.WriteLine("2. View enrolled courses");
                    Console.WriteLine("3. View payment history");
                    Console.WriteLine("4. Go back");
                    Console.Write("Enter your choice: ");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            EnrollInCourse(student);
                            break;
                        case 2:
                            ViewEnrolledCourses(student);
                            break;
                        case 3:
                            ViewPaymentHistory(student);
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }

            private static void TeacherMenu()
            {
                Console.Clear();
                Console.WriteLine("Teacher Menu");

                Console.Write("Enter your Teacher ID: ");
                int teacherId = Convert.ToInt32(Console.ReadLine());
                var teacher = _teacherService.GetTeacherById(teacherId);

                if (teacher == null)
                {
                    Console.WriteLine("Teacher not found.");
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, {teacher.FirstName} {teacher.LastName}");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. View assigned courses");
                    Console.WriteLine("2. View students in your courses");
                    Console.WriteLine("3. Go back");
                    Console.Write("Enter your choice: ");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            ViewAssignedCourses(teacher.TeacherID);
                            break;
                        case 2:
                            ViewStudentsInCourses(teacher.TeacherID);
                            break;
                        case 3:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }

            private static void ManagementMenu()
            {
                Console.Clear();
                Console.WriteLine("Management Menu");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. View all students");
                Console.WriteLine("2. View all courses");
                Console.WriteLine("3. View all teachers");
                Console.WriteLine("4. View all payments");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ViewAllStudents();
                        break;
                    case 2:
                        ViewAllCourses();
                        break;
                    case 3:
                        ViewAllTeachers();
                        break;
                    case 4:
                        ViewAllPayments();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            private static void EnrollInCourse(Student student)
            {
                Console.Clear();
                Console.WriteLine("Enroll in Course");

                Console.Write("Enter the Course Name: ");
                string courseName = Console.ReadLine();
                var course = _courseService.GetCourseByName(courseName);

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                _enrollmentService.EnrollInCourse(student, course);
                Console.WriteLine($"Successfully enrolled in {courseName}.");
            }

            private static void ViewEnrolledCourses(Student student)
            {
                Console.Clear();
                Console.WriteLine("Enrolled Courses");

                var courses = _studentService.GetEnrolledCourses(student);
                Console.WriteLine($"Courses for {student.FirstName} {student.LastName}:");
                Console.WriteLine("------------------------------------------------");
                foreach (var course in courses)
                {
                    Console.WriteLine($"- {course.CourseName}");
                }
                Console.WriteLine("------------------------------------------------");
            }

            private static void ViewPaymentHistory(Student student)
            {
                Console.Clear();
                Console.WriteLine("Payment History");

                var payments = _paymentService.GetPaymentHistory(student);
                Console.WriteLine($"Payments for {student.FirstName} {student.LastName}:");
                Console.WriteLine("------------------------------------------------");
                foreach (var payment in payments)
                {
                    Console.WriteLine($"- Amount: {payment.Amount:C}, Date: {payment.PaymentDate.ToShortDateString()}");
                }
                Console.WriteLine("------------------------------------------------");
            }

            private static void ViewAssignedCourses(int teacherID)
            {
                Console.Clear();
                Console.WriteLine("Assigned Courses");

                // Retrieve the teacher using the teacherID
                var teacher = _teacherService.GetTeacherById(teacherID);

                if (teacher == null)
                {
                    Console.WriteLine("Teacher not found.");
                    return;
                }

                var courses = _teacherService.GetAssignedCourses(teacherID);
                Console.WriteLine($"Courses assigned to {teacher.FirstName} {teacher.LastName}:");
                Console.WriteLine("------------------------------------------------");

                if (courses.Count == 0)
                {
                    Console.WriteLine("No courses assigned.");
                }
                else
                {
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"- {course.CourseName}");
                    }
                }

                Console.WriteLine("------------------------------------------------");
            }


            private static void ViewAllStudents()
                {
                    Console.Clear();
                    Console.WriteLine("All Students");
                    // Assuming there's a method to get all students
                    var students = _studentService.GetAllStudents();
                    foreach (var student in students)
                    {
                        Console.WriteLine($"- {student.FirstName} {student.LastName}, ID: {student.StudentID}");
                    }
                    Console.WriteLine("------------------------------------------------");
                }

                private static void ViewAllCourses()
                {
                    Console.Clear();
                    Console.WriteLine("All Courses");
                    // Assuming there's a method to get all courses
                    var courses = _courseService.GetAllCourses();
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"- {course.CourseName}, ID: {course.CourseID}");
                    }
                    Console.WriteLine("------------------------------------------------");
                }

                private static void ViewAllTeachers()
                {
                    Console.Clear();
                    Console.WriteLine("All Teachers");
                    // Assuming there's a method to get all teachers
                    var teachers = _teacherService.GetAllTeachers();
                    foreach (var teacher in teachers)
                    {
                        Console.WriteLine($"- {teacher.FirstName} {teacher.LastName}, ID: {teacher.TeacherID}");
                    }
                    Console.WriteLine("------------------------------------------------");
                }

                private static void ViewAllPayments()
                {
                    Console.Clear();
                    Console.WriteLine("All Payments");
                    // Assuming there's a method to get all payments
                    var payments = _paymentService.GetAllPayments();
                    foreach (var payment in payments)
                    {
                        Console.WriteLine($"- Student ID: {payment.StudentID}, Amount: {payment.Amount:C}, Date: {payment.PaymentDate.ToShortDateString()}");
                    }
                    Console.WriteLine("------------------------------------------------");
                }
                private static void ViewStudentsInCourses(int teacherID)
                {
                     Console.Clear();
                     Console.WriteLine("Students in Your Courses");

                // Retrieve the teacher using the teacherID
                     var teacher = _teacherService.GetTeacherById(teacherID);

                    if (teacher == null)
                    {
                        Console.WriteLine("Teacher not found.");
                        return;
                    }

                    var courses = _teacherService.GetAssignedCourses(teacherID);
                    Console.WriteLine($"Students in courses taught by {teacher.FirstName} {teacher.LastName}:");

                    foreach (var course in courses)
                    {
                        var enrollments = _courseService.GetEnrollments(course);
                        Console.WriteLine($"Course: {course.CourseName}");

                        if (enrollments.Count == 0)
                        {
                            Console.WriteLine("No students enrolled in this course.");
                        }
                        else
                        {
                            foreach (var enrollment in enrollments)
                            {
                                var student = _studentService.GetStudentById(enrollment.StudentID);
                                if (student != null) // Check if the student is found
                                {
                                    Console.WriteLine($"- {student.FirstName} {student.LastName}");
                                }
                                else
                                {
                                    Console.WriteLine($"- Student ID {enrollment.StudentID} not found.");
                                }
                            }
                        }
                        Console.WriteLine("------------------------------------------------");
                    }
                }


    }
}
