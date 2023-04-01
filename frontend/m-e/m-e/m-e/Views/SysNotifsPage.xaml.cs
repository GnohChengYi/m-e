using m_e.Models;
using m_e.ViewModels;
using m_e.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace m_e.Views
{
    public partial class SysNotifsPage : ContentPage
    {
        SysNotifsViewModel _viewModel;

        public SysNotifsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SysNotifsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}