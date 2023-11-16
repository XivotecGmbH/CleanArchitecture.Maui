using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which saves a <see cref="ToDoItemDto"/> to the repository.
/// </summary>
/// <param name="Item">The <see cref="ToDoItemDto"/> to be saved.</param>
public record AddToDoItemCommand(ToDoItemDto Item) : IRequest<ToDoItemDto>;

public class AddToDoItemHandler : IRequestHandler<AddToDoItemCommand, ToDoItemDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public AddToDoItemHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<ToDoItemDto> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var itemToAdd = _mapper.Map<ToDoItem>(request.Item);
            var repo = _unitOfWork.GetRepository<ToDoItem>();

            // Create and dispatch new DomainEvent
            var domainEvent = new ToDoItemCreatedEvent()
            {
                Value = itemToAdd
            };
            await _domainEventDispatcher.RaiseDomainEvent(domainEvent);

            var result = await repo.AddAsync(itemToAdd);
            return _mapper.Map<ToDoItemDto>(result);
        }

        catch (Exception e)
        {
            throw new RepositoryException("Add Todo Item", e);
        }
    }
}