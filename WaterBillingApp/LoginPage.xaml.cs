using System.Collections.ObjectModel;
using System.Text.Json;
using WaterBillingApp.Model;
using WaterBillingApp.Services;

namespace WaterBillingApp;

public partial class LoginPage : ContentPage
{
    private readonly LoginService _loginService;
    public ObservableCollection<string> LoginTypes { get; set; } = new ObservableCollection<string>();
    public ObservableCollection<string> LoginDivisions { get; set; } = new ObservableCollection<string>();
    public LoginPage()
	{
		InitializeComponent();
        _loginService = new LoginService();
        BindingContext = this;
        _ = FetchLoginTypesAsync();
        DivisionPicker.IsVisible = false;
    }
    private async Task FetchLoginTypesAsync()
    {
        var roles = await _loginService.FetchLoginTypesAsync();

        LoginTypes.Clear();
        foreach (var role in roles)
            LoginTypes.Add(role);
    }
    private async Task FetchLoginDivisionsAsync()
    {
        var roles = await _loginService.FetchLoginDivisionsAsync();

        LoginDivisions.Clear();
        foreach (var role in roles)
            LoginDivisions.Add(role);
    }
    private async void LoginAsPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedRole = LoginAsPicker.SelectedItem?.ToString();

        if (selectedRole == "Operator")
        {
            DivisionPicker.IsVisible = true;
            await FetchLoginDivisionsAsync();
        }
        else
        {
            DivisionPicker.SelectedItem = null;
            DivisionPicker.IsVisible = false;
        }
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        
        LoginLoader.IsVisible = true;
        LoginLoader.IsRunning = true;

        try
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string selectedLoginType = LoginAsPicker.SelectedItem?.ToString();
            string selectedDivision = DivisionPicker.SelectedItem?.ToString();

            var userDetails = await _loginService.LoginAsync(username, password, selectedLoginType, selectedDivision);

            if (userDetails != null)
            {
                string userRole = userDetails.RoleName;
                await Navigation.PushAsync(new MainLayoutPage(userRole));
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid credentials or role.", "OK");
            }
        }
        finally
        {
            // Hide loader
            LoginLoader.IsRunning = false;
            LoginLoader.IsVisible = false;
        }
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        // Handle forgot password action here (e.g., navigate to a password reset page)
        await DisplayAlert("Forgot Password", "Password reset functionality not implemented.", "OK");
    }
    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        // Navigate to a registration page or perform the registration action
        await DisplayAlert("Register", "Navigate to the registration page", "OK");
    }
}