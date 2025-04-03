using WebAppDatabase.DTO;
using WebAppDatabase.Models;

namespace WebAppDatabase.Services;

public interface ITeacherService
{
    TeacherReadOnlyDTO? InsertTeacher(TeacherInsertDTO teacherInsertDto);
    void UpdateTeacher(TeacherUpdateDTO teacherUpdateDto);
    void DeleteTeacher(int id);
    TeacherReadOnlyDTO GetTeacherById(int id);
    TeacherReadOnlyDTO GetTeacherByEmail(string email);
    TeacherReadOnlyDTO GetTeacherByTaxNumber(string taxNumber);
    List<TeacherReadOnlyDTO> GetAllTeachers();
}