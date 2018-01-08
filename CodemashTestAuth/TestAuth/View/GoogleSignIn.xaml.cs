using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TestAuth.View
{
    public partial class GoogleSignIn : ContentPage
    {
        public GoogleSignIn()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Unsubscribe<string>(this, "LoginDone");
            MessagingCenter.Subscribe<string>(this, "LoginDone", async errorMessage => await Navigation.PushModalAsync(new MainPage()));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "LoginDone");
        }
    }
}
