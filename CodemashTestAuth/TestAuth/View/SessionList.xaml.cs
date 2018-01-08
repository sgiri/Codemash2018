﻿using TestAuth.Model;
using TestAuth.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionsList : ContentPage
    {
        public ObservableCollection<SessionModel> sessionList { get; set; }
        public SessionsList(ObservableCollection<SessionModel> list, string title)
        {
            InitializeComponent();
            sessionList = list;
            lvSession.ItemsSource = sessionList;
            this.Title = title;
        }
        private void LvSession_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var session = (SessionModel)e.SelectedItem;
            //    Navigation.PushAsync(new NavigationPage(new TestAuth.View.SessionDetail(session)));

            Navigation.PushModalAsync(new TestAuth.View.SessionDetail(session));
            lvSession.SelectedItem = null;
        }

        private void BtnTitle_OnClicked(object sender, EventArgs e)
        {
            sessionList = new ObservableCollection<SessionModel>(sessionList.OrderBy(o => o.Title));
            BtnTitle.Image = "openbook.png";
            BtnTitle.BackgroundColor = Color.White;
            BtnDate.BackgroundColor = Color.Gray;
            BtnSpeaker.BackgroundColor = Color.Gray;
            BtnTechnologies.BackgroundColor = Color.Gray;
            lvSession.ItemsSource = sessionList;
        }

        private void BtnDate_OnClicked(object sender, EventArgs e)
        {
            sessionList = new ObservableCollection<SessionModel>(sessionList.OrderBy(o => o.SessionStartTime));
            BtnDate.Image = "calendar.png";
            BtnTitle.BackgroundColor = Color.Gray;
            BtnDate.BackgroundColor = Color.White;
            BtnSpeaker.BackgroundColor = Color.Gray;
            BtnTechnologies.BackgroundColor = Color.Gray;
            lvSession.ItemsSource = sessionList;
        }

        private void BtnSpeaker_OnClicked(object sender, EventArgs e)
        {
            sessionList = new ObservableCollection<SessionModel>(sessionList.OrderBy(o => o.SpeakerName));
            BtnSpeaker.Image = "user.png";
            BtnTitle.BackgroundColor = Color.Gray;
            BtnDate.BackgroundColor = Color.Gray;
            BtnSpeaker.BackgroundColor = Color.White;
            BtnTechnologies.BackgroundColor = Color.Gray;
            lvSession.ItemsSource = sessionList;
            lvSession.ItemsSource = sessionList;
        }

        private void BtnTechnologies_OnClicked(object sender, EventArgs e)
        {
            sessionList = new ObservableCollection<SessionModel>(sessionList.OrderBy(o => o.Category));

            BtnTitle.BackgroundColor = Color.Gray;
            BtnDate.BackgroundColor = Color.Gray;
            BtnSpeaker.BackgroundColor = Color.Gray;
            BtnTechnologies.BackgroundColor = Color.White;
            lvSession.ItemsSource = sessionList;
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
                    var session = sessionList.FirstOrDefault(f => f.Id == id);
                    string commaSeparatedSessionIds = "";
                    bool updateDataStore = false;
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
                                List<string> sessIds = new List<string>();
                                List<int> sessionIds = new List<int>();
                                foreach (SessionModel s in App.Favoritesessions)
                                {
                                    sessIds.Add(s.Id.ToString());
                                    sessionIds.Add(s.Id);
                                }

                                // Make it comma separated session ids to store in firebase.
                                commaSeparatedSessionIds = string.Join(",", sessIds);
                                App.FormattedFavsessionIDs = commaSeparatedSessionIds;
                                App.FavoritesessionIDs = sessionIds;
                                updateDataStore = true;
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

                                List<string> sessIds = new List<string>();
                                List<int> sessionIds = new List<int>();
                                foreach (SessionModel s in App.Favoritesessions)
                                {
                                    sessIds.Add(s.Id.ToString());
                                    sessionIds.Add(s.Id);
                                }
                                // Make it comma separated session ids.
                                commaSeparatedSessionIds = string.Join(",", sessIds);
                                App.FormattedFavsessionIDs = commaSeparatedSessionIds;
                                App.FavoritesessionIDs = sessionIds;
                                updateDataStore = true;
                            }
                        }
                        ((Image)sender).Source = session.FavImage;
                        lvSession.ItemsSource = sessionList;

                        if (updateDataStore)
                        {
                            if (AppManager.User != null)
                            {
                                // save it local db and firebase                             
                                if (String.IsNullOrEmpty(AppManager.User.UserId))
                                {
                                   Navigation.PushModalAsync(new UpdateDataPage());
                                }
                                else
                                {
                                    if (String.IsNullOrEmpty(AppManager.User.FirebaseToken))
                                    {
                                        Navigation.PushModalAsync(new UpdateDataPage());
                                    }
                                    else
                                    {
                                        AppManager.SaveFavoritesToFirebase(AppManager.User.UserId, AppManager.User.FirebaseToken, commaSeparatedSessionIds);
                                    }
                                }
                            }
                            else
                            {
                                Navigation.PushModalAsync(new UpdateDataPage());
                            }
                        }
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

