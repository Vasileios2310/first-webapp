using System.Transactions;
using AutoMapper;
using WebAppDatabase.DAO;
using WebAppDatabase.DTO;
using WebAppDatabase.Exceptions;
using WebAppDatabase.Models;

namespace WebAppDatabase.Services;

public class TeacherServiceImpl : ITeacherService
{
    
    private readonly ITeacherDAO _teacherDAO;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherServiceImpl> _logger;

    public TeacherServiceImpl(ITeacherDAO teacherDao, IMapper mapper, ILogger<TeacherServiceImpl> logger)
    {
        _teacherDAO = teacherDao;
        _mapper = mapper;
        _logger = logger;
    }

    public TeacherReadOnlyDTO? InsertTeacher(TeacherInsertDTO teacherInsertDto)
    {
        TeacherReadOnlyDTO teacherReadOnlyDto;

        try
        {
            using TransactionScope scope = new();
            Teacher teacher = _mapper.Map<Teacher>(teacherInsertDto);
            Teacher? insertedTeacher = _teacherDAO.Insert(teacher);
            teacherReadOnlyDto = _mapper.Map<TeacherReadOnlyDTO>(insertedTeacher);
            scope.Complete();
            return teacherReadOnlyDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error inserting teacher {FirstName} {LastName} {TaxNumber} {Email} ", 
                teacherInsertDto.Firstname , teacherInsertDto.Lastname, teacherInsertDto.TaxNumber, teacherInsertDto.Email);
            throw;
        }
    }

    public void UpdateTeacher(TeacherUpdateDTO teacherUpdateDto)
    {
        try
        {
            using TransactionScope scope = new();
            if (_teacherDAO.GetTeacherById(teacherUpdateDto.Id) == null)
            {
                throw new TeacherNotFoundException($"Teacher with Id: {teacherUpdateDto.Id} not found");
            }

            Teacher teacher = _mapper.Map<Teacher>(teacherUpdateDto);
            _teacherDAO.Update(teacher);
            scope.Complete();
        }
        catch (TeacherNotFoundException ex)
        {
            _logger.LogError("Error,  teacher with {Id} not found {ErrorMessage}", teacherUpdateDto.Id, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating teacher {Firstname} {Lastname} not updated {ErrorMessage}", 
                teacherUpdateDto.Firstname , teacherUpdateDto.Lastname,ex.Message);
        }
    }

    public void DeleteTeacher(int id)
    {
        
    }

    public TeacherReadOnlyDTO GetTeacherById(int id)
    {
        throw new NotImplementedException();
    }

    public TeacherReadOnlyDTO GetTeacherByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public TeacherReadOnlyDTO GetTeacherByTaxNumber(string taxNumber)
    {
        throw new NotImplementedException();
    }

    public List<TeacherReadOnlyDTO> GetAllTeachers()
    {
        throw new NotImplementedException();
    }
}