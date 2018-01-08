using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace TestAuth
{
    public class OAuthProviderSetting
    {
        public string ClientId { get; private set; }
        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string RequestTokenUrl { get; private set; }
        public string AccessTokenUrl { get; private set; }
        public string AuthorizeUrl { get; private set; }
        public string CallbackUrl { get; private set; }

        public enum OauthIdentityProvider
        {
            GOOGLE,
            FACEBOOK,
            TWITTER,           
            LINKEDIN,
            GITHUB    
        }

        public OAuth1Authenticator LoginWithTwitter()
        {
            OAuth1Authenticator Twitterauth = null;
            try
            {
                
                Twitterauth = new OAuth1Authenticator(
             consumerKey: "yYNCopS6LACEQoeRibGlDTPWY",
             consumerSecret: "IYZN5UsmPubDHUSccPLIc51rqAC2oaDxKq6M4McibM81Jl6rXi",
             requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
             authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
             accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
             callbackUrl: new Uri("https://testauth-49443.firebaseapp.com/__/auth/handler")

             );
            }
            catch (Exception ex)
            {
                // log exception
            }
            return Twitterauth;
        }


        public OAuth2Authenticator LoginWithProvider(string Provider)
        {
            OAuth2Authenticator auth = null;
            switch (Provider)
            {
                case "Google":
                    {
                        auth = new OAuth2Authenticator(
                                    "ClientId",
                                   "ClientSecret",
                                    "https://www.googleapis.com/auth/userinfo.email",
                                    new Uri("https://accounts.google.com/o/oauth2/auth"),
                                    new Uri("http://www.testauth.com"),
                                    new Uri("https://accounts.google.com/o/oauth2/token")
                                    );

                        break;
                    }
                case "FaceBook":
                    {
                        auth = new OAuth2Authenticator(
                        clientId: "",
                        scope: "",
                        authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                        redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")
                        );
                        break;
                    }
                case "MICROSOFT":
                    {
                        auth = new OAuth2Authenticator(
                        clientId: "MY ID",
                        scope: "bingads.manage",
                        authorizeUrl: new Uri("https://login.live.com/oauth20_authorize.srf?client_id=myid&scope=bingads.manage&response_type=token&redirect_uri=https://login.live.com/oauth20_desktop.srf"),
                        redirectUrl: new Uri("https://www.testauth.com")
                        );
                        break;
                    }
                case "Github":
                    {
                        auth = new OAuth2Authenticator(
                                "ClientId",
                               "ClientSecret",
                                "",
                                new Uri("https://github.com/login/oauth/authorize"),
                                new Uri("http://www.devenvexe.com"),
                                new Uri("https://github.com/login/oauth/access_token")
                                );

                        break;
                    }
            }
            return auth;
        }

    }
}
