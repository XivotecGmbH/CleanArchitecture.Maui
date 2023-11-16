using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Queries;

public class GetToDoListByIdQueryTest : BaseObjects
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly GetToDoListByIdHandler _sut;

    public GetToDoListByIdQueryTest()
    {
        _sut = new GetToDoListByIdHandler(_mapper, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldFindCorrectListById_WhenListExists()
    {
        //Arrange
        var repo = _unitOfWork.GetRepository<ToDoList>();
        repo.GetByIdAsync(Arg.Any<Guid>()).Returns(ToDoList);

        _mapper.Map<ToDoListDto>(Arg.Any<ToDoList>()).Returns(ToDoListDto);

        //Act
        var result = await _sut.Handle(new GetToDoListByIdQuery(ToDoList.Id), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(ToDoList.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowItemNotFoundExceptionWhenListDoesNotExist()
    {
        //Arrange
        var repo = _unitOfWork.GetRepository<ToDoList>();
        repo.GetByIdAsync(Arg.Any<Guid>()).Throws(new RepositoryException("GetById", new Exception()));

        //Act
        await _sut.Invoking(y => y.Handle(new GetToDoListByIdQuery(Guid.NewGuid()), CancellationToken.None))

            //Assert
            .Should().ThrowAsync<RepositoryException>();
    }
}