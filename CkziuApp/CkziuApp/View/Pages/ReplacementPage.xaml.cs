using CkziuApp.ViewModel.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReplacementPage : ContentPage
    {
        public ReplacementPage()
        {
            InitializeComponent();
            ReplacementPageViewModel vm = new ReplacementPageViewModel();
            this.BindingContext = vm;
        }
        protected override void OnAppearing()
        {
            ReplacementPageViewModel vm = new ReplacementPageViewModel();
            this.BindingContext = vm;
        }
    }
}