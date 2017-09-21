using CkziuApp.ViewModel.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanPage : TabbedPage
    {
        public PlanPage()
        {
            InitializeComponent();
            PlanPageViewModel vm = new PlanPageViewModel();
            this.BindingContext = vm;
        }
    }
}
