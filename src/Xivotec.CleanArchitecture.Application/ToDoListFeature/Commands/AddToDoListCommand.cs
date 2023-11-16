using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which saves a <see cref="ToDoListDto"/> to the repository.
/// </summary>
/// <param name="Item">The <see cref="ToDoListDto"/> to be saved.</param>
public record AddToDoListCommand(ToDoListDto Item) : IRequest<ToDoListDto>;

public class AddToDoListHandler : IRequestHandler<AddToDoListCommand, ToDoListDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddToDoListHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ToDoListDto> Handle(AddToDoListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var itemToAdd = _mapper.Map<ToDoList>(request.Item);
            var repo = _unitOfWork.GetRepository<ToDoList>();

            var result = await repo.AddAsync(itemToAdd);
            return _mapper.Map<ToDoListDto>(result);
        }
        catch (Exception e)
        {
            throw new RepositoryException(request.GetType().Name, e);
        }
    }
}