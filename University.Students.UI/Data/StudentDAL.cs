using Microsoft.Data.SqlClient;
using University.Students.UI.Models;

namespace University.Students.UI.Data
{
    public class StudentDAL
    {
        private readonly string _connectionString;

        public StudentDAL(string connectionString) => _connectionString = connectionString;

        public Student GetStudentById(int Id)
        {
            Student student = new Student();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT Id, Name, Email, Age FROM Students WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", Id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                student.Id = reader.GetInt32(0);
                student.Name = reader.GetString(1);
                student.Email = reader.GetString(2);
                student.Age = reader.GetInt32(3);
            }

            return student;
        }

        public IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT Id, Name, Email, Age FROM Students", connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            return students;
        }

        public void AddStudent(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("INSERT INTO Students (Name, Email, Age) VALUES (@Name, @Email, @Age)", connection);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Email", student.Email);
            command.Parameters.AddWithValue("@Age", student.Age);

            command.ExecuteNonQuery();
        }

        public void EditStudent(Student student, string id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("UPDATE Students SET Name = @Name, Email = @Email, Age = @Age WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Email", student.Email);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public void DeleteStudent(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var command = new SqlCommand("DELETE FROM Students WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Log exception or handle it as needed
                throw new Exception("An error occurred while deleting the student.", ex);
            }
        }

    }
}
