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

public class UpdateToDoListCommandTest : BaseObjects
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly UpdateToDoListHandler _sut;

    public UpdateToDoListCommandTest()
    {
        _sut = new UpdateToDoListHandler(_mapper,
            _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldNotThrowExceptionWhenUpdatingEntry()
    {
        //Arrange
        _mapper.Map<ToDoList>(Arg.Any<ToDoListDto>()).Returns(ToDoList);

        //Act
        await _sut.Invoking(sut => sut.Handle(new UpdateToDoListCommand(ToDoListDto), CancellationToken.None))

            //Assert
            .Should().NotThrowAsync();
    }
}