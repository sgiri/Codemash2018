using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CodeMash.Model;
using CodeMash.ViewModel;

namespace CodeMash.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesFuture : ContentPage
    {
        public FavoritesFuture()
        {
            InitializeComponent();           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetData();
        }
        private async Task GetData()
        {
            try
            {
                List<int> ids = App.GetFavorites();
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
                                App.SaveFavorites();
                            }
                        }
                        //else
                        //{
                        //    // check if it is already there before adding
                        //    var sess = App.Favoritesessions.FirstOrDefault(f => f.Id == id);
                        //    if (sess == null)
                        //    {
                        //        session.isFavorite = true;
                        //        App.Favoritesessions.Add((SessionModel)session);
                        //        App.SaveFavorites();
                        //    }
                        //}
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
