using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class DeleteToDoListCommandTest : BaseObjects
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IDomainEventDispatcher _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();
    private readonly DeleteToDoListHandler _sut;

    public DeleteToDoListCommandTest()
    {
        _sut = new DeleteToDoListHandler(
            _mapper,
            _unitOfWork,
            _domainEventDispatcher);
    }

    [Fact]
    public async Task Handle_ShouldNotThrowExceptionWhenDeletingEntry()
    {
        //Arrange
        _mapper.Map<ToDoList>(Arg.Any<ToDoListDto>()).Returns(ToDoList);

        //Act
        await _sut.Invoking(sut => sut.Handle(new DeleteToDoListCommand(ToDoListDto), CancellationToken.None))

            //Assert
            .Should().NotThrowAsync();
    }
}