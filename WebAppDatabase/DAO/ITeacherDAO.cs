using WebAppDatabase.Models;

namespace WebAppDatabase.DAO;

public interface ITeacherDAO
{
    Teacher? Insert(Teacher teacher);
    void Update(Teacher teacher);
    void Delete(int id);
    Teacher? GetTeacherById(int id);
    Teacher? GetTeacherByEmail(string email);
    Teacher? GetTeacherByTax(string taxNumber);
    List<Teacher> GetTeachers();
    
}