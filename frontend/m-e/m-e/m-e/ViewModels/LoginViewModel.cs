using Plugin.Fingerprint.Abstractions;
using m_e.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading;

namespace m_e.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command FingerprintLoginCommand { get; }
        public Command PasswordLoginCommand { get; }

        public LoginViewModel()
        {
            FingerprintLoginCommand = new Command(FingerprintLogin);
            PasswordLoginCommand = new Command(OnLoginClicked);
        }

        private async void FingerprintLogin()
        {
            var _cancel = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var reason = "Login";
            var dialogConfig = new AuthenticationRequestConfiguration("M-E", reason);
            var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);
            if (result.Authenticated)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
