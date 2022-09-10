using System.Collections.ObjectModel;

namespace DynamicControlCreationExample.WPFMVVM.ViewModel;

public class TabViewModel : BaseViewModel
{
    private readonly MainViewModel _mainViewModel;
    public ObservableCollection<PersonViewModel> Persons { get; init; } = new ObservableCollection<PersonViewModel>();
    public PersonViewModel Person { get; init; }
    public int Index { get; set; }

    public TabViewModel(PersonViewModel person, int index, MainViewModel mainViewModel)
    {
        Persons!.Add(person);
        Persons.Add(new PersonViewModel($"{mainViewModel.TabForEveryPerson.Count + 2}",
            $"StaticPerson {mainViewModel.TabForEveryPerson.Count + 2}"));
        Person = person;
        _mainViewModel = mainViewModel;
        Index = index;
    }

}