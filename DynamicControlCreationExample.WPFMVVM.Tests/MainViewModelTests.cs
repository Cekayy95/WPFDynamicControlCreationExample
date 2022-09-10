using System;
using System.Runtime.InteropServices.ComTypes;
using Xunit;
using DynamicControlCreationExample.WPFMVVM;
using DynamicControlCreationExample.WPFMVVM.ViewModel;
using FluentAssertions;

namespace DynamicControlCreationExample.WPFMVVM.Tests;

public class MainViewModelTests
{
    public static  PersonViewModel person = new PersonViewModel("1", "test");
    
    [Theory]
    [InlineData(typeof(PersonViewModel))]
    public void AddPersonsToTabForEveryPerson_ShouldChangeTabCount(Type pers)
    {
        //Arrange
        var sut = new MainViewModel();
        var counter = sut.TabForEveryPerson.Count;
        var person1 = Activator.CreateInstance(pers,"1","Test") as PersonViewModel;
        var person2 = Activator.CreateInstance(pers,"2","Test2") as PersonViewModel;
        var person3 = Activator.CreateInstance(pers,"3","Test3") as PersonViewModel;
        var person4 = Activator.CreateInstance(pers,"4","Test4") as PersonViewModel;
        var person5 = Activator.CreateInstance(pers,"4","Test5") as PersonViewModel;
        PersonViewModel[] persons =
        {
            person1, person2, person3, person4, person5
        };
        sut.TimerForAddPerson.Stop();
        sut.TabForEveryPerson.Clear();
        sut.PersonsFromDataBase.Clear();
        //Act

        sut.CreateTabViewModelForEveryPerson(persons);
        var counter2 = sut.TabForEveryPerson.Count;
        //Assert
        counter.Should().BeLessThan(counter2);
        counter2.Should().Be(5);
    }
}