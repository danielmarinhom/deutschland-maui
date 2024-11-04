
using Deutschland_Game.Service;

namespace Deutschland_Game.View;

public partial class CreditosPage : ContentPage
{
    AudioService audioService;
	public CreditosPage(AudioService audioService)
	{
		InitializeComponent();
        this.audioService = audioService;
    }

    private void gitHubImageBtn_Clicked(object sender, EventArgs e)
    {
        audioService.PlayClickAudio();
        modalSection.IsVisible = true;

    }

    private async void backIconBtn_Clicked(object sender, EventArgs e)
    {
        audioService.PlayClickAudio();
        await Navigation.PopAsync();
    }
    protected override bool OnBackButtonPressed()
    {
        audioService.PlayClickAudio();
        Navigation.PopAsync();
        return true;
    }

    private void backendBtn_Clicked(object sender, EventArgs e)
    {
        audioService.PlayClickAudio();
        Uri uri = new Uri("https://github.com/EduardoPQueiroz/deutschland_game_project");
        tryToOpenGitHub(uri);
    }

    private void frontendBtn_Clicked(object sender, EventArgs e)
    {
        audioService.PlayClickAudio();
        Uri uri = new Uri("https://github.com/danielmarinhom/deutschland-maui");
        tryToOpenGitHub(uri);
    }

    private void closeModalBtn_Clicked(object sender, EventArgs e)
    {
        audioService.PlayClickAudio();
        modalSection.IsVisible = false;
    }

    public async Task<bool> canOpenGitHubApp(Uri uri)
    {
        return await Launcher.TryOpenAsync(uri);
    }

    public async void openGitHubWebSite(Uri uri)
    {
        audioService.PlayClickAudio();
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