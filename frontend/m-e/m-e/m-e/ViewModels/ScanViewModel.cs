using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace m_e.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        public ScanViewModel()
        {
            Title = "Scan";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}