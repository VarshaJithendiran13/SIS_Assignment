using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfoSys.Models;
using StudentInfoSys.Utilities;
using System.Data.SqlClient;
using StudentInfoSys.Services;
using StudentInfoSys.Repository;

namespace StudentInfoSys.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SqlConnection _connection;

        public TeacherRepository()
        {
            _connection = new SqlConnection(DbConnUtil.GetConnString());
        }
        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            string query = "SELECT * FROM Teachers";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher
                        {
                            TeacherID = (int)reader["TeacherID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"]
                        };
                        teachers.Add(teacher);
                    }
                }
                _connection.Close();
            }

            return teachers;
        }
        public Teacher GetTeacherById(int TeacherID)
        {
            Teacher teacher = null;
            string query = "SELECT * FROM Teachers WHERE TeacherID = @TeacherID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@TeacherID", TeacherID);

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

        public void AddTeacher(Teacher teacher)
        {
            string query = "INSERT INTO Teachers (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            string query = "UPDATE Teachers SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE TeacherID = @TeacherID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);
                cmd.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteTeacher(int teacherID)
        {
            string query = "DELETE FROM Teachers WHERE TeacherID = @TeacherID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@TeacherID", teacherID);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Course> GetAssignedCourses(int teacherId)
        {
            List<Course> courses = new List<Course>();
            string query = "SELECT * FROM Courses WHERE TeacherID = @TeacherID"; // Assuming there's a TeacherID in Courses table

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var course = new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseName = reader["CourseName"].ToString(),
                        Credits = (int)reader["Credits"]
                    };
                    courses.Add(course);
                }

                _connection.Close();
            }

            return courses;
        }
    }
}