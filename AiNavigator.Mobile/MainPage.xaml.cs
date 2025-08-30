using System.Windows.Input;
using AiNavigator.Mobile.Models;
using AiNavigator.Mobile.Services;
using Microsoft.Maui.Controls;

namespace AiNavigator.Mobile;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    public ICommand OpenLinkCommand { get; }

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        OpenLinkCommand = new Command<string>(async (url) => await OpenLink(url));
        BindingContext = this;
    }

    private async void OnSendPromptClicked(object sender, EventArgs e)
    {
        try
        {
            if (CategoryPicker.SelectedItem == null)
            {
                await DisplayAlert("Błąd", "Wybierz kategorię z listy!", "OK");
                return;
            }

            var selectedCategory = (Category)CategoryPicker.SelectedItem;

            SummaryLabel.Text = string.Empty;
            ModelsCollection.ItemsSource = null;

            var result = await _apiService.GetModelsAsync(selectedCategory);

            if (result != null)
            {
                SummaryLabel.Text = result.Summary;
                ModelsCollection.ItemsSource = result.Models;
            }
            else
            {
                await DisplayAlert("Informacja", $"Nie znaleziono modeli dla kategorii '{selectedCategory}'.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Błąd", $"Nie udało się pobrać danych: {ex.Message}", "OK");
            Console.WriteLine($"MainPage Error: {ex}");
        }
    }

    private async Task OpenLink(string url)
    {
        if (!string.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            await Launcher.OpenAsync(uriResult);
        }
        else
        {
            await DisplayAlert("Błąd", "Nieprawidłowy adres URL.", "OK");
        }
    }
}