using System.Transactions;
using AutoMapper;
using WebAppDatabase.DAO;
using WebAppDatabase.DTO;
using WebAppDatabase.Exceptions;
using WebAppDatabase.Models;

namespace WebAppDatabase.Services;

public class StudentServiceImpl : IStudentService
{
    private readonly IStudentDAO _studentDAO;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentServiceImpl> _logger;

    //This constructor of StudentServiceImpl uses Dependency Injection (DI) to receive required dependencies
    //(IStudentDAO, IMapper, and ILogger). This follows the Inversion of Control (IoC) principle,
    //where the dependencies are not created inside the class but injected from an external container
    //(usually configured in ASP.NET Core's DI system).
    public StudentServiceImpl(IStudentDAO studentDao, IMapper mapper, ILogger<StudentServiceImpl> logger)
    {
        _studentDAO = studentDao;
        _mapper = mapper;
        _logger = logger;
    }

    public StudentReadonlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO)
    {
        StudentReadonlyDTO? studentReadonly;

        try
        {
            using TransactionScope scope = new TransactionScope();

            Student student = _mapper.Map<Student>(studentInsertDTO); // converts StudentInsertDTO → Student entity.
            Student? insertedStudent = _studentDAO.InsertStudent(student); // insert the Student entity into the database, and the database generates an ID.
            studentReadonly = _mapper.Map<StudentReadonlyDTO>(insertedStudent); // convert the inserted Student → StudentReadonlyDTO, which does not include the ID.

            scope.Complete();
            return studentReadonly;
        }
        catch (TransactionException ex)
        {
            _logger.LogError(ex, "Error. Student {Firstname} {Lastname} not inserted",
                studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error. Student {Firstname} {Lastname} not inserted",
                studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
            throw;
        }
    }

    public void UpdateStudent(StudentUpdateDTO studentUpdateDTO)
    {
        Student student = new Student();

        try
        {
            using TransactionScope scope = new TransactionScope();
            if (_studentDAO.GetStudentById(studentUpdateDTO.Id) == null)
            {
                throw new StudentNotFoundException($"Student with id {studentUpdateDTO.Id} not found");
            }
            student = _mapper.Map<Student>(studentUpdateDTO);
            _studentDAO.UpdateStudent(student);
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogError("Error. Student with id {0} not found. {1}", studentUpdateDTO.Id, ex.Message);
        }
        catch (TransactionException ex)
        {
            _logger.LogError("Error. Student with {Firstname} {Lastname} not updated} . {ErrorMessage}",
                studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
        }
    }

    public void DeleteStudent(int id)
    {
        throw new NotImplementedException();
    }

    public StudentReadonlyDTO? GetStudent(int id)
    {
        throw new NotImplementedException();
    }

    public List<StudentReadonlyDTO> GetAllStudents()
    {
        throw new NotImplementedException();
    }
}