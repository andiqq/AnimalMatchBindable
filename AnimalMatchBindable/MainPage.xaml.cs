namespace AnimalMatchBindable;

public partial class MainPage
{
    private readonly ViewModel vm = new();
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = vm;
    }
}