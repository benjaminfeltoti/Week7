using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Week7.Models;
using Week7.Views.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Week7.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            BackgroundColor = Constants.BackGroundColor;
            lblUsername.TextColor = Constants.MainTextColor;
            lblPassword.TextColor = Constants.MainTextColor;
            activitySpinner.IsVisible = false;
            loginIcon.HeightRequest = Constants.LoginIconHeight;
            App.startCheckIfInternet(lblNoInternet, this);

            entryUsername.Completed += (s,e) => entryPassword.Focus();
            entryPassword.Completed += (s, e) => signIn(s, e);
        }

        private async void signIn(object sender, EventArgs e)
        {
            User user = new User(entryUsername.Text, createMd5(entryPassword.Text));
            if (user.checkInformation())
            {
                if (await App.checkIfInternet())
                {
                    Token result = await App.RestService.login(user);

                    //if (result.accessToken != null)
                    //{
                      //  var result = new Token();
                    //}

                    if (result != null)
                    {
                        DisplayAlert("Login", "Login Success!", "OK");
                        //App.UserDatabe.saveUser(user);
                        //App.TokenDatabase.saveToken(result);
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            Application.Current.MainPage = new NavigationPage(new Dashboard());
                        }
                    }
                }
            }
            else
            {
                DisplayAlert("Login", "Login Not correct, empty username or password", "Ok");
            }
        }

        private string createMd5(string input) 
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}