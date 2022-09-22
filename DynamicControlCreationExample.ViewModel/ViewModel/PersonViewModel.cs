namespace DynamicControlCreationExample.WPFMVVM.ViewModel;
public class PersonViewModel : BaseViewModel
{
    private string _id;
    private string _name;

    public PersonViewModel(string id, string name)
    {
        _id = id;
        _name = name;
    }

    public string Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public override string ToString()
    {
        return $"{this._id}: {this._name}";
    }
    
}