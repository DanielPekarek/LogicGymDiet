using Syncfusion.Maui.Charts;
using ShellNav.Themes;


namespace ShellNav.Pages.prehled;


public partial class prehledPage : ContentPage
{

    public prehledPage()
    {
        
        InitializeComponent();
        this.BindingContext = new ViewModel();
        int cas = Preferences.Get("cas", 0);
        int spaleni = Preferences.Get("spaleni", 0);
        int kalorie = Preferences.Get("kalorie", 0);
        int bilkoviny = Preferences.Get("bilkoviny", 0);
        int vlaknina = Preferences.Get("vlaknina", 0);
        int tuky = Preferences.Get("tuky", 0);
        int sacharidy = Preferences.Get("sacharidy", 0);
        string cislo = Preferences.Default.Get("theme", "1");
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




    private void OpenSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NastaveniPage(), false);
    }

}
