using System.Collections.ObjectModel;
using WaterBillingApp.Model;
using WaterBillingApp.Services;

namespace WaterBillingApp.Pages;

public partial class ConsumersPage : ContentPage
{
    public ObservableCollection<ConsumerListDto> Consumers { get; set; } = new ObservableCollection<ConsumerListDto>();
    private readonly ConsumerService _consumerService = new();

    public ConsumersPage()
    {
        InitializeComponent();
        ConsumerLoader.IsVisible = true;
        ConsumerLoader.IsRunning = true;
        BindingContext = this;
        LoadConsumers();
       
    }

    private async void LoadConsumers()
    {
        try
        { 
            var result = await _consumerService.GetConsumersAsync();


            Consumers.Clear();
            foreach (var consumer in result)
            {
                Consumers.Add(consumer);
            }
            ConsumersCollectionView.ItemsSource = Consumers;
            ConsumerLoader.IsVisible = false;
            ConsumerLoader.IsRunning = false;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
