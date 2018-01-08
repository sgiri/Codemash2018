using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Auth;

namespace TestAuth
{
    public partial class LoginPage : ContentPage
    {
       
        public LoginPage()
        {
            InitializeComponent();
        }

        public void Handle_Clicked(object sender, System.EventArgs e)
        {
            OAuthProviderSetting oauth = new OAuthProviderSetting();
            OAuth1Authenticator authenticator = oauth.LoginWithTwitter();

            authenticator.Completed +=
                (s, ea) =>
                {

                    App.token = ea.Account.Properties["oauth_token"];
                    App.tokensecret = ea.Account.Properties["oauth_token_secret"];

                    Navigation.PushModalAsync(new ProviderLoginPage());

                };
            authenticator.Error +=
                   (s, ea) =>
                   {

                       string h = ea.Message;


                       return;
                   };


            Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
            presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);


        }

       

    }
}
