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
            MessagingCenter.Subscribe<string,string>("Error", "AppError", (sender,args) => {
                DisplayAlert("Error", args, "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>("Error", "AppError");
        }

    }
}
