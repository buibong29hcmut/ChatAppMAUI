using Client.MaUI.ViewModels;
using System.Collections.ObjectModel;

namespace Client.MaUI.Controls;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UserOnlineControl : ContentView
{
    public ObservableCollection<string> ItemsSource { get; set; }

    public static readonly BindableProperty ItemsSourceProperty =
       BindableProperty
       .Create(
           propertyName: "ItemsSource",
           returnType: typeof(ObservableCollection<string>),
           declaringType: typeof(UserOnlineControl),
           defaultValue: null,
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: ItemsSourcePropertyChanged);
    private static  void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var items = newValue as ObservableCollection<string>;
    }

    public UserOnlineControl()
	{
		InitializeComponent();
        
	}
}