using CodeMash.Data;
using CodeMash.Model;
using CodeMash.View;
using CodeMash.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using CodeMash.Service;

namespace CodeMash
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

        static CodeMashDatabase database;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new CodeMash.MainPage());
        }

        public static CodeMashDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new CodeMashDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("FavSQLite.db3"));
                }
                return database;
            }
        }

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
            //  GetSessionData();

            GetFireBaseData();
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
                var t  = Database.SaveItems(favs);
                return t;
            }
            catch(Exception ex)
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
                List<FavSessionModel> favorites = Database.GetItems().Result;             
                foreach (FavSessionModel m in favorites)
                {
                    ids.Add(m.SessionId);
                }
                return ids;
            }
            catch (Exception ex)
            {
                string jj = ex.Message;
            }
            return null;           
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
        }


        private async Task GetSessionData()
        {
            try
            {
                var assembly = typeof(SessionsList).GetTypeInfo().Assembly;
                using (var streamSpeaker = assembly.GetManifestResourceStream("CodeMash.Speakers.json"))
                {
                    string textSpeaker = "";
                    using (var reader = new System.IO.StreamReader(streamSpeaker))
                    {
                        textSpeaker = reader.ReadToEnd();
                    }
                    allspeakerList = JsonConvert.DeserializeObject<ObservableCollection<SpeakerModel>>(textSpeaker);

                    allspeakerDetailList  = JsonConvert.DeserializeObject<ObservableCollection<SpeakerDetailModel>>(textSpeaker);
                }
                using (var stream = assembly.GetManifestResourceStream("CodeMash.Session.json"))
                {
                    string text = "";
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        text = reader.ReadToEnd();
                    }
                    allsessionList = JsonConvert.DeserializeObject<ObservableCollection<SessionModel>>(text);
                }

                precompilerSessionList = new ObservableCollection<SessionModel>(allsessionList.Where(x => x.SessionType == "Pre-Compiler"));

                generalSessionList = new ObservableCollection<SessionModel>(allsessionList.Where(x => x.SessionType == "General Session"));

                kidsSessionList = new ObservableCollection<SessionModel>(allsessionList.Where(x => x.SessionType == "Kidz Mash"));

                List<int> favs = GetFavorites();
              
               if( favs.Count > 0)
                {
                    foreach (SessionModel model in allsessionList)
                    {
                        var sessionItem = (from item in favs
                                           where item == model.Id
                                           select item).FirstOrDefault();

                        if (sessionItem > 0)
                        {
                            model.isFavorite = true;                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string h = ex.Message;
            }
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
