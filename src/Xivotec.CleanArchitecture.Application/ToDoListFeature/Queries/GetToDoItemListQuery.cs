using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="ToDoListDto"/> in the repository.
/// </summary>
public record GetToDoItemListQuery : IRequest<List<ToDoItemDto>>;

public class GetToDoItemListQueryHandler : IRequestHandler<GetToDoItemListQuery, List<ToDoItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetToDoItemListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ToDoItemDto>> Handle(GetToDoItemListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repo = _unitOfWork.GetRepository<ToDoItem>();
            var lists = await repo.GetAllAsync();

            return lists.Select(_mapper.Map<ToDoItemDto>).ToList();
        }
        catch (Exception e)
        {
            throw new RepositoryException(request.GetType().Name, e);
        }
    }
}