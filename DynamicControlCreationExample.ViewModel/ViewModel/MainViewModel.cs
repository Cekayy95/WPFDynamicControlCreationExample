using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicControlCreationExample.WPFMVVM.ViewModel;

public class MainViewModel : BaseViewModel, IDataInterface
{
    
    public ObservableCollection<PersonViewModel> PersonsFromDataBase { get; } = new ();
    public ObservableCollection<TabViewModel> TabForEveryPerson { get; } = new ();

    private bool checkBoxIsChecked;

    public bool CheckBoxIsChecked
    {
        get => checkBoxIsChecked;
        set => SetProperty(ref checkBoxIsChecked, value);
    }
    private bool checkBoxIsChecked2;

    public bool CheckBoxIsChecked2
    {
        get => checkBoxIsChecked2;
        set => SetProperty(ref checkBoxIsChecked2, value);
    }
    private TabViewModel _selectedTabViewModel;
    public TabViewModel SelectedTabViewModel
    {
        get => _selectedTabViewModel;
        set => SetProperty(ref _selectedTabViewModel, value);
    }

    public MainViewModel()
    {
        CreateTabViewModelForEveryPerson(GetPersonViewModelData().GetAwaiter().GetResult());
        _selectedTabViewModel = TabForEveryPerson.First();
    }
    public void CreateTabViewModelForEveryPerson(params IPerson[] persons)
    {
        foreach (var iperson in persons)
        {
            if (iperson is not PersonViewModel personViewModel) continue;
            TabForEveryPerson.Add(new TabViewModel(personViewModel, TabForEveryPerson.Count, this));
            PersonsFromDataBase.Add(personViewModel);
        }
    }

    public int Add2Integers(params int[] numbers)
    {
        return numbers.Sum();
    }

    public double Divide2Numbers(double left, double divisor)
    {
        if (divisor is 0.0)
            throw new DivideByZeroException("Cannot divide by zero");
        return left / divisor;
    }

    public async Task<double> Wait1SecondThenDivide2Numbers(double left, double divisor)
    {
        await Task.Delay(1000);
        if (divisor is 0.0)
            throw new DivideByZeroException("Cannot divide by zero");
        return left/divisor;
    }
    
    public async Task<IPerson[]> GetPersonViewModelData()
    {
        //get Data from DB not suitable for unit testing
        //use NSubstitute for mocking
        await Task.Delay(2000).ConfigureAwait(false);
        PersonViewModel[] persons =
        {
            new ("1", "Hans"),
            new ("2", "Thomas"),
            new ("3", "Max"),
            new ("4", "Mustermann"),
            new ("5", "Test"),
            new ("6", "Test6"),
            new ("7", "Test7"),
            new ("8", "Test8"),
        };
        return persons;
    }
}
public class MainViewModelClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
            new PersonViewModel("1","test"),
            new PersonViewModel("2","test2"),
            new PersonViewModel("3","test3"),
            new PersonViewModel("4","test4")
        };
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
public interface IDataInterface
{
    Task<IPerson[]> GetPersonViewModelData();
}