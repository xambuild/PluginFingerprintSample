using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FingerprintSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnFPLogin_Clicked(object sender, EventArgs e)
        {
            var result = await CrossFingerprint.Current.IsAvailableAsync();

            if (result) 
            {
                var dialogConfig = new AuthenticationRequestConfiguration("Login using fingerprint", "Confirm login with your fingerprint")
                {
                    FallbackTitle = "Use Password",
                    AllowAlternativeAuthentication = true,
                };

                var auth = await CrossFingerprint.Current.AuthenticateAsync(dialogConfig);
                if (auth.Authenticated) 
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new MainPage();
                    });
                   
                }
                else 
                {
                    await DisplayAlert("Authentication Failed", "Fingerprint authentication failed", "CLOSE");
                }
            }
        }
    }
}