using AiNavigator.Mobile.Converters;
using AiNavigator.Mobile.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AiNavigator.Mobile;

public partial class HistoryPage : ContentPage
{
    public List<ApiGroupResult> HistoryItems { get; set; }

    public HistoryPage(List<ApiGroupResult> apiResults)
    {
        InitializeComponent();
        HistoryItems = apiResults;
        BindingContext = this;

        HistoryCollection.ItemsSource = HistoryItems;
    }
}
