using System.Text.Json;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.IO.Pipes;
using System.Reflection.PortableExecutable;

namespace ShellNav.Pages.katy;

public partial class jidelnaPage : ContentPage
{
    int KalorieReal;
    int savedIndex;
    int kalorie;
    int bilkoviny;
    int vlaknina;
    int tuky;
    int sacharidy;
    public jidelnaPage()
	{
		InitializeComponent();
        ReadFile();

    }

    public async void ReadFile()
    {
        string mainDir = FileSystem.Current.AppDataDirectory;
        string fileName = "jidlafile.txt";
        string filePath = System.IO.Path.Combine(mainDir, fileName);

        try
        {
            using Stream fileStream = System.IO.File.OpenRead(filePath);
            using StreamReader reader = new StreamReader(fileStream);
            string fileContent = await reader.ReadToEndAsync();
            string[] arrayclass = fileContent.Split('.');
            arrayclass = arrayclass.SkipLast(1).ToArray();
            foreach (var item in arrayclass)
            {
                string[] arrayitem = item.Split(',');
                JidloDataClass.FoodList.Add(new JidloDataClass.Jidlo
                {
                    Name = arrayitem[0],
                    Kalorie = int.Parse(arrayitem[1]),
                    Bilkoviny = int.Parse(arrayitem[2]),
                    Vlaknina = int.Parse(arrayitem[3]),
                    Tuky = int.Parse(arrayitem[4]),
                    Sacharidy = int.Parse(arrayitem[5]),
                });
            }
        }
        catch (Exception)
        {
        }
    }

    public async void SaveFile()
    {
        foreach (var item in JidloDataClass.FoodList)
        {
            kalorie += item.Kalorie;
            bilkoviny += item.Bilkoviny;
            vlaknina += item.Vlaknina;
            tuky    += item.Tuky;
            sacharidy += item.Sacharidy;
        }
        Preferences.Default.Set("kalorie", kalorie);
        Preferences.Default.Set("bilkoviny", bilkoviny);
        Preferences.Default.Set("vlaknina", vlaknina);
        Preferences.Default.Set("tuky", tuky);
        Preferences.Default.Set("sacharidy", sacharidy);


        string mainDir = FileSystem.Current.AppDataDirectory;
        string fileName = "jidlafile.txt";
        string filePath = System.IO.Path.Combine(mainDir, fileName);
        File.Delete(filePath);

        using FileStream outputStream = System.IO.File.OpenWrite(filePath);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(string.Empty);

        foreach (var item in JidloDataClass.FoodList)
        {
            string itemString = (item.Name + "," + item.Kalorie.ToString() + "," + item.Bilkoviny.ToString() + "," + item.Vlaknina.ToString() + "," + item.Tuky.ToString() + "," + item.Sacharidy.ToString() + ".");
            try
            {
                await streamWriter.WriteAsync(itemString);
                // File saved successfully
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., show an error message)
            }
        }
        kalorie = 0;
        bilkoviny = 0;
        vlaknina = 0;
        tuky = 0;
        sacharidy = 0;
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        addGrid.IsVisible = true;
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = (MenuItem)sender;
        var item = (JidloDataClass.Jidlo)menuItem.BindingContext; // Replace YourDataType with the actual type of your item
        JidloDataClass.FoodList.Remove(item);
        SaveFile();
    }

    private void saveBtn_Clicked(object sender, EventArgs e)
    {
        addGrid.IsVisible =false;
        try
        {
            JidloDataClass.FoodList.Add(new JidloDataClass.Jidlo
            {
                Name = nameEntry.Text,
                Kalorie = int.Parse(kalorieEntry.Text),
                Bilkoviny = int.Parse(bilkovinyEntry.Text),
                Vlaknina = int.Parse(vlakninaEntry.Text),
                Tuky = int.Parse(tukyEntry.Text),
                Sacharidy = int.Parse(sacharidyEntry.Text)

            });
            SaveFile();
            addGrid.IsVisible = false;
            nameEntry.Text = null;
            kalorieEntry.Text = null;
            bilkovinyEntry.Text = null;
            vlakninaEntry.Text = null;
            tukyEntry.Text = null;
            sacharidyEntry.Text = null;
        }
        catch (Exception)
        {

            DisplayAlert("chyba!", "Bud Franta uzivatel", "ok");
        }
        
        
    }

    private void cancelBtn_Clicked(object sender, EventArgs e)
    {
        addGrid.IsVisible = false;
        nameEntry.Text = null;
        kalorieEntry.Text = null;
        bilkovinyEntry.Text = null;
        vlakninaEntry.Text = null;
        tukyEntry.Text = null;
        sacharidyEntry.Text = null;

    }
    private void Edit_Clicked(object sender, EventArgs e)
    {

        var menuItem = (MenuItem)sender;
        var item = (JidloDataClass.Jidlo)menuItem.BindingContext; // Replace YourDataType with the actual type of your item
        int index = JidloDataClass.FoodList.IndexOf(item);
        JidloDataClass.FoodList[index] = item;
        savedIndex = index;
        nameEditEntry.Text = JidloDataClass.FoodList[index].Name;
        kalorieEditEntry.Text = JidloDataClass.FoodList[index].Kalorie.ToString();
        bilkovinyEditEntry.Text = JidloDataClass.FoodList[index].Bilkoviny.ToString();
        vlakninaEditEntry.Text = JidloDataClass.FoodList[index].Vlaknina.ToString();
        tukyEditEntry.Text = JidloDataClass.FoodList[index].Tuky.ToString();
        sacharidyEditEntry.Text = JidloDataClass.FoodList[index].Sacharidy.ToString();
        editGrid.IsVisible = true;
    }

    private void saveEditBtn_Clicked(object sender, EventArgs e)
    {
        int index = savedIndex;
        JidloDataClass.FoodList[index].Name = nameEditEntry.Text;
        JidloDataClass.FoodList[index].Kalorie = int.Parse(kalorieEditEntry.Text);
        JidloDataClass.FoodList[index].Bilkoviny  = int.Parse(bilkovinyEditEntry.Text);
        JidloDataClass.FoodList[index].Vlaknina = int.Parse(vlakninaEditEntry.Text);
        JidloDataClass.FoodList[index].Tuky = int.Parse(tukyEditEntry.Text);
        JidloDataClass.FoodList[index].Sacharidy = int.Parse(sacharidyEditEntry.Text);
        editGrid.IsVisible = false;
        SaveFile();

    }

    private void cancelEditBtn_Clicked(object sender, EventArgs e)
    {
        editGrid.IsVisible = false;
    }

    //public async void WriteTextToFile(string text)
    //{
    //    // Write the file content to the app data directory  
    //    string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "jidlatxt.txt");
    //    using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
    //    using StreamWriter streamWriter = new StreamWriter(outputStream);
    //    await streamWriter.WriteAsync(text);
    //}

}