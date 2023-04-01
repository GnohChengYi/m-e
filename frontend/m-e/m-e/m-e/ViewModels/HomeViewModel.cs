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
        public Command LogOut { get; }

        public HomeViewModel()
        {
            Shell.Current.GoToAsync("//LoginPage");
            Title = "Home";
            IsBusy = true;
            LastLogin = "Last login: " + DateTime.Now.Subtract(TimeSpan.FromHours(2)).ToString("MMM dd HH:mm:ss");
            DisplayDocument = "IC.png";
            LoadProfileCommand = new Command(async () => await ExecuteLoadProfileCommand());
            LogOut = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
            DisplayIC = new Command(() => { DisplayDocument = "IC.png"; resetBorders(); IC_Border = "Black"; });
            DisplayLicense = new Command(() => { DisplayDocument = "lesen.png"; resetBorders(); License_Border = "Black"; });
            DisplayAddCard = new Command(() => { DisplayDocument = "add_card.png"; resetBorders(); Add_Border = "Black"; });
            OpenMySST = new Command(async () => await Browser.OpenAsync("https://mysst.customs.gov.my"));
            OpenMySIKAP = new Command(async () => await Browser.OpenAsync("https://public.jpj.gov.my/public/login.zul"));
            OpenMyPassport = new Command(async () => await Browser.OpenAsync("https://imigresen-online.imi.gov.my/eservices/myPasport"));
            OpenPenang = new Command(async () => await Browser.OpenAsync("https://www.penang.gov.my/"));
            OpenBayar = new Command(async () => await Browser.OpenAsync("https://mybayar.rmp.gov.my/en"));
            OpenMPM = new Command(async () => await Browser.OpenAsync("https://www.mpm.edu.my/stpm-online/semakan-keputusan-stpm"));
            OpenSirip = new Command(async () => await Browser.OpenAsync("https://eservices.dof.gov.my/ePerkhidmatan/index.php?mod=authentication&amp;opt=login"));
            OpenMySejahtera = new Command(async () => await Browser.OpenAsync("https://play.google.com/store/apps/details?id=my.gov.onegovappstore.mysejahtera"));
        }

        string greeting = string.Empty;
        public string Greeting
        {
            get { return greeting; }
            set { SetProperty(ref greeting, value); }
        }

        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string lastLogin = string.Empty;
        public string LastLogin
        {
            get { return lastLogin; }
            set { SetProperty(ref lastLogin, value); }
        }

        string displayDocument = string.Empty;
        public string DisplayDocument
        {
            get { return displayDocument; }
            set { SetProperty(ref displayDocument, value); }
        }

        public string ic_border = "Black";
        public string IC_Border
        {
            get { return ic_border; }
            set { SetProperty(ref ic_border, value); }
        }

        public string license_border;
        public string License_Border
        {
            get { return license_border; }
            set { SetProperty(ref license_border, value); }
        }

        public string add_border;
        public string Add_Border
        {
            get { return add_border; }
            set { SetProperty(ref add_border, value); }
        }

        public void resetBorders()
        {
            IC_Border = "#00000000";
            License_Border = "#00000000";
            Add_Border = "#00000000";
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

        public ICommand DisplayIC { get; }

        public ICommand DisplayLicense { get; }

        public ICommand DisplayAddCard { get; }

        public ICommand OpenMySST { get; }

        public ICommand OpenMySIKAP { get; }

        public ICommand OpenMyPassport { get; }
        public ICommand OpenPenang { get; }
        public ICommand OpenBayar { get; }
        public ICommand OpenMPM { get; }
        public ICommand OpenSirip { get; }
        public ICommand OpenMySejahtera { get; }

        async Task ExecuteLoadProfileCommand()
        {
            IsBusy = true;
            try
            {
                Profile profile = await DataStore.GetProfileAsync(true);
                string am_pm = DateTime.Now.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);
                if (am_pm.Equals("AM"))
                {
                    Greeting = "Good morning,";
                }
                else if (am_pm.Equals("PM"))
                {
                    Greeting = "Good afternoon,";
                }
                else
                {
                    Greeting = "Welcome,";
                }
                Name = profile.Name;
                IC = "IC: " + profile.IC;
                LicenseValidity = profile.LicenseValidity;
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