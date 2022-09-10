﻿namespace DynamicControlCreationExample.WPFMVVM.ViewModel;
public class PersonViewModel : BaseViewModel
{
    public PersonViewModel(string id, string name)
    {
        Id = id;
        Name = name;
        //RaisePropertyChanged(nameof(Id));
        //RaisePropertyChanged(nameof(Name));
    }

    public string Id { get; set; }

    public string Name { get; set; }

    
}