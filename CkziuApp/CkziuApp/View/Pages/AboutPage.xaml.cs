using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var assembly = typeof(CkziuApp.App);
            Logo.Source = ImageSource.FromResource("CkziuApp.Resources.ckziu.png", assembly);
        }
    }
}