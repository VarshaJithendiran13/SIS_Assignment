using StudentInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInfoSys.Utilities;
using StudentInfoSys.Repository;
using StudentInfoSys.Services.Interfaces;

namespace StudentInfoSys.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SqlConnection _connection;

        public CourseRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
        }

        public Course GetCourseById(int courseID)
        {
            Course course = null;
            string query = "SELECT * FROM Courses WHERE CourseID = @CourseID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    course = new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseName = reader["CourseName"].ToString(),
                        Credits = (int)reader["Credits"],
                        TeacherID = (int)reader["TeacherID"]
                    };
                }

                _connection.Close();
            }

            return course;
        }

        public void AddCourse(Course course)
        {
            string query = "INSERT INTO Courses (CourseName, Credits, TeacherID) VALUES (@CourseName, @Credits, @TeacherID)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@Credits", course.Credits);
                cmd.Parameters.AddWithValue("@TeacherID", course.TeacherID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }


        public void UpdateCourse(Course course)
        {
            string query = "UPDATE Courses SET CourseName = @CourseName, Credits = @Credits, TeacherID = @TeacherID WHERE CourseID = @CourseID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@Credits", course.Credits);
                cmd.Parameters.AddWithValue("@TeacherID", course.TeacherID);
                cmd.Parameters.AddWithValue("@CourseID", course.CourseID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteCourse(int courseID)
        {
            string query = "DELETE FROM Courses WHERE CourseID = @CourseID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Enrollment> GetEnrollments(int courseID)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            string query = @"
                SELECT e.EnrollmentID, e.StudentID, e.CourseID, e.EnrollmentDate
                FROM Enrollments e
                WHERE e.CourseID = @CourseID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseID);

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

        public Teacher GetAssignedTeacher(int courseID)
        {
            Teacher teacher = null;
            string query = @"
                SELECT t.TeacherID, t.FirstName, t.LastName, t.Email
                FROM Teachers t
                JOIN Courses c ON t.TeacherID = c.TeacherID
                WHERE c.CourseID = @CourseID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseID);

                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    teacher = new Teacher
                    {
                        TeacherID = (int)reader["TeacherID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                }

                _connection.Close();
            }

            return teacher;
        }
        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            string query = "SELECT * FROM Courses";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseName = reader["CourseName"].ToString(),
                            Credits = (int)reader["Credits"],
                            TeacherID = (int)reader["TeacherID"]
                        };
                        courses.Add(course);
                    }
                }
                _connection.Close();
            }

            return courses;
        }
        public Course GetCourseByName(string courseName)
        {
            Course course = null;
            string query = "SELECT * FROM Courses WHERE CourseName = @CourseName";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseName", courseName);

                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        course = new Course
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseName = (string)reader["CourseName"],
                            Credits = (int)reader["Credits"],
                            TeacherID = (int)reader["TeacherID"] 
                        };
                    }
                }
                _connection.Close();
            }

            return course;
        }
    }
}


