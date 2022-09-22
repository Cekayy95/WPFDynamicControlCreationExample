namespace DynamicControlCreationExample.WPFMVVM.ViewModel;
public class PersonViewModel : BaseViewModel, IPerson
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

public interface IPerson
{
    string Id { get; set; }
    string Name { get; set; }
    abstract string ToString();

}