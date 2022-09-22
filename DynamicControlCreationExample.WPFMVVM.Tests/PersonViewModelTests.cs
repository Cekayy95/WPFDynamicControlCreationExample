using Xunit;
using DynamicControlCreationExample.WPFMVVM.ViewModel;
using FluentAssertions;

namespace DynamicControlCreationExample.WPFMVVM.Tests;

public class PersonViewModelTests
{
    [Theory]
    [InlineData("1", "test1", "1: test1")]
    [InlineData("100", "test100", "100: test100")]
    [InlineData("69", "test69", "69: test69")]
    public void ToString_ShouldPrintId_FollowedByName(string id, string name, string result)
    {
        //Arrange
         PersonViewModel sut = new(id, name);
         
        //Act
        
        var test = sut.ToString();

        //Assert
        test.Should().Be(result);
    }
    [Fact]
    public void Id_ShouldSetId()
    {
        //Arrange
        PersonViewModel sut = new("1", "name");
        var propChanged = false;
        string settedId = string.Empty;
        sut.PropertyChanged += (sender, args) =>
        {
            if(args.PropertyName == nameof(PersonViewModel.Id))
                propChanged = true;
            if (sender is PersonViewModel pers)
            {
                settedId = pers.Id;
            }
        };
        
        //Act
        
        sut.Id = "42069";
        
        //Assert
        sut.Id.Should().Be("42069","Because changed to awesome number");
        propChanged.Should().Be(true);
        settedId.Should().Be("42069");
    }
    [Fact]
    public void NameProp_ShouldChangeWhenSetterIsCalled()
    {
        //Arrange
        PersonViewModel sut = new("1", "name");
        var propChanged = false;
        string settedName = string.Empty;
        sut.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(PersonViewModel.Name))
                propChanged = true;
            if (sender is PersonViewModel pers)
            {
                settedName = pers.Name;
            }
        };

        //Act
        sut.Name.Should().Be("name");
        sut.Name = "NewNameForNewLife";

        //Assert
        sut.Name.Should().Be("NewNameForNewLife", "Because changed to awesome number");
        propChanged.Should().Be(true);
        settedName.Should().Be("NewNameForNewLife");
    }


}