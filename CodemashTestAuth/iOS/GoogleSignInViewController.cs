using System;
using System.Net.Http;
using System.Text;
using CoreGraphics;
using Firebase.Auth;
using Foundation;
using Google.SignIn;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TestAuth;
using TestAuth.Model;


[assembly: ExportRenderer(typeof(TestAuth.View.GoogleSignIn), typeof(ConfApp.iOS.Views.GoogleSignInViewController))]

namespace ConfApp.iOS.Views
{
    public class GoogleSignInViewController : PageRenderer, ISignInDelegate, ISignInUIDelegate
    {
        void HandleAuthTokenHandler(string token, NSError error)
        {
            string key = token;

            using (var httpClient = new HttpClient())
            {
          //      string key = "AEoYo8s6ZORLE3u4YeY17RuXPA1opMzyjziq4OS59DdOaXgLfFd4PH3EwmJdi2SEpJFxAFwqm-WiTKpuBWhljMIBxCLHcBjFdWXCBdGFvl94cBIPp001JeLunNX8VPFnJeKiBRFcT2_56R9YMvz7yjRBxuncJTknRTTVJtc-6UcxKmrFPu0kv30KrkJH4BGEeppdfqYyU5NmIsgrJFfpIbg8QufBFw_Ig0ktC-90RSAWuZciJsIzzc_AcCX8Q_5FeagMHEDgsoDHCBYx6-wfn3-Xx4YnB3UFG9W8CbIKw1FxOq22Sz26vdJ2NIABrJVDGiW-qIvS2Os4jIHw0VIg7tWNEQclMDorXb8iL5G6MKAPVyOcFQrvG0P91rRJMGqwk3nbkCdC3lKk";
             //
                string url = "https://testauth-49443.firebaseio.com/users/0/SessionIds/.json?auth=" + key;
               
                var uri = new Uri(url);
                var jsonstr = JsonConvert.SerializeObject("9");
                var content2 = new System.Net.Http.StringContent(jsonstr, Encoding.UTF8, "application/json");

                try
                {
                    var response = httpClient.PutAsync(uri, content2).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string responseString = responseContent.ReadAsStringAsync().Result;
                     //   var info = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    string h1 = ex.Message;

                }
            }
                
        }

        public GoogleSignInViewController()
        {
        }

        private SignInButton BtnSignIn;


        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                BtnSignIn = new SignInButton();
                BtnSignIn.Frame = new CGRect(20, 100, 150, 44);
           //     View.AddSubview(BtnSignIn);
           //     BtnSignIn.Enabled = true;
                SignIn.SharedInstance.ClientID = Firebase.Core.App.DefaultInstance.Options.ClientId;
                SignIn.SharedInstance.Delegate = this;
                SignIn.SharedInstance.UIDelegate = this;
                SignIn.SharedInstance.SignInUser();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"          ERROR: ", ex.Message);
            }
        }

        //public override void ViewDidLoad()
        //{
        //    base.ViewDidLoad();
        //    // Perform any additional setup after loading the view, typically from a nib.
        //    BtnSignIn = new SignInButton();
        //    BtnSignIn.Frame = new CGRect(20, 100, 150, 44);
        //    View.AddSubview(BtnSignIn);
        //    BtnSignIn.Enabled = false;
        //    SignIn.SharedInstance.ClientID = Firebase.Core.App.DefaultInstance.Options.ClientId;
        //    SignIn.SharedInstance.Delegate = this;
        //    SignIn.SharedInstance.UIDelegate = this;
        //    SignIn.SharedInstance.SignInUserSilently();
        //}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void DidSignIn(SignIn signIn, GoogleUser user, NSError error)
        {
            
            if (error == null && user != null)
            {
                // Get Google ID token and Google access token and exchange them for a Firebase credential
                var authentication = user.Authentication;
                var credential = GoogleAuthProvider.GetCredential(authentication.IdToken, authentication.AccessToken);

             //   Auth.DefaultInstance.CurrentUser.Link(credential, LinkCompletion);
                // Authenticate with Firebase using the credential
                Auth.DefaultInstance.SignIn(credential, SignInOnCompletion);

            }
            else
            {
                BtnSignIn.Enabled = true;
            //    AppDelegate.ShowMessage("Could not login!", error.LocalizedDescription, NavigationController);
            }
        }

        [Export("signIn:didDisconnectWithUser:withError:")]
        public void DidDisconnect(SignIn signIn, GoogleUser user, NSError error)
        {
            NavigationController.PopToRootViewController(true);
        }


        void LinkCompletion(User user, NSError error)
        {
            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignIn method with credentials could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.InvalidCredential:
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.EmailAlreadyInUse:
                    case AuthErrorCode.UserDisabled:
                    case AuthErrorCode.WrongPassword:
                    default:
                       // AppDelegate.ShowMessage("Could not login!", error.LocalizedDescription, NavigationController);
                        break;
                }
            }

            string t = user.Uid;
            AppManager.GetFavoritesFromFirebase(user.Uid);
        }
       

        void SignInOnCompletion(User user, NSError error)
        {
            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignIn method with credentials could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.InvalidCredential:
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.EmailAlreadyInUse:
                    case AuthErrorCode.UserDisabled:
                    case AuthErrorCode.WrongPassword:
                    default:
             //           AppDelegate.ShowMessage("Could not login!", error.LocalizedDescription, NavigationController);
                        break;
                }

                return;
            }

            string h = user.Uid;
            AppManager.GetFavoritesFromFirebase(user.Uid);

         //   user.GetIdToken(HandleAuthTokenHandler);

            Xamarin.Forms.MessagingCenter.Send("NavigateToNextPage", "LoginDone");
            //NavigationController.PushViewController(new UserViewController("Google"), true);
        }
    }
}
