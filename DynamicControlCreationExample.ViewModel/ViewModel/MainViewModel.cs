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
    private System.Timers.Timer timer;
    public ObservableCollection<PersonViewModel> PersonsFromDataBase { get; } = new ObservableCollection<PersonViewModel>();
    public ObservableCollection<TabViewModel> TabForEveryPerson { get; } = new ObservableCollection<TabViewModel>();


    public MainViewModel()
    {
        timer = new System.Timers.Timer(5000);
        timer.Elapsed += AddPersonEvery5Sec;
        timer.Start();
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
            TabForEveryPerson.Add(new TabViewModel(new PersonViewModel(personViewModel.Id, personViewModel.Name)));
        }
        RaisePropertyChanged(nameof(TabForEveryPerson));
    }

    public void AddPersonEvery5Sec(object? obj, ElapsedEventArgs args)
    {
        if (TabForEveryPerson.Count == 10)
        {
            timer.Stop();
            timer.Dispose();
            return;
        }
        TabForEveryPerson.AddOnUI(new TabViewModel(new PersonViewModel($"{TabForEveryPerson.Count + 1}",
            $"Mustermann{TabForEveryPerson.Count + 1}")));
        RaisePropertyChanged(nameof(TabForEveryPerson));
    }
}