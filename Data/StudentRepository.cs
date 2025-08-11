using Microsoft.Data.Sqlite;
using StudentManagementApp.Models;
using System.Data;

namespace StudentManagementApp.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository()
        {
            string dbPath = Path.Combine(Application.StartupPath, "StudentManagement.db");
            _connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentNumber TEXT UNIQUE NOT NULL,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    DateOfBirth TEXT NOT NULL,
                    Gender TEXT,
                    Email TEXT,
                    Phone TEXT,
                    Program TEXT,
                    Year INTEGER,
                    Address TEXT
                )";
            command.ExecuteNonQuery();
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Students ORDER BY LastName, FirstName";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32("Id"),
                    StudentNumber = reader.GetString("StudentNumber"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    DateOfBirth = DateTime.Parse(reader.GetString("DateOfBirth")),
                    Gender = reader.IsDBNull("Gender") ? "" : reader.GetString("Gender"),
                    Email = reader.IsDBNull("Email") ? "" : reader.GetString("Email"),
                    Phone = reader.IsDBNull("Phone") ? "" : reader.GetString("Phone"),
                    Program = reader.IsDBNull("Program") ? "" : reader.GetString("Program"),
                    Year = reader.IsDBNull("Year") ? 0 : reader.GetInt32("Year"),
                    Address = reader.IsDBNull("Address") ? "" : reader.GetString("Address")
                });
            }

            return students;
        }

        public List<Student> SearchStudents(string searchTerm)
        {
            var students = new List<Student>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Students 
                WHERE StudentNumber LIKE @SearchTerm 
                OR FirstName LIKE @SearchTerm 
                OR LastName LIKE @SearchTerm 
                OR Email LIKE @SearchTerm
                ORDER BY LastName, FirstName";
            command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32("Id"),
                    StudentNumber = reader.GetString("StudentNumber"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    DateOfBirth = DateTime.Parse(reader.GetString("DateOfBirth")),
                    Gender = reader.IsDBNull("Gender") ? "" : reader.GetString("Gender"),
                    Email = reader.IsDBNull("Email") ? "" : reader.GetString("Email"),
                    Phone = reader.IsDBNull("Phone") ? "" : reader.GetString("Phone"),
                    Program = reader.IsDBNull("Program") ? "" : reader.GetString("Program"),
                    Year = reader.IsDBNull("Year") ? 0 : reader.GetInt32("Year"),
                    Address = reader.IsDBNull("Address") ? "" : reader.GetString("Address")
                });
            }

            return students;
        }

        public Student? GetStudentById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Students WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt32("Id"),
                    StudentNumber = reader.GetString("StudentNumber"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    DateOfBirth = DateTime.Parse(reader.GetString("DateOfBirth")),
                    Gender = reader.IsDBNull("Gender") ? "" : reader.GetString("Gender"),
                    Email = reader.IsDBNull("Email") ? "" : reader.GetString("Email"),
                    Phone = reader.IsDBNull("Phone") ? "" : reader.GetString("Phone"),
                    Program = reader.IsDBNull("Program") ? "" : reader.GetString("Program"),
                    Year = reader.IsDBNull("Year") ? 0 : reader.GetInt32("Year"),
                    Address = reader.IsDBNull("Address") ? "" : reader.GetString("Address")
                };
            }

            return null;
        }

        public bool IsStudentNumberExists(string studentNumber, int excludeId = 0)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Students WHERE StudentNumber = @StudentNumber AND Id != @ExcludeId";
            command.Parameters.AddWithValue("@StudentNumber", studentNumber);
            command.Parameters.AddWithValue("@ExcludeId", excludeId);

            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        public int AddStudent(Student student)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Students (StudentNumber, FirstName, LastName, DateOfBirth, Gender, Email, Phone, Program, Year, Address)
                VALUES (@StudentNumber, @FirstName, @LastName, @DateOfBirth, @Gender, @Email, @Phone, @Program, @Year, @Address)";
            
            command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Gender", student.Gender ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Phone", student.Phone ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Program", student.Program ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Year", student.Year);
            command.Parameters.AddWithValue("@Address", student.Address ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
            return (int)connection.LastInsertRowId;
        }

        public bool UpdateStudent(Student student)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Students 
                SET StudentNumber = @StudentNumber, FirstName = @FirstName, LastName = @LastName, 
                    DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, Phone = @Phone, 
                    Program = @Program, Year = @Year, Address = @Address
                WHERE Id = @Id";
            
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Gender", student.Gender ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Phone", student.Phone ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Program", student.Program ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Year", student.Year);
            command.Parameters.AddWithValue("@Address", student.Address ?? (object)DBNull.Value);

            return command.ExecuteNonQuery() > 0;
        }

        public bool DeleteStudent(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Students WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }
    }
} 