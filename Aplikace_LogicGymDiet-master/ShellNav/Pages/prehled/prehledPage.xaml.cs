using Syncfusion.Maui.Charts;
using ShellNav.Themes;


namespace ShellNav.Pages.prehled;


public partial class prehledPage : ContentPage
{
    
    ViewModel myViewModel = new ViewModel();
    public prehledPage()
    {

        InitializeComponent();

        loadcol();
        int cas = Preferences.Get("cas", 0);
        int kalorie = Preferences.Get("kalorie", 0);
        string cislo = Preferences.Default.Get("theme", "1");
        this.BindingContext = myViewModel;
        int theme = int.Parse(cislo);
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            switch (theme)
            {
                case 1:
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new Theme1());

                    break;
                case 2:
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new Theme2());

                    break;
                case 3:

                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new Theme3());

                    break;
                case 4:
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new Theme4Custom());

                    break;
                default:
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new Theme1());
                    break;
            }
        }

    }

    private void loadcol()
    {
        myViewModel.Data[0] = new Person()
        {
            Makroziviny = "Bílkoviny",
            Height = Preferences.Get("bilkoviny", 0)
        };
        myViewModel.Data[1] = new Person()
        {
            Makroziviny = "Vláknina",
            Height = Preferences.Get("vlaknina", 0)
        };
        myViewModel.Data[2] = new Person()
        {
            Makroziviny = "Tuky",
            Height = Preferences.Get("tuky", 0)
        };
        myViewModel.Data[3] = new Person()
        {
            Makroziviny = "Sacharidy",
            Height = Preferences.Get("sacharidy", 0)
        };
        columns.ItemsSource = myViewModel.Data;
        int  kalorie = Preferences.Get("kalorie", 0);
        int spaleni = Preferences.Get("spaleni", 0);
        RangePointer.Value = kalorie - spaleni;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        loadcol();
    }
    


    private void OpenSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NastaveniPage(), false);
    }

}
