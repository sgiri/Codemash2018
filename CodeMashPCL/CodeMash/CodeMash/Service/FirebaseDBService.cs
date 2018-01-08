using System;
using System.Net.Http;

namespace CodeMash.Service
{
    public delegate void SessionsHandler(string sessionsString);
    public delegate void SpeakersHandler(string speakersString);
    public delegate void UsersHandler(string usersString);

    public class FireBaseDBService
    {
        private static string BaseURI = "https://codemashconfapp.firebaseio.com/";
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

        public static string GetUser(string id)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(BaseURI + "/users.json"));
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

    }
}

