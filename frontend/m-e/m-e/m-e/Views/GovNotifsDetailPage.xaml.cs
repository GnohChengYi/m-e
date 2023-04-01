using m_e.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace m_e.Views
{
    public partial class GovNotifsDetailPage : ContentPage
    {
        public GovNotifsDetailPage()
        {
            InitializeComponent();
            BindingContext = new GovNotifsDetailViewModel();
        }
    }
}