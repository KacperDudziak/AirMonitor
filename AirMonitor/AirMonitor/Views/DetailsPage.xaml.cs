using System;
using System.ComponentModel;
using Xamarin.Forms;
using AirMonitor.Airly;

namespace AirMonitor.Views
{
    [DesignTimeVisible(false)]
    public partial class DetailsPage : ContentPage
    {
        Measurement measurement = null;

        public DetailsPage() => InitializeComponent();


        private void HelpClicked(object sender, EventArgs e)
        {
            DisplayAlert(
                title: "What is CAQI?",
                message: "The CAQI is a number on a scale from 1 to 100, where a low value means good air quality and a high value means bad air quality.",
                cancel: "Close");
        }
    }
}
