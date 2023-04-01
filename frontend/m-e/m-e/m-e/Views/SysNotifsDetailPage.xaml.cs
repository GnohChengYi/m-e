using m_e.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace m_e.Views
{
    public partial class SysNotifsDetailPage : ContentPage
    {
        public SysNotifsDetailPage()
        {
            InitializeComponent();
            BindingContext = new SysNotifsDetailViewModel();
        }
    }
}