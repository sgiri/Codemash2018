using TestAuth.Model;
using TestAuth.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Auth;

namespace TestAuth.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new MainPageViewModel();
            }
            catch (Exception ex)
            {
                string h = ex.Message;
            }
        }

        private void LvMain_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var main = (MainPageModel)e.SelectedItem;
            MainPageModel p = e.SelectedItem as MainPageModel;
            //DisplayAlert("Selected", p.Display, "OK");

            switch (main.Id)
            {
                case 1:
                     Navigation.PushAsync(new NavigationPage(new TestAuth.View.SessionTab()));
                   // Navigation.PushModalAsync(new TestAuth.View.SessionTab());
                    break;
                case 2:
                      Navigation.PushAsync(new NavigationPage(new TestAuth.View.Speakers()));
                  //  Navigation.PushModalAsync(new TestAuth.View.Speakers());
                    break;
                case 3:
                    if (string.IsNullOrEmpty(App.token))
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
                    else
                    {
                        Navigation.PushAsync(new NavigationPage(new TestAuth.View.FavoritesFuture()));
                     //   Navigation.PushModalAsync(new TestAuth.View.FavoritesFuture());
                    }
                    break;
                case 4:
                    Navigation.PushAsync(new NavigationPage(new TestAuth.View.FavoritesFuture()));
                    break;
                case 5:
                    //     Navigation.PushModalAsync(new TestAuth.View.Map());
                    Navigation.PushModalAsync(new TestAuth.View.GoogleSignIn());
                    break;
                default:
                    break;//do nothing for now
            }

            lvMain.SelectedItem = null;
        }
    }
}

