using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TestAuth.View
{
    public partial class UpdateDataPage : ContentPage
    {
        public UpdateDataPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Unsubscribe<string>(this, "UpdateDone");
            MessagingCenter.Subscribe<string>(this, "UpdateDone", async errorMessage => await Navigation.PushModalAsync(new TestAuth.View.FavoritesFuture()));

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "UpdateDone");

        }
    }
}
