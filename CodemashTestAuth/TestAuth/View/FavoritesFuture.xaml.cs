using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestAuth.Model;
using TestAuth.ViewModel;

namespace TestAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesFuture : ContentPage
    {
        public FavoritesFuture()
        {
            InitializeComponent();
            GetData();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //   GetData();
        }
        private async Task GetData()
        {
            try
            {
                List<int> ids = new List<int>();
                ids = App.FavoritesessionIDs;


                if (App.Favoritesessions != null)
                {
                    App.Favoritesessions.Clear();
                }

                foreach (int id in ids)
                {
                    var sessionItem = (from item in App.allsessionList
                                       where item.Id == id
                                       select item).FirstOrDefault();
                    if (sessionItem != null)
                    {
                        if (DateTime.Parse(sessionItem.SessionStartTime) >= DateTime.Now)
                        {
                            sessionItem.isFavorite = true;
                            App.Favoritesessions.Add((SessionModel)sessionItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string jj = ex.Message;
            }
            lvSession3.ItemsSource = App.Favoritesessions;
        }

        public void Favorites_Clicked(object sender, EventArgs e)
        {
            try
            {
                Image imageSender = (Image)sender;
                SessionModel sendercontext = (SessionModel)imageSender.BindingContext;
                var id = sendercontext.Id;
                if (App.Favoritesessions != null)
                {
                    var session = App.Favoritesessions.FirstOrDefault(f => f.Id == id);
                    if (session != null)
                    {
                        if (session.isFavorite)
                        {
                            var sessionItem = (from item in App.Favoritesessions
                                               where item.Id == session.Id
                                               select item).FirstOrDefault();

                            if (sessionItem != null)
                            {
                                session.isFavorite = false;
                                App.Favoritesessions.Remove((SessionModel)sessionItem);

                                if (AppManager.User == null)
                                {
                                    Navigation.PushModalAsync(new ProviderLoginPage());
                                }
                                else
                                {
                                    string firebaseToken = AppManager.User.FirebaseToken;
                                    string userid = AppManager.User.UserId;
                                    List<string> sessIds = new List<string>();
                                    List<int> sessionIds = new List<int>();
                                    foreach (SessionModel s in App.Favoritesessions)
                                    {
                                        sessIds.Add(s.Id.ToString());
                                        sessionIds.Add(s.Id);
                                    }

                                    // Make it comma separated session ids.
                                    string commaSeparatedSessionIds = string.Join(",", sessIds);
                                    App.FavoritesessionIDs = sessionIds;
                                    AppManager.SaveFavoritesToFirebase(userid, firebaseToken, commaSeparatedSessionIds);

                                }


                            }
                        }
                        else
                        {
                            // check if it is already there before adding
                            var sess = App.Favoritesessions.FirstOrDefault(f => f.Id == id);
                            if (sess == null)
                            {
                                session.isFavorite = true;
                                App.Favoritesessions.Add((SessionModel)session);

                                if (AppManager.User == null)
                                {
                                    Navigation.PushModalAsync(new ProviderLoginPage());
                                }
                                else
                                {
                                    string firebaseToken = AppManager.User.FirebaseToken;
                                    string userid = AppManager.User.UserId;
                                    List<string> sessIds = new List<string>();
                                    List<int> sessionIds = new List<int>();
                                    foreach (SessionModel s in App.Favoritesessions)
                                    {
                                        sessIds.Add(s.Id.ToString());
                                        sessionIds.Add(s.Id);
                                    }

                                    // Make it comma separated session ids.
                                    string commaSeparatedSessionIds = string.Join(",", sessIds);
                                    App.FavoritesessionIDs = sessionIds;
                                    AppManager.SaveFavoritesToFirebase(userid, firebaseToken, commaSeparatedSessionIds);
                                }


                            }
                        }
                        lvSession3.ItemsSource = App.Favoritesessions;
                    }
                }
            }
            catch (Exception ex)
            {
                string h = ex.Message;
            }
        }
    }
}
