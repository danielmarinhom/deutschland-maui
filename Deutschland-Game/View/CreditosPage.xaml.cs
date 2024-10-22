using Deutschland_Game.Models.ViewModels;
using Deutschland_Game.Service;
using System.Diagnostics;
using System.Text.Json;

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
    protected override bool OnBackButtonPressed()
    {
        Navigation.PopAsync();
        return true;
    }

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