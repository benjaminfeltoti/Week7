using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Week7.Data;
using Week7.Droid.Data;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Week7.Droid.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool isConnected { get; set; }

        public void checkNetworkConnection()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null)
            {
                isConnected = true;
            }
            else
            {
                isConnected = false;
            }
        }
    }
}