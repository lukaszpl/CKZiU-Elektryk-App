using CkziuApp.ViewModel.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();          
        }
        protected override void OnAppearing()
        {
            webView.GoForward();
        }
        private void backClicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
                webView.GoBack();         
        }
    }
}