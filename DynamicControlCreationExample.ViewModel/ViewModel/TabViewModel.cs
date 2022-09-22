using System.Collections.ObjectModel;

namespace DynamicControlCreationExample.WPFMVVM.ViewModel;

public class TabViewModel : BaseViewModel
{
    public ObservableCollection<PersonViewModel> Persons { get; init; } = new ();
    public PersonViewModel Person { get; init; }
    public int Index { get; set; }

    public TabViewModel(PersonViewModel person, int index, MainViewModel mainViewModel)
    {
        Persons!.Add(person);
        Persons.Add(new PersonViewModel($"{mainViewModel.TabForEveryPerson.Count + 2}",
            $"StaticPerson {mainViewModel.TabForEveryPerson.Count + 2}"));
        Person = person;
        Index = index;
    }

}