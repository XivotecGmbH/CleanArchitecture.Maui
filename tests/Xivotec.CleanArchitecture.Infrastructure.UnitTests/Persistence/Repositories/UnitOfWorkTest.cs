using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.Persistence;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Repositories;

public class UnitOfWorkTest
{
    private readonly IUnitOfWork _sut;

    private readonly IDataContext _context = Substitute.For<IDataContext>();
    private readonly List<ToDoItem> _testEntities;

    public UnitOfWorkTest()
    {
        IEnumerable<IPersistentRepository> persRepo = new List<IPersistentRepository>();
        IEnumerable<IRuntimeRepository> runRepo = new List<IRuntimeRepository>() { new ItemRunRepo() };

        _sut = new UnitOfWork(_context, persRepo, runRepo);


        _testEntities = new()
        {
            new ToDoItem() { Title = "Sophie" },
            new ToDoItem() { Title = "Tom" }
        };
    }

    [Fact]
    public void GetRepository_ShouldReturnInstance_WhenRepositoryExist()
    {
        //Arrange

        //Act
        var result = _sut.GetRepository<ToDoItem>();
        var result2 = _sut.GetRepository<ToDoItem, Guid>();

        //Assert
        result.Should().NotBeNull();
        result2.Should().NotBeNull();
    }

    [Fact]
    public async Task GetRepository_ShouldReturnTwoItems_WhenRepositoryItemWereAdded()
    {
        // Arrange
        var repo = _sut.GetRepository<ToDoItem>();
        await repo.AddAsync(_testEntities[0]);
        await repo.AddAsync(_testEntities[1]);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        result.Count.Should().Be(2);

        await Task.CompletedTask;
    }
}

class ItemRunRepo : RuntimeRepository<ToDoItem>
{
}