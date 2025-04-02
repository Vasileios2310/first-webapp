using AutoMapper;
using WebAppDatabase.DTO;
using WebAppDatabase.Models;

namespace WebAppDatabase.Configuration;

public class MapperConfig : Profile
{
    protected MapperConfig()
    {
        // CreateMap<TSource, TDestination>() and reverse
        CreateMap<StudentInsertDTO, Student>().ReverseMap(); 
        CreateMap<StudentUpdateDTO, Student>().ReverseMap();
        CreateMap<StudentReadonlyDTO, Student>().ReverseMap();
    }
}