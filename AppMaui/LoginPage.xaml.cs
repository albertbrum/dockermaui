namespace AppMaui;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Please enter username and password.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Simple hardcoded authentication
        if (username == "admin" && password == "password")
        {
            // Navigate to MainPage
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            ErrorLabel.Text = "Invalid username or password.";
            ErrorLabel.IsVisible = true;
        }
    }
}