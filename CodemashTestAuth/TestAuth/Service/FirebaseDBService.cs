using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestAuth.Service
{
    public delegate void SessionsHandler(string sessionsString);
    public delegate void SpeakersHandler(string speakersString);
    public delegate void UsersHandler(string usersString);
    public delegate void FavoritesHandler(string favoritesString);
    public delegate void FavoritesUpdateHandler(string result);

    public class FireBaseDBService
    {
        private static string BaseURI = "https://testauth-49443.firebaseio.com/";
        public static void Sessions(SessionsHandler response)
        {
            response(SessionsInner());
        }
        private static string SessionsInner()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(BaseURI + "sessions2018.json"));
                client.BaseAddress = uri;
                try
                {
                    var response = client.GetAsync(uri).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        return responseContent.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    return null;
                }
            }
        }

        public static void Speakers(SpeakersHandler response)
        {
            response(SpeakersInner());
        }

        private static string SpeakersInner()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(BaseURI + "/speakers2018.json"));
                client.BaseAddress = uri;
                try
                {
                    var response = client.GetAsync(uri).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        return responseContent.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static void Favorites(FavoritesHandler response, string userid)
        {
            response(GetUser(userid));
        }

        //public static async Task<string> GetUserByUid(string id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        string url = String.Format("https://testauth-49443.firebaseio.com/users/{0}/SessionIds/.json", id);
        //        var uri = new Uri(url);

        //        client.BaseAddress = uri;
        //        try
        //        {
        //            var response = client.GetAsync(uri).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseContent = response.Content;
        //                var h = await responseContent.ReadAsStringAsync();
        //                return h;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        public static string GetUser(string id)
        {
            using (var client = new HttpClient())
            {
                string url = String.Format("https://testauth-49443.firebaseio.com/users/{0}/SessionIds/.json", id);
                var uri = new Uri(url);
             
                client.BaseAddress = uri;
                try
                {
                    var response = client.GetAsync(uri).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        return responseContent.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static void UpdateFavorites(FavoritesUpdateHandler response, string userid, string token, string sessionid)
        {
            response(UpdateFirebaseData(userid, token, sessionid));
        }

        public static string UpdateFirebaseData(string userid, string token, string sessionid)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Format("https://testauth-49443.firebaseio.com/users/{0}/SessionIds/.json?auth={1}", userid, token);
                var uri = new Uri(url);
                var jsonstr = JsonConvert.SerializeObject(sessionid);            
                var content2 = new System.Net.Http.StringContent(jsonstr, Encoding.UTF8, "application/json");

                try
                {
                    var response = httpClient.PutAsync(uri, content2).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        return responseContent.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    string h1 = ex.Message;
                }
                return null;
            }
        }

    }
}

