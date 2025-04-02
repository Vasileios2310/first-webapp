using WebAppDatabase.DTO;

namespace WebAppDatabase.Services;

public interface IStudentService
{
    StudentReadonlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO);
    void UpdateStudent(StudentUpdateDTO studentUpdateDTO);
    void DeleteStudent(int id);
    StudentReadonlyDTO GetStudent(int id);
    List<StudentReadonlyDTO> GetAllStudents();
    
}