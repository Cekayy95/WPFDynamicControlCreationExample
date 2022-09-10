using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
namespace DynamicControlCreationExample.WPFMVVM.ViewModel;

public class MainViewModel : BaseViewModel
{
    public System.Timers.Timer TimerForAddPerson;
    public ObservableCollection<PersonViewModel> PersonsFromDataBase { get; } = new ObservableCollection<PersonViewModel>();
    public ObservableCollection<TabViewModel> TabForEveryPerson { get; } = new ObservableCollection<TabViewModel>();
    private TabViewModel selectedTabViewModel;

    public TabViewModel SelectedTabViewModel
    {
        get => selectedTabViewModel;
        set => SetProperty(ref selectedTabViewModel, value);
    }

    public MainViewModel()
    {
        TimerForAddPerson = new System.Timers.Timer(5000);
        TimerForAddPerson.Elapsed += AddPersonEvery5Sec;
        TimerForAddPerson.Start();
        PersonViewModel[] persons =
        {
            new PersonViewModel("1", "Hans"),
            new PersonViewModel("2", "Thomas"),
            new PersonViewModel("3", "Max"),
            new PersonViewModel("4", "Mustermann")
        };
        CreateTabViewModelForEveryPerson(persons);
        
    }
    public void CreateTabViewModelForEveryPerson(params PersonViewModel[] persons)
    {
        foreach (var personViewModel in persons)
        {
            TabForEveryPerson.Add(new TabViewModel(personViewModel,TabForEveryPerson.Count,this));
            PersonsFromDataBase.Add(personViewModel);
        }
        //RaisePropertyChanged(nameof(TabForEveryPerson));
    }

    public void AddPersonEvery5Sec(object? obj, ElapsedEventArgs args)
    {
        if (TabForEveryPerson.Count > 0)
        {
            //TabForEveryPerson.DeleteOnUi();
            RaisePropertyChanged(nameof(TabForEveryPerson));
            //TimerForAddPerson.Stop();
            //TimerForAddPerson.Dispose();
            return;
        }
        TabForEveryPerson.AddOnUI(new TabViewModel(new PersonViewModel($"{TabForEveryPerson.Count + 1}",
            $"Mustermann{TabForEveryPerson.Count + 1}"), 0,this));
        RaisePropertyChanged(nameof(TabForEveryPerson));
    }
}