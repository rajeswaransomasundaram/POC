using System;
using System.Collections.Generic;
using TelstraPOC.ViewModels;
using Xamarin.Forms;

namespace TelstraPOC.Views
{
    public partial class MyDataListPage : ContentPage
    {
        public MyDataListPage()
        {
            InitializeComponent();

            BindingContext = new MyDataListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>("Error", "ConnectionError", (sender) => {
                // do something whenever the "Hi" message is sent
                DisplayAlert("Error", "Network Connection is not available", "OK");
            });

            MessagingCenter.Subscribe<string>("Error", "HostError", (sender) =>
            {
                DisplayAlert("Error", "Host Not Reachable", "OK");
            });

            MessagingCenter.Subscribe<string>("Error", "404Error", (sender) =>
              {
                  DisplayAlert("Error", "Error 404 not found", "OK");
              });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>("Error", "ConnectionError");
            MessagingCenter.Unsubscribe<string>("Error", "HostError");
            MessagingCenter.Unsubscribe<string>("Error", "404Error");
        }

    }
}
