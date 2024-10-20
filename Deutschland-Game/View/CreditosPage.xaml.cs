namespace Deutschland_Game.View;

public partial class CreditosPage : ContentPage
{
	public CreditosPage()
	{
		InitializeComponent();
	}

    private void gitHubImageBtn_Clicked(object sender, EventArgs e)
    {

        modalSection.IsVisible = true;

    }

    private async void backIconBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
<<<<<<< HEAD
    protected override bool OnBackButtonPressed()
    {
        Navigation.PopAsync();
        return true;
    }
=======
>>>>>>> eb4d2c8408a20fd27e137362d9a9e1599ec5aeef

    private void backendBtn_Clicked(object sender, EventArgs e)
    {
        Uri uri = new Uri("https://github.com/EduardoPQueiroz/deutschland_game_project");
        tryToOpenGitHub(uri);
    }

    private void frontendBtn_Clicked(object sender, EventArgs e)
    {
        Uri uri = new Uri("https://github.com/danielmarinhom/deutschland-maui");
        tryToOpenGitHub(uri);
    }

    private void closeModalBtn_Clicked(object sender, EventArgs e)
    {
        modalSection.IsVisible = false;
    }

    public async Task<bool> canOpenGitHubApp(Uri uri)
    {
        return await Launcher.TryOpenAsync(uri);
    }

    public async void openGitHubWebSite(Uri uri)
    {
        await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }

    public void tryToOpenGitHub(Uri uri)
    {
        bool gitHubAppOpened = canOpenGitHubApp(uri).Result;
        if (!gitHubAppOpened)
        {
            openGitHubWebSite(uri);
        }
    }
}