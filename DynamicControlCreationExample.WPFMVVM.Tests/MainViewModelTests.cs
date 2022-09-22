using System;
using System.Collections.Generic;
using Xunit;
using DynamicControlCreationExample.WPFMVVM.ViewModel;
using FluentAssertions;
using System.Threading.Tasks;
using NSubstitute;

namespace DynamicControlCreationExample.WPFMVVM.Tests;

public class MainViewModelTests
{
    private readonly MainViewModel _sut = new();
    [Theory]
    [ClassData(typeof(MainViewModelClassData))]
    public void AddPersonsToTabForEveryPerson_ShouldChangeTabCount(params PersonViewModel[] person)
    {
        //Arrange
        
        //var counter = _sut.TabForEveryPerson.Count;
        //var person1 = Activator.CreateInstance(pers,"1","Test") as PersonViewModel;
        //var person2 = Activator.CreateInstance(pers,"2","Test2") as PersonViewModel;
        //var person3 = Activator.CreateInstance(pers,"3","Test3") as PersonViewModel;
        //var person4 = Activator.CreateInstance(pers,"4","Test4") as PersonViewModel;
        //var person5 = Activator.CreateInstance(pers,"4","Test5") as PersonViewModel;
        //PersonViewModel[] persons =
        //{
        //    person1!, person2!, person3!, person4!, person5!
        //};
        List<PersonViewModel> persons = new(person);
        _sut.TimerForAddPerson.Stop();
        _sut.PersonsFromDataBase.Clear();
        _sut.TabForEveryPerson.Clear();

        //Act

        _sut.TabForEveryPerson.Count.Should().Be(0);
        _sut.PersonsFromDataBase.Count.Should().Be(0);
        _sut.CreateTabViewModelForEveryPerson(persons.ToArray());

        //Assert
        _sut.TabForEveryPerson.Count.Should().Be(4);
        _sut.PersonsFromDataBase.Count.Should().Be(4);

    }
    [Fact]
    public async Task GetPersonData_ShouldReturnPersonViewModelArray()
    {
        //Arrange
        var sut = new MainViewModel();
        var substitute = Substitute.For<IDataInterface>();
        substitute.GetPersonViewModelData().Returns(new[]
        {
            new PersonViewModel("1", "UnitTest1"),
            new PersonViewModel("2", "UnitTest2"),

        });
        //Act
        var personsArray = await substitute.GetPersonViewModelData();
        //Assert
        Func<PersonViewModel, PersonViewModel, bool >checkEquality = (left, right) => left.Id == right.Id;
        substitute.Received(1);
        personsArray.Length.Should().Be(2);
        personsArray.Should().NotBeEmpty();
        personsArray.Should().OnlyHaveUniqueItems(x=> x.Id);
        personsArray.Should().OnlyHaveUniqueItems(x=> x.Name);
        
    }
    [Theory]
    [InlineData(10, 4, 6)]
    [InlineData(10, 5, 5)]
    [InlineData(1100, 100, 1000)]
    [InlineData(1, -4, 5)]
    public void Add2GenericNumbers_ShouldAdd2GenericNumbers(int result, params int[] numbers)
    {
        //Arrange
        var sut = new MainViewModel();
        
        //Act
        var calcResult =  sut.Add2Integers(numbers);
        //Assert
        calcResult.Should().Be(result);
    }
    [Theory]
    [InlineData(10.0, 100.0, 10.0)]
    [InlineData(200.0/13.5, 200.0, 13.5)]
    [InlineData(200.0/50, 200.0, 50.0)]
    [InlineData(2.5, 5.0, 2.0)] 
    [InlineData(-1.0, -4.0, 4.0)]
    public async Task Wait1SecondThenDivide2NumbersAsync_ShouldWait1SecondThenDivide2NumbersAsync(double result, params double[] numbers)
    {
        //Arrange
        var sut = new MainViewModel();

        //Act
        var calcResult = await sut.Wait1SecondThenDivide2Numbers(numbers[0], numbers[1]);
        //Assert
        calcResult.Should().Be(result);
        
    }
    [Theory]
    [InlineData(100.0, 0.0)]
    [InlineData(50.0, 0.0)]
    [InlineData(5.0, 0.0)]
    [InlineData(-4.0, 0.0)]
    public async Task Wait1SecondThenDivide2NumbersAsync_ShouldWait1SecondThenThrowDivideByZeroException(params double[] numbers)
    {
        //Arrange
        var sut = new MainViewModel();
        //Act
        Func<Task> f = async () => { await sut.Wait1SecondThenDivide2Numbers(numbers[0],numbers[1]); };
        //Assert
        await f.Should().ThrowAsync<DivideByZeroException>().WithMessage("Cannot divide by zero");

    }
    [Theory]
    [InlineData(10.0, 100.0, 10.0)]
    [InlineData(10.0, 50.0, 5.0)]
    [InlineData(2.0, 50.0, 25.0)]
    [InlineData(2.5, 5.0, 2.0)]
    [InlineData(-1.0, -4.0, 4.0)]
    public void Divide2Numbers_ShouldDivide2Numbers(double result, params double[] numbers)
    {
        //Arrange
        var sut = new MainViewModel();

        //Act
        var calcResult = sut.Divide2Numbers(numbers[0], numbers[1]);
        //Assert
        calcResult.Should().Be(result);
    }
    [Theory]
    [InlineData(100.0, 0.0)]
    [InlineData(50.0, 0.0)]
    [InlineData(5.0, 0.0)]
    [InlineData(-4.0, 0.0)]
    public void Divide2Numbers_ShouldThrow_DivideByZeroException(params double[] numbers)
    {
        //Arrange
        var sut = new MainViewModel();

        //Act
        Action act = () => sut.Divide2Numbers(numbers[0], numbers[1]);
        //Assert
        
        act.Should().Throw<DivideByZeroException>()
            .WithMessage("Cannot divide by zero");
    }
}