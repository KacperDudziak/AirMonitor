using System.Windows.Input;
using AirMonitor.Views;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    public class HomeViewModel
    {
        readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            GoToDetails = new Command(OnGoToDetails);
        }

        public ICommand GoToDetails { get; private set; }

        void OnGoToDetails() => navigation.PushAsync(new DetailsPage());
    }
}
