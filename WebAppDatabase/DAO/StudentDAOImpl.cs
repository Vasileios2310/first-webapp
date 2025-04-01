using Microsoft.Data.SqlClient;
using WebAppDatabase.Models;
using WebAppDatabase.Services.DBHelper;

namespace WebAppDatabase.DAO;

public class StudentDAOImpl : IStudentDAO

{
    public Student? InsertStudent(Student student)
    {
        // 2. Declaring Variables
        Student? studentToReturn = null;
        int insertedId = 0;
        
        // 3. SQL Query for Insertion
        // Inserts a new student into the Students table with Firstname and Lastname.
        // Retrieves the last inserted ID using SCOPE_IDENTITY(),
        // which returns the identity value of the last inserted row within the same session
        string sql1 = "INSERT INTO Students (Firstname , Lastname) VALUES (@firstname, @lastname); " + 
                     "SELECT SCOPE_IDENTITY();";
        
        // 4. Database Connection
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        // 5. Executing the Insertion Query
        using SqlCommand command1 = new SqlCommand(sql1, connection);
        command1.Parameters.AddWithValue("@firstname", student.Firstname);
        command1.Parameters.AddWithValue("@lastname", student.Lastname);
        
        // 6. Retrieving the Inserted ID
        object insertedObject = command1.ExecuteScalar();
        if (insertedObject != null)
        {
            if (!int.TryParse(insertedObject.ToString(), out insertedId))
            {
                throw new Exception("Inserted object is invalid");
            }
        }
        
        // 7. Querying the Inserted Student
        string sql2 = "SELECT * FROM Students WHERE Id = @studentsId;";
        using SqlCommand command2 = new SqlCommand(sql2, connection);
        command2.Parameters.AddWithValue("@studentsId", insertedId);
        
        using SqlDataReader reader = command2.ExecuteReader();
        if (reader.Read())
        {
            studentToReturn = new Student()
            {
                Id = (int)reader["Id"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"], 
            };
        }
        return studentToReturn;
    }

    public void UpdateStudent(Student student)
    {
        string sql = "UPDATE Students SET Firstname = @firstname, Lastname = @lastname WHERE Id = @id; ";
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", student.Id);
        command.Parameters.AddWithValue("@firstname", student.Firstname);
        command.Parameters.AddWithValue("@lastname", student.Lastname);
        
        command.ExecuteNonQuery();
        
    }

    public void DeleteStudent(int id)
    {
        string sql = "DELETE FROM Students Where Id = @id; ";
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command .Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public Student? GetStudentById(int id)
    {
        Student? studentToReturn = null;

        string sql = "SELECT * FROM Students WHERE Id = @id; ";
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            studentToReturn = new Student()
            {
                Id = (int)reader["Id"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"],
            };
        }
        return studentToReturn;
    }

    public List<Student> GetAll()
    {
        List<Student> students = [];
        string sql = "SELECT * FROM Students; ";
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            students.Add(new Student()
            {
                Id = (int)reader["Id"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"],
            });
        }
        return students;
    }
}