//using TestAuth.Data;
using TestAuth.Model;
using TestAuth.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using TestAuth.Service;

namespace TestAuth
{
    public partial class App : Application
    {
        public static ObservableCollection<SessionModel> Favoritesessions { get; set; }
        public static ObservableCollection<SessionModel> allsessionList { get; set; }
        public static ObservableCollection<SessionModel> generalSessionList { get; set; }
        public static ObservableCollection<SessionModel> precompilerSessionList { get; set; }
        public static ObservableCollection<SessionModel> kidsSessionList { get; set; }
        public static ObservableCollection<SpeakerModel> allspeakerList { get; set; }
        public static ObservableCollection<SpeakerDetailModel> allspeakerDetailList { get; set; }

        public static List<int> FavoritesessionIDs { get; set; }
        public static string FormattedFavsessionIDs { get; set; }

        public static string token { get; set; }
        public static string tokensecret { get; set; }

        //   static CodeMashDatabase database;
        public App()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                
                MainPage = new NavigationPage(new TestAuth.View.MainPage());
            }
            else
                MainPage = new NavigationPage(new TestAuth.View.MainPage());
          
        }

        //public static CodeMashDatabase Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new CodeMashDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("FavSQLite.db3"));
        //        }
        //        return database;
        //    }
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
            Favoritesessions = new ObservableCollection<SessionModel>();
            allsessionList = new ObservableCollection<SessionModel>();
            generalSessionList = new ObservableCollection<SessionModel>();
            precompilerSessionList = new ObservableCollection<SessionModel>();
            kidsSessionList = new ObservableCollection<SessionModel>();
            allspeakerList = new ObservableCollection<SpeakerModel>();
            allspeakerDetailList = new ObservableCollection<SpeakerDetailModel>();
            FavoritesessionIDs = new List<int>();
            //  GetSessionData();
            GetFireBaseData();
        }

        public void GetFireBaseData()
        {
            SessionsHandler handler = (sessionsString) =>
            {
                var sessionsText = sessionsString;
                allsessionList = JsonConvert.DeserializeObject<ObservableCollection<SessionModel>>(sessionsText);

                precompilerSessionList = new ObservableCollection<SessionModel>(allsessionList.Where(x => x.SessionType == "Pre-Compiler"));

                generalSessionList = new ObservableCollection<SessionModel>(allsessionList.Where(x => x.SessionType == "General Session"));

                kidsSessionList = new ObservableCollection<SessionModel>(allsessionList.Where(x => x.SessionType == "Kidz Mash"));
            };
            FireBaseDBService.Sessions(handler);

            SpeakersHandler speakerhandler = (speakersString) =>
            {
                var speakersText = speakersString;
                allspeakerList = JsonConvert.DeserializeObject<ObservableCollection<SpeakerModel>>(speakersText);
                allspeakerDetailList = JsonConvert.DeserializeObject<ObservableCollection<SpeakerDetailModel>>(speakersText);
            };
            FireBaseDBService.Speakers(speakerhandler);
            if (AppManager.User != null)
            {
                if (!string.IsNullOrEmpty(AppManager.User.UserId))
                {
                    AppManager.GetFavoritesFromFirebase(AppManager.User.UserId);
                }
            }
            else
            {
                // get from local db
            }
        }

        public static Task<int> SaveFavorites()
        {
            try
            {
                List<FavSessionModel> favs = new List<FavSessionModel>();
                foreach (SessionModel m in Favoritesessions)
                {
                    FavSessionModel f = new FavSessionModel();
                    f.SessionId = m.Id;
                    favs.Add(f);

                }
                //  var t = Database.SaveItems(favs);
                //  return t;
                return null;
            }
            catch (Exception ex)
            {
                string jj = ex.Message;
            }
            return null;
        }

        public static List<int> GetFavorites()
        {
            try
            {
                List<int> ids = new List<int>();
                List<FavSessionModel> favorites = new List<FavSessionModel>();

                List<SessionModel> temp = Favoritesessions.ToList();
                foreach (SessionModel m in temp)
                {
                    if (m.isFavorite)
                    {
                        ids.Add(m.Id);
                    }
                }
               
                return ids;
            }
            catch (Exception ex)
            {
                string jj = ex.Message;

            }
            return null;
        }

       
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
