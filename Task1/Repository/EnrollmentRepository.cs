using StudentInfoSys.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using StudentInfoSys.Utilities;
using StudentInfoSys.Services;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;

namespace StudentInfoSys.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly SqlConnection _connection;

        public EnrollmentRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
        }

        public Enrollment GetEnrollmentById(int enrollmentID)
        {
            Enrollment enrollment = null;
            string query = "SELECT * FROM Enrollments WHERE EnrollmentID = @EnrollmentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    enrollment = new Enrollment
                    {
                        EnrollmentID = (int)reader["EnrollmentID"],
                        StudentID = (int)reader["StudentID"],
                        CourseID = (int)reader["CourseID"],
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                    };
                }

                _connection.Close();
            }

            return enrollment;
        }
        public Course GetCourseById(int courseId)
        {
            string query = "SELECT * FROM Courses WHERE CourseID = @CourseID";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseName = reader["CourseName"].ToString(),
                        Credits = (int)reader["Credits"],
                        TeacherID = (int)reader["TeacherID"] 
                    };
                }
                _connection.Close();
            }
            return null; // Return null if the course is not found
        }
        public List<Enrollment> GetEnrollmentsByStudent(int studentId)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            string query = "SELECT * FROM Enrollments WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    enrollments.Add(new Enrollment
                    {
                        EnrollmentID = (int)reader["EnrollmentID"],
                        StudentID = (int)reader["StudentID"],
                        CourseID = (int)reader["CourseID"],
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                    });
                }

                _connection.Close();
            }

            return enrollments;
        }
        public void AddEnrollment(Enrollment enrollment)
        {
            string query = "INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate) VALUES (@StudentID, @CourseID, @EnrollmentDate)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", enrollment.StudentID);
                cmd.Parameters.AddWithValue("@CourseID", enrollment.CourseID);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollment.EnrollmentDate);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteEnrollment(int enrollmentID)
        {
            string query = "DELETE FROM Enrollments WHERE EnrollmentID = @EnrollmentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public Student GetStudentByEnrollment(int enrollmentID)
        {
            Student student = null;
            string query = @"
                SELECT s.StudentID, s.FirstName, s.LastName, s.Email, s.PhoneNumber
                FROM Students s
                JOIN Enrollments e ON s.StudentID = e.StudentID
                WHERE e.EnrollmentID = @EnrollmentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    student = new Student
                    {
                        StudentID = (int)reader["StudentID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }

                _connection.Close();
            }

            return student;
        }

        public Course GetCourseByEnrollment(int enrollmentID)
        {
            Course course = null;
            string query = @"
                SELECT c.CourseID, c.CourseName, c.Credits
                FROM Courses c
                JOIN Enrollments e ON c.CourseID = e.CourseID
                WHERE e.EnrollmentID = @EnrollmentID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    course = new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseName = reader["CourseName"].ToString(),
                        Credits = (int)reader["Credits"]
                    };
                }

                _connection.Close();
            }

            return course;
        }
        public bool IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            string query = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@CourseID", courseId);

                _connection.Open();
                int count = (int)cmd.ExecuteScalar();
                _connection.Close();

                return count > 0; // If count is greater than 0, student is enrolled
            }
        }
    }
}
