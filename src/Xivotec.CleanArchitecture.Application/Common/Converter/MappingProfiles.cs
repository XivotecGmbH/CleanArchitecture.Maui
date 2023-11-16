using AutoMapper;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.Common.Converter;

public class MappingProfiles : Profile
{
    /// <summary>
    /// Defines all available AutoMapper Profiles within the Application
    /// </summary>
    public MappingProfiles()
    {
        _ = CreateMap<ToDoList, ToDoListDto>().ReverseMap();
        _ = CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
    }
}
