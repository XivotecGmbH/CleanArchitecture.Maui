using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Queries;

public class GetToDoListListQueryTest : BaseObjects
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly GetToDoListListHandler _sut;

    public GetToDoListListQueryTest()
    {
        _sut = new GetToDoListListHandler(_mapper, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoToDoListsExist()
    {
        //Arrange
        var repo = _unitOfWork.GetRepository<ToDoList>();
        repo.GetAllAsync().Returns(new List<ToDoList>());

        //Act
        var result = await _sut.Handle(new GetToDoListListQuery(), CancellationToken.None);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectListOfLists_WhenMultipleToDoListsExist()
    {
        //Arrange
        var listId = Guid.NewGuid();

        var toDoItems = new List<ToDoItem>()
        {
            new ToDoItem()
            {
                Title = "Title 1",
                Note = "Note 1",
                Reminder = DateTime.MinValue,
                Done = false,
                ListId = listId,
            },
            new ToDoItem()
            {
                Title = "Title 2",
                Note = "Note 2",
                Reminder = DateTime.MaxValue,
                Done = true,
                ListId = listId,
            }
        };

        var listOfToDoLists = new List<ToDoList>
        {
            new() { Title = "List 1", ToDoItems = toDoItems },
            new() { Title = "List 2", ToDoItems = toDoItems }
        };

        var repo = _unitOfWork.GetRepository<ToDoList>();
        repo.GetAllAsync().Returns(listOfToDoLists);
        _mapper.Map<ToDoListDto>(Arg.Any<ToDoList>()).Returns(ToDoListDto);

        //Act
        var result = await _sut.Handle(new GetToDoListListQuery(), CancellationToken.None);

        //Assert
        result.Count.Should().Be(listOfToDoLists.Count);
        result[0].Should().NotBeNull();
        result[0].Title.Should().NotBeNull();
        result[0].Title.Should().BeEquivalentTo(listOfToDoLists[0].Title);
        result[0].ToDoItems.Count.Should().Be(2);
        result[0].ToDoItems[0].Title.Should().BeEquivalentTo(ToDoItems[0].Title);
        result[0].ToDoItems[1].Title.Should().BeEquivalentTo(ToDoItems[1].Title);
        result[0].ToDoItems[1].ListId.Should().Be(ToDoItems[1].ListId);
    }
}