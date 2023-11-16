using FluentAssertions;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Repositories;

public class RepositoryTest
{
    private readonly TestRepository _sut = new();

    private readonly List<TestEntity> _testEntities = new()
    {
        new TestEntity() { Id = Guid.NewGuid(), Name = "Sophie" },
        new TestEntity() { Id = Guid.NewGuid(), Name = "Tom" }
    };

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoToDoListsExist()
    {
        //Arrange

        //Act
        var result = await _sut.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddAsync_ShouldAddSingleToDoList_WhenNoToDoListsExist()
    {
        //Arrange
        await _sut.AddAsync(_testEntities[0]);

        //Act
        var allResults = await _sut.GetAllAsync();

        //Assert
        allResults.Should().ContainSingle();
    }

    [Fact]
    public async Task AddAsync_ShouldAddMultipleToDoLists_WhenNoToDoListsExist()
    {
        //Arrange
        await _sut.AddAsync(_testEntities[0]);
        await _sut.AddAsync(_testEntities[1]);

        //Act
        var allResults = await _sut.GetAllAsync();

        //Assert
        allResults.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldFindCorrectList()
    {
        //Arrange
        await _sut.AddAsync(_testEntities[0]);

        //Act
        var result = await _sut.GetByIdAsync(_testEntities[0].Id);

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(_testEntities[0]);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowExceptionForMissingList()
    {
        //Arrange
        await _sut.AddAsync(_testEntities[0]);

        //Act

        //Assert
        await _sut.Invoking(y => y.GetByIdAsync(_testEntities[1].Id))
            .Should().ThrowAsync<ItemNotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCorrectList()
    {
        //Arrange
        await _sut.AddAsync(_testEntities[0]);
        await _sut.AddAsync(_testEntities[1]);

        //Act
        await _sut.DeleteAsync(_testEntities[0]);
        var result = await _sut.GetAllAsync();

        //Assert
        result.Should().Contain(_testEntities[1]);
        result.Should().NotContain(_testEntities[0]);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCorrectList()
    {
        //Arrange
        await _sut.AddAsync(_testEntities[0]);
        await _sut.AddAsync(_testEntities[1]);

        var newName = "Updated List 1";
        _testEntities[0].Name = newName;

        //Act
        await _sut.UpdateAsync(_testEntities[0]);
        var result = await _sut.GetByIdAsync(_testEntities[0].Id);

        //Assert
        result.Name.Should().Be(newName);
    }
}
