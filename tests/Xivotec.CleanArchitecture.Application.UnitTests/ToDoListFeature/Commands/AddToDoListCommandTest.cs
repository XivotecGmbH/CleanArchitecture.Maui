using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class AddToDoListCommandTest : BaseObjects
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly AddToDoListHandler _sut;

    public AddToDoListCommandTest()
    {
        _sut = new AddToDoListHandler(_mapper, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldAddNewToDoList()
    {
        //Arrange
        _mapper.Map<ToDoList>(Arg.Any<ToDoListDto>()).Returns(ToDoList);
        _mapper.Map<ToDoListDto>(Arg.Any<ToDoList>()).Returns(ToDoListDto);

        //Act
        var result = await _sut.Handle(new AddToDoListCommand(ToDoListDto), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(ToDoListDto.Title);
        result.ToDoItems.Should().BeEquivalentTo(ToDoItemsDto);
    }
}