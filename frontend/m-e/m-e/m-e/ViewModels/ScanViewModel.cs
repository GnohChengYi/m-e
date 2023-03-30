using System;
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
            var result = await scanner.Scan();
            if (result != null)
            {
                ScanResult = result.Text;
            }
        }
    }
}