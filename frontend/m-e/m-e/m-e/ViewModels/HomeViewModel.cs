using m_e.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace m_e.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command LoadProfileCommand { get; }

        public HomeViewModel()
        {
            Shell.Current.GoToAsync("//LoginPage");
            Title = "Home";
            IsBusy = true;
            LoadProfileCommand = new Command(async () => await ExecuteLoadProfileCommand());
            OpenMySST = new Command(async () => await Browser.OpenAsync("https://mysst.customs.gov.my"));
            OpenMySIKAP = new Command(async () => await Browser.OpenAsync("https://public.jpj.gov.my/public/login.zul"));
            OpenMyPassport = new Command(async () => await Browser.OpenAsync("https://imigresen-online.imi.gov.my/eservices/myPasport"));
        }

        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string ic = string.Empty;
        public string IC
        {
            get { return ic; }
            set { SetProperty(ref ic, value); }
        }

        string licenseValidity = string.Empty;
        public string LicenseValidity
        {
            get { return licenseValidity; }
            set { SetProperty(ref licenseValidity, value); }
        }

        public ICommand OpenMySST { get; }

        public ICommand OpenMySIKAP { get; }

        public ICommand OpenMyPassport { get; }

        async Task ExecuteLoadProfileCommand()
        {
            IsBusy = true;
            try
            {
                Profile profile = await DataStore.GetProfileAsync(true);
                Debug.WriteLine(profile);
                Name = profile.Name;
                Debug.WriteLine("after name");
                IC = "IC: " + profile.IC;
                Debug.WriteLine("after ic");
                LicenseValidity = profile.LicenseValidity;
                Debug.WriteLine("after license");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}