using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace m_e.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Shell.Current.GoToAsync("//LoginPage");
            Title = "Home";
            OpenMySST = new Command(async () => await Browser.OpenAsync("https://mysst.customs.gov.my"));
            OpenMySIKAP = new Command(async () => await Browser.OpenAsync("https://public.jpj.gov.my/public/login.zul"));
            OpenMyPassport = new Command(async () => await Browser.OpenAsync("https://imigresen-online.imi.gov.my/eservices/myPasport"));
        }

        public ICommand OpenMySST { get; }

        public ICommand OpenMySIKAP { get; }

        public ICommand OpenMyPassport { get; }
    }
}