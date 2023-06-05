using System.Text.Json;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.IO.Pipes;
using System.Reflection.PortableExecutable;

namespace ShellNav.Pages.katy;

public partial class aktivityPage : ContentPage
{
    public int SportyReal;
    public int savedIndex;
    int cas;
    int spaleni;

    public aktivityPage()
    {
        InitializeComponent();
        ReadFile();

    }

    public async void ReadFile()
    {
        string mainDir = FileSystem.Current.AppDataDirectory;
        string fileName = "sportyfile.txt";
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
                SportyDataClass.SportList.Add(new SportyDataClass.Sport
                {
                    Name = arrayitem[0],
                    Cas = int.Parse(arrayitem[1]),
                    Spaleni = int.Parse(arrayitem[2]),
                });
            }
        }
        catch (Exception)
        {
        }
    }

    public async void SaveFile()
    {
        foreach (var item in SportyDataClass.SportList)
        {
            cas += item.Cas;
            spaleni += item.Spaleni;
        }
        Preferences.Default.Set("spaleni", spaleni);
        Preferences.Default.Set("cas", cas);
        string mainDir = FileSystem.Current.AppDataDirectory;
        string fileName = "sportyfile.txt";
        string filePath = System.IO.Path.Combine(mainDir, fileName);
        File.Delete(filePath);

        using FileStream outputStream = System.IO.File.OpenWrite(filePath);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(string.Empty);

        foreach (var item in SportyDataClass.SportList)
        {
            string itemString = (item.Name + "," + item.Cas.ToString() + "," + item.Spaleni.ToString() + "." );
            try
            {
                await streamWriter.WriteAsync(itemString);
                // File saved successfully
            }
            catch (Exception)
            {
                // Handle the exception (e.g., show an error message)
            }
        }
        cas = 0;
        spaleni = 0;

    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        addGrid.IsVisible = true;
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = (MenuItem)sender;
        Preferences.Default.Set("cas", cas);
        Preferences.Default.Set("spaleni", spaleni);

        var item = (SportyDataClass.Sport)menuItem.BindingContext; // Replace YourDataType with the actual type of your item
        SportyDataClass.SportList.Remove(item);
        SaveFile();
    }

    private void saveBtn_Clicked(object sender, EventArgs e)
    {
       
        Preferences.Default.Set("cas", cas);
        Preferences.Default.Set("spaleni", spaleni);

        addGrid.IsVisible = false;
        try
        {
            SportyDataClass.SportList.Add(new SportyDataClass.Sport
            {
                Name = nameEntry.Text,
                Cas = int.Parse(casEntry.Text),
                Spaleni = int.Parse(spaleniEntry.Text),
            });
            SaveFile();
            addGrid.IsVisible = false;
            nameEntry.Text = null;
            casEntry.Text = null;
            spaleniEntry.Text = null;
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
        casEntry.Text = null;
        spaleniEntry.Text = null;

    }
    private void Edit_Clicked(object sender, EventArgs e)
    {

        var menuItem = (MenuItem)sender;
        var item = (SportyDataClass.Sport)menuItem.BindingContext; // Replace YourDataType with the actual type of your item
        int index = SportyDataClass.SportList.IndexOf(item);
        SportyDataClass.SportList[index] = item;
        savedIndex = index;
        nameEditEntry.Text = SportyDataClass.SportList[index].Name;
        casEditEntry.Text = SportyDataClass.SportList[index].Cas.ToString();
        spaleniEditEntry.Text = SportyDataClass.SportList[index].Spaleni.ToString();
        editGrid.IsVisible = true;
    }

    private void saveEditBtn_Clicked(object sender, EventArgs e)
    {
        int index = savedIndex;
        SportyDataClass.SportList[index].Name = nameEditEntry.Text;
        SportyDataClass.SportList[index].Cas = int.Parse(casEditEntry.Text);
        SportyDataClass.SportList[index].Spaleni = int.Parse(spaleniEditEntry.Text);
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