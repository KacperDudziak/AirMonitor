using AirMonitor.Views;
using Xamarin.Forms;

namespace AirMonitor
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new RootTabbedPage();
        }
    }
}
