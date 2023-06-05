using ShellNav.Themes;

namespace ShellNav.Pages.prehled;

public partial class NastaveniPage : ContentPage
{
	public NastaveniPage()
	{
		InitializeComponent();
        setup();
	}

    private void setup()
    {
    }

  
    private void CloseSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync(false);
    }

    private void themeChanged(ResourceDictionary theme, string number)
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(theme);
        }
        string cislo = number.ToString();
        Preferences.Default.Set("theme", cislo);

    }
    private void theme1_Tapped(object sender, EventArgs e)
    {
        themeChanged(new Theme1() ,"1");
    }
    private void theme2_Tapped(object sender, EventArgs e)
    {
        themeChanged(new Theme2(), "2");

    }
    private void theme3_Tapped(object sender, EventArgs e)
    {
        themeChanged(new Theme3(), "3");


    }
    private void theme4custom_Tapped(object sender, EventArgs e)
    {
        themeChanged(new Theme4Custom(), "4");


    }

}