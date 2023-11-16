using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;

/// <summary>
/// Query which returns the requested <see cref="ToDoListDto"/>.
/// </summary>
/// <param name="Id">The ID of the requested <see cref="ToDoListDto"/></param>
public record GetToDoListByIdQuery(Guid Id) : IRequest<ToDoListDto>;

public class GetToDoListByIdHandler : IRequestHandler<GetToDoListByIdQuery, ToDoListDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetToDoListByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ToDoListDto> Handle(GetToDoListByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repo = _unitOfWork.GetRepository<ToDoList>();
            var list = await repo.GetByIdAsync(request.Id);

            return _mapper.Map<ToDoListDto>(list);
        }
        catch (Exception e)
        {
            throw new RepositoryException(request.GetType().Name, e);
        }
    }
}