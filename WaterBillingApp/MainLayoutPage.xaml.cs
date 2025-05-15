using Microsoft.Maui.Controls;
using System.Data;
using System.Diagnostics;
using WaterBillingApp.Model;
using WaterBillingApp.Pages;

namespace WaterBillingApp;

public partial class MainLayoutPage : ContentPage
{
    private bool isSidebarVisible = false;
    public MainLayoutPage(string userRole)
    {
        InitializeComponent();
        Sidebar.IsVisible = isSidebarVisible;
        
        var menuItems = GetMenuForRole(userRole);
        SidebarMenu.ItemsSource = menuItems;
        //RoleLabel.Text = $"Logged in as: {userRole}";
        if (menuItems.Any())
        {
            var defaultMenuItem = menuItems[0];
            defaultMenuItem.NavigateCommand.Execute(null);

            // Optional: visually select it
            SidebarMenu.SelectedItem = defaultMenuItem;
        }
    }

    private void OnToggleSidebarClicked(object sender, EventArgs e)
    {
        isSidebarVisible = !isSidebarVisible;

        Sidebar.IsVisible = isSidebarVisible;

         
    }

    private List<MenuItemModel> GetMenuForRole(string role)
    {
        var items = new List<MenuItemModel>();

       
        if (role == "Admin")
        {
            items.Add(new MenuItemModel("Dashboard", new Command(() => Navigate(new AdminDashboardPage()))));
            items.Add(new MenuItemModel("Users", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Consumer", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Connections", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Billing", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Approvals", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Grievances", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Reports", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Settings", new Command(() => Navigate(new UserManagementPage()))));
        }
        else if (role == "Junior Engineer")
        {
            items.Add(new MenuItemModel("Tasks", new Command(() => Navigate(new TaskListPage()))));
            items.Add(new MenuItemModel("Approvals", new Command(() => Navigate(new UserManagementPage()))));
        }
        else if (role == "Assistant Engineer")
        {
            items.Add(new MenuItemModel("Tasks", new Command(() => Navigate(new TaskListPage()))));
            items.Add(new MenuItemModel("Approvals", new Command(() => Navigate(new UserManagementPage()))));
        }
        else if (role == "Executive Engineer")
        {
            items.Add(new MenuItemModel("Tasks", new Command(() => Navigate(new TaskListPage()))));
            items.Add(new MenuItemModel("Approvals", new Command(() => Navigate(new UserManagementPage()))));
        }
        else if (role == "Operator")
        {
            items.Add(new MenuItemModel("Dashboard", new Command(() => Navigate(new OperatorDashboardPage()))));
            items.Add(new MenuItemModel("Consumer", new Command(() => Navigate(new ConsumersPage()))));
            items.Add(new MenuItemModel("Billing", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Reports", new Command(() => Navigate(new UserManagementPage()))));
        }
        else if (role == "Consumer")
        {
            items.Add(new MenuItemModel("My Bills", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Register Complaint", new Command(() => Navigate(new UserManagementPage()))));
            items.Add(new MenuItemModel("Complaint History", new Command(() => Navigate(new UserManagementPage()))));
        }
        else
        {
            DisplayAlert("Error", "Something went wrong. Please try again.", "OK");
        }

        return items;
    }

    private void Navigate(ContentPage page)
    {
        MainContentArea.Content = page.Content;
        Sidebar.IsVisible = false;
        
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirmed = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (confirmed)
        {
             
            await Navigation.PopToRootAsync();  
        }
    }

}
