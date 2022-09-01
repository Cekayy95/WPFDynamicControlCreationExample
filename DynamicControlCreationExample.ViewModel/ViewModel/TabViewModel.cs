namespace DynamicControlCreationExample.WPFMVVM.ViewModel;

public class TabViewModel : BaseViewModel
{
    public PersonViewModel Person { get; init; }

    public TabViewModel(PersonViewModel person)
    {
        Person = person;
    }
}