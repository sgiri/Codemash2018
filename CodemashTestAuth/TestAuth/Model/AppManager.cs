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


namespace TestAuth.Model
{
    public static class AppManager
    {
        public static UserDetails User;
        public static void GetFavoritesFromFirebase(string userid)
        {
            try
            {
                FavoritesHandler favoritesHandler = (favoritesString) =>
                {
                    var favoritesText = favoritesString;
                    var i = JsonConvert.DeserializeObject<string>(favoritesText);

                    List<int> ids = new List<int>();
                    string[] arr = i.Split(',');
                    int k = 0;
                    for (int j = 0; j < arr.Length; j++)
                    {                     
                        bool result = int.TryParse(arr[j], out k);
                        if (result)
                        {
                            ids.Add(k);
                        }
                    }
                    App.FavoritesessionIDs = ids;

                    List<int> favs = App.FavoritesessionIDs;

                    if (favs.Count > 0)
                    {
                        foreach (SessionModel model in App.allsessionList)
                        {
                            var sessionItem = (from item in favs
                                               where item == model.Id
                                               select item).FirstOrDefault();

                            if (sessionItem > 0)
                            {                               
                                model.isFavorite = true;
                                App.Favoritesessions.Add(model);
                            }
                        }
                    }

                };
                FireBaseDBService.Favorites(favoritesHandler, userid);

            }
            catch (Exception ex)
            {
                string jj = ex.Message;
            }
        }


        public static void SaveFavoritesToFirebase(string userid, string token, string sessionid)
        {
            try
            {
                FavoritesUpdateHandler favoritesUpdateHandler = (test) =>
                {
                  
                };
                FireBaseDBService.UpdateFavorites(favoritesUpdateHandler, userid, token, sessionid);

            }
            catch (Exception ex)
            {
                string jj = ex.Message;
            }
        }


    }
}
