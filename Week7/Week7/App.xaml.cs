using System;
using System.Threading;
using System.Threading.Tasks;
using Week7.Data;
using Week7.Models;
using Week7.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Week7
{
    public partial class App : Application
    {
        static TokenDatabaseController tokenDatabase;
        static UserDatabaseController userDatabase;
        static RestService restService;
        private static Label labelScreen;
        private static bool hasInternet;
        private static Page currentPage;
        private static Timer timer;
        private static bool noInternetShow;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static UserDatabaseController UserDatabase 
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }

                return userDatabase;
            }
        }

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabaseController();
                }

                return tokenDatabase;
            }
        }

        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }

                return restService;
            }
        }

        public static void startCheckIfInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            currentPage = page;

            if (timer == null)
            {
                timer = new Timer((e) =>
                {
                    checkInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        private static void checkInternetOverTime()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.checkNetworkConnection();

            if (!networkConnection.isConnected)
            {
                Device.BeginInvokeOnMainThread(async () =>
               {
                   if (hasInternet)
                   {
                       if (!noInternetShow)
                       {
                           hasInternet = false;
                           labelScreen.IsVisible = true;
                           await ShowDisplayAlert();
                       }
                   }
               });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        public static async Task<bool> checkIfInternet()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.checkNetworkConnection();

            if (!networkConnection.isConnected)
            {
                if (!noInternetShow)
                {
                    await ShowDisplayAlert();
                    return false;
                }
            }

            return true;
        }

        private static async Task ShowDisplayAlert()
        {
            noInternetShow = false;
            await currentPage.DisplayAlert("Internet", "Device has no internet, please reconnect!", "Ok");
            noInternetShow = false;
        }
    }
}
