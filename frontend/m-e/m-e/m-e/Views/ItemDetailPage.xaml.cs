using m_e.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace m_e.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}