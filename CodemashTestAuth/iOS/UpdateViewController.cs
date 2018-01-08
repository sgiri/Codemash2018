using System;
using System.Net.Http;
using System.Text;
using CoreGraphics;
using Firebase.Auth;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using TestAuth;
using TestAuth.View;
using TestAuth.Model;


[assembly: ExportRenderer(typeof(UpdateDataPage), typeof(TestAuth.iOS.UpdateViewController))]
namespace TestAuth.iOS
{
    public partial class UpdateViewController : PageRenderer
    {
        
        string favSessions = string.Empty;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            OAuthProviderSetting oauth = new OAuthProviderSetting();
            bool signin = true;
            if (signin)
            {
                try
                {
                    var credential = TwitterAuthProvider.GetCredential(App.token, App.tokensecret);
                    Auth.DefaultInstance.SignIn(credential, SignInOnCompletion);
                }
                catch (Exception ex)
                {
                    string j = ex.Message;
                }

            }
            else
            {

            }
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
            if(  AppManager.User == null)
            {
                AppManager.User = new UserDetails();
            }
            AppManager.User.UserId = user.Uid;
            user.GetIdToken(HandleAuthTokenHandler);
                   
        }


        void HandleAuthTokenHandler(string token, NSError error)
        {
            if (AppManager.User == null)
            {
                AppManager.User = new UserDetails();
            }
            AppManager.User.FirebaseToken = token;
            favSessions = App.FormattedFavsessionIDs;
            AppManager.SaveFavoritesToFirebase(AppManager.User.UserId, token, favSessions);

        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }



    }
}
