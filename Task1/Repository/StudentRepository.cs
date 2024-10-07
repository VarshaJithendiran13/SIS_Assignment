using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;
using StudentInfoSys.Utilities;
using StudentInfoSys.Models;


namespace StudentInfoSys.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SqlConnection _connection;

        public StudentRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
        }

        public Student GetStudentById(int studentID)
        {
            Student student = null;
            string query = "SELECT * FROM Students WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    student = new Student
                    {
                        StudentID = (int)reader["StudentID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }

                _connection.Close();
            }

            return student;
        }
        public void DeleteStudent(int studentID)
        {
            string query = "DELETE FROM Students WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                _connection.Open();

                // Execute the command
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // If no rows were affected, throw an exception
                    throw new Exception("Student not found.");
                }

                _connection.Close();
            }
        }
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT * FROM Students";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                        students.Add(student);
                    }
                }
                _connection.Close();
            }

            return students;
        }

        public void AddStudent(Student student)
        {
            string query = "INSERT INTO Students (FirstName, LastName, DateOfBirth, Email, PhoneNumber) VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void UpdateStudent(Student student)
        {
            string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Email = @Email, PhoneNumber = @PhoneNumber WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@StudentID", student.StudentID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }


        public List<Course> GetEnrolledCourses(int studentID)
        {
            List<Course> courses = new List<Course>();
            string query = @"
                SELECT c.CourseID, c.CourseName, c.Credits 
                FROM Courses c
                JOIN Enrollments e ON c.CourseID = e.CourseID
                WHERE e.StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseName = reader["CourseName"].ToString(),
                        Credits = (int)reader["Credits"]
                    });
                }

                _connection.Close();
            }

            return courses;
        }

        public List<Payment> GetPaymentHistory(int studentID)
        {
            List<Payment> payments = new List<Payment>();
            string query = "SELECT * FROM Payments WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    payments.Add(new Payment
                    {
                        PaymentID = (int)reader["PaymentID"],
                        StudentID = (int)reader["StudentID"],
                        Amount = (decimal)reader["Amount"],
                        PaymentDate = (DateTime)reader["PaymentDate"]
                    });
                }

                _connection.Close();
            }

            return payments;
        }
    }
}

