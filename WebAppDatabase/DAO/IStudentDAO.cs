using WebAppDatabase.Models;

namespace WebAppDatabase.DAO;

public interface IStudentDAO
{
    Student? InsertStudent(Student student);
    void  UpdateStudent(Student student);
    void DeleteStudent(Student student);
    Student? GetStudentById(int id);
    List<Student> GetAll();
}