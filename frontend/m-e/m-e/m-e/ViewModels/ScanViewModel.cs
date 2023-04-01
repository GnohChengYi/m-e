using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Mobile;

namespace m_e.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        public ScanViewModel() 
        {
            Title = "Scan";
            ScanResult = "nothing yet";
            ScanCommand = new Command(() => scan());
        }

        string scanResult = string.Empty;

        public string ScanResult
        {
            get { return scanResult; }
            set { SetProperty(ref scanResult, value); }
        }

        public ICommand ScanCommand { get; }

        private async void scan()
        {
            // TODO flashlight permission
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (!status.Equals(PermissionStatus.Granted))
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
                if (!status.Equals(PermissionStatus.Granted))
                {
                    // TODO user does not grant permission, return to home
                    ScanResult = "no permission";
                    return;
                }
            }
            var scanner = new MobileBarcodeScanner();
            var scanResult = await scanner.Scan();
            if (scanResult != null)
            {
                ScanResult = scanResult.Text;
                bool answer = await App.Current.MainPage.DisplayAlert("Alert", "The Government Agency is requesting the following information:\n- Passport\n- IC\n- Address", "Agree", "Decline");
                Debug.WriteLine("Answer: " + answer);
                if (answer)
                {
                    giveConsent(scanResult.ToString());
                }
            }
        }

        private async void giveConsent(string scanResult)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://kyjqqy73i2.execute-api.ap-southeast-1.amazonaws.com");
            // TODO send user token
            var content = new StringContent(scanResult);
            Debug.WriteLine("v073 before postAsync");
            HttpResponseMessage response = await client.PostAsync("/default/login", content);
            Debug.WriteLine($"v073 {response}");
            ScanResult = response.ToString();
            //var result = await response.Content.ReadAsStringAsync();
        }
    }
}
