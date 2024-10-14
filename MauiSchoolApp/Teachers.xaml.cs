using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Maui.Controls;

namespace MauiSchoolApp;

public partial class Teachers : ContentPage
{
    public ObservableCollection<string> NamesList { get; set; }
    private readonly string filePath;

    public Teachers()
    {
        InitializeComponent();
        filePath = Path.Combine(FileSystem.AppDataDirectory, "teachers.txt");
        NamesList = new ObservableCollection<string>();
        LoadNamesFromFile();
        namesListView.ItemsSource = NamesList;
    }

    private async void OnAddNameClicked(object sender, EventArgs e)
    {
        string newName = await DisplayPromptAsync("Add Name", "Enter a name:");

        if (!string.IsNullOrWhiteSpace(newName))
        {
            NamesList.Add(newName);
            SaveNamesToFile();
        }
    }

    private void OnDeleteNameClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is string nameToDelete)
        {
            NamesList.Remove(nameToDelete);
            SaveNamesToFile();
        }
    }

    private void SaveNamesToFile()
    {
        File.WriteAllLines(filePath, NamesList);
    }

    private void LoadNamesFromFile()
    {
        if (File.Exists(filePath))
        {
            var namesFromFile = File.ReadAllLines(filePath);
            foreach (var name in namesFromFile)
            {
                NamesList.Add(name);
            }
        }
    }
}