using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7.Models;
using Week7.Views.DetailViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Week7.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BackgroundColor = Constants.BackGroundColor;
            App.startCheckIfInternet(lblNoInternet, this);
        }

        async void SelectedScreen1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoScreen1());
        }

    }
}