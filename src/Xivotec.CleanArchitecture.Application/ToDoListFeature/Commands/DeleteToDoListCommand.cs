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
/// Command which deletes the requested <see cref="ToDoListDto"/> from the repository.
/// </summary>
/// <param name="Item">The <see cref="ToDoListDto"/> to be deleted.</param>
public record DeleteToDoListCommand(ToDoListDto Item) : IRequest;

public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public DeleteToDoListHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var itemToDelete = _mapper.Map<ToDoList>(request.Item);
            var repo = _unitOfWork.GetRepository<ToDoList>();

            var domainEvent = new ToDoListDeletedEvent()
            {
                Value = itemToDelete
            };
            await _domainEventDispatcher.RaiseDomainEvent(domainEvent);

            // Delete all items before list
            await repo.DeleteAsync(itemToDelete);
        }
        catch (Exception e)
        {
            throw new RepositoryException(request.GetType().Name, e);
        }
    }
}