using CkziuApp.ViewModel.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FreeDaysPage : ContentPage
    {
        public FreeDaysPage()
        {
            InitializeComponent();
            FreeDaysPageViewModel vm = new FreeDaysPageViewModel();
            this.BindingContext = vm;
        }
    }
}