using System.Transactions;
using AutoMapper;
using WebAppDatabase.DAO;
using WebAppDatabase.DTO;
using WebAppDatabase.Exceptions;
using WebAppDatabase.Models;

namespace WebAppDatabase.Services;
/// <summary>
/// Initializes a new instance of the StudentServiceImpl class.
/// This constructor uses Dependency Injection (DI) to receive required dependencies.
/// </summary>
/// <param name="studentDAO">Data Access Object (DAO) for student operations.</param>
/// <param name="mapper">AutoMapper instance for DTO to Entity mapping.</param>
/// <param name="logger">Logger instance for logging errors and operations.</param>
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
    /// <summary>
    /// 
    /// <summary>
    /// Inserts a new student into the database.
    /// </summary>
    /// <param name="studentInsertDTO">DTO containing student details.</param>
    /// <returns>Returns a StudentReadonlyDTO containing inserted student details.</returns>
    /// <exception cref="TransactionException">Thrown when a transaction fails.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
    /// </summary>
    /// <param name="studentDao"></param>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
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
    
    /// <summary>
    /// Updates an existing student's information.
    /// </summary>
    /// <param name="studentUpdateDTO">DTO containing updated student details.</param>
    /// <exception cref="StudentNotFoundException">Thrown when the student is not found.</exception>
    /// <exception cref="TransactionException">Thrown when a transaction fails.</exception>
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
            scope.Complete();
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogError("Error. Student with id {0} not found. {1}", studentUpdateDTO.Id, ex.Message);
            throw;
        }
        catch (TransactionException ex)
        {
            _logger.LogError("Error. Student with {Firstname} {Lastname} not updated} . {ErrorMessage}",
                studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error. Student with {Id} {Firstname} {Lastname} not updated} . {ErrorMessage}",
                studentUpdateDTO.Id, studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
            throw;
        }
    }
    /// <summary>
    /// Deletes a student by ID.
    /// </summary>
    /// <param name="id">Student ID to delete.</param>
    /// <exception cref="StudentNotFoundException">Thrown when the student is not found.</exception>
    /// <exception cref="TransactionException">Thrown when a transaction fails.</exception>
    public void DeleteStudent(int id)
    {
        try
        {
            using TransactionScope scope = new();
            if (_studentDAO.GetStudentById(id) == null)
            {
                throw new StudentNotFoundException($"Student with id {id} not found");
            }
            _studentDAO.DeleteStudent(id);
            scope.Complete();
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogError("Error. Student with id {Id} not found. {ErrorMessage}",id, ex.Message);
            throw;
        }
        catch (TransactionException ex)
        {
            _logger.LogError("Error. Student with id {Id} not deleted. {ErrorMessage}",id, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error. Student with id {Id} could not deleted. {ErrorMessage}", id ,ex.Message);
            throw;
        }
    }
    /// <summary>
    /// Retrieves a student by ID.
    /// </summary>
    /// <param name="id">Student ID to retrieve.</param>
    /// <returns>Returns a StudentReadonlyDTO containing the student's details.</returns>
    /// <exception cref="StudentNotFoundException">Thrown when the student is not found.</exception>
    public StudentReadonlyDTO GetStudent(int id)
    {
        StudentReadonlyDTO studentReadonlyDTO;
        Student? student;

        try
        {
            student = _studentDAO.GetStudentById(id);
            if (_studentDAO.GetStudentById(id) == null)
            {
                throw new StudentNotFoundException($"Student with id {id} not found");
            }
            studentReadonlyDTO = _mapper.Map<StudentReadonlyDTO>(student);
            return studentReadonlyDTO;
            
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogError("Error. Student with id {Id} not found. {ErrorMessage}", id, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error. Student with id {Id} could not found. {ErrorMessage}", id, ex.Message);
            throw;
        }
    }
    /// <summary>
    /// Retrieves all students from the database.
    /// </summary>
    /// <returns>Returns a list of StudentReadonlyDTO containing student details.</returns>
    /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
    public List<StudentReadonlyDTO> GetAllStudents()
    {
        StudentReadonlyDTO studentReadonlyDTO;
        List<StudentReadonlyDTO> studentsReadonlyDTOs = new();
        List<Student> students;

        try
        {
            students = _studentDAO.GetAll();
            foreach (Student student in students)
            {
                studentReadonlyDTO = _mapper.Map<StudentReadonlyDTO>(student);
                studentsReadonlyDTOs.Add(studentReadonlyDTO);
            }
            return studentsReadonlyDTOs;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error. Students not found. {ErrorMessage}", ex.Message);
            throw;
        }
    }
}