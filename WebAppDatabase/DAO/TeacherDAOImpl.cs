using Microsoft.Data.SqlClient;
using WebAppDatabase.Models;
using WebAppDatabase.Services.DBHelper;

namespace WebAppDatabase.DAO;

public class TeacherDAOImpl : ITeacherDAO 
{
    public Teacher? Insert(Teacher teacher)
    {
        Teacher? teacherToReturn = null;
        string sql = "INSERT INTO Teachers (FirstName, LastName, Email, PhoneNumber) VALUES (@firstname, @lastname, @email, @phone); " +
                     "SELECT SCOPE_IDENTITY()";

        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@firstname", teacher.Firstname);
        command.Parameters.AddWithValue("@lastname", teacher.Lastname);
        command.Parameters.AddWithValue("@taxNumber", teacher.TaxNumber);
        command.Parameters.AddWithValue("@email", teacher.Email);
        
        int studentId = (int)command.ExecuteScalar();
        
        string sql2 = "Select * from Teachers where Id = @teacherId";
        
        using SqlCommand command2 = new SqlCommand(sql2, connection);
        command2.Parameters.AddWithValue("@teacherId", studentId);
        
        using SqlDataReader reader = command2.ExecuteReader();

        if (reader.Read())
        {
            teacherToReturn = new Teacher()
            {
                Id = (int)reader["id"],
                Firstname = (string)reader["firstname"],
                Lastname = (string)reader["lastname"],
                Email = (string)reader["email"],
                TaxNumber = (string)reader["taxNumber"],
            };
        }
        return teacherToReturn;
    }

    public void Update(Teacher teacher)
    {
        string sql = "UPDATE Teachers SET Firstname = @firstname , Lastname=@Lastname , Email = @email , TaxNumber = @taxNumber Where Id = @teacherId";
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", teacher.Id);
        command.Parameters.AddWithValue("@firstname", teacher.Firstname);
        command.Parameters.AddWithValue("@lastname", teacher.Lastname);
        command.Parameters.AddWithValue("@email", teacher.Email);
        command.Parameters.AddWithValue("@taxNumber", teacher.TaxNumber);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        string sql = "DELETE FROM Teachers WHERE Id = @teacherId";
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@teacherId", id);
        
        command.ExecuteNonQuery();
    }

    public Teacher? GetTeacherById(int id)
    {
        Teacher? teacherToReturn = null;
        string sql = "SELECT * FROM Teachers WHERE Id = @teacherId";
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@teacherId", id);
        
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            teacherToReturn = new Teacher()
            {
                Id = (int)reader["id"],
                Firstname = (string)reader["firstname"],
                Lastname = (string)reader["lastname"],
                Email = (string)reader["email"],
                TaxNumber = (string)reader["taxNumber"],
            };
        }
        return teacherToReturn;
    }

    public Teacher? GetTeacherByEmail(string email)
    {
       Teacher? teacherToReturn = null;
       string sql = "SELECT * FROM Teachers WHERE Email = @email";
       using SqlConnection connection = DBUtil.GetConnection();
       connection.Open();
       
       using SqlCommand command = new SqlCommand(sql, connection);
       command.Parameters.AddWithValue("@email", email);
       
       using SqlDataReader reader = command.ExecuteReader();

       if (reader.Read())
       {
           teacherToReturn = new Teacher()
           {
               Id = (int)reader["id"],
               Email = (string)reader["email"],
           };
       }
       return teacherToReturn;
    }

    public Teacher? GetTeacherByTax(string taxNumber)
    {
        Teacher? teacherToReturn = null;
        
        string sql = "SELECT * FROM Teachers WHERE TaxNumber = @taxNumber";
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@taxNumber", taxNumber);
        
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            teacherToReturn = new Teacher()
            {
                Id = (int)reader["id"],
                TaxNumber = (string)reader["taxNumber"],
            };
        }
        return teacherToReturn;
    }

    public List<Teacher> GetTeachers()
    {
        string sql = "SELECT * FROM Teachers";
        List<Teacher> teachersList = [];
        
        using SqlConnection connection = DBUtil.GetConnection();
        connection.Open();
        
        using SqlCommand command = new SqlCommand(sql, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            teachersList.Add(new Teacher()
            {
                Id = (int)reader["id"],
                Firstname = (string)reader["firstname"],
                Lastname = (string)reader["lastname"],
                Email = (string)reader["email"],
                TaxNumber = (string)reader["taxNumber"],
            });
        }
        return teachersList;
    }
}