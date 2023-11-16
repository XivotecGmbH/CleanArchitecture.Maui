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

public class AddToDoItemCommandTest : BaseObjects
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IDomainEventDispatcher _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();

    private readonly AddToDoItemHandler _sut;

    public AddToDoItemCommandTest()
    {
        _sut = new AddToDoItemHandler(_mapper,
            _unitOfWork,
            _domainEventDispatcher);
    }

    [Fact]
    public async Task Handle_ShouldAddNewToDoItem()
    {
        //Arrange
        var doItemDto = ToDoItemsDto[0];
        _mapper.Map<ToDoItem>(Arg.Any<ToDoItemDto>()).Returns(ToDoItems[0]);
        _mapper.Map<ToDoItemDto>(Arg.Any<ToDoItem>()).Returns(doItemDto);

        //Act
        var result = await _sut.Handle(new AddToDoItemCommand(doItemDto), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(doItemDto.Title);
    }
}