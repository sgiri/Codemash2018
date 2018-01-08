using System;
using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Firebase.Auth;
using TestAuth;
using TestAuth.Droid;
using Android.Gms.Tasks;
using TestAuth.Model;

[assembly:ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]
namespace TestAuth.Droid
{
    public class LoginRenderer : PageRenderer, IOnCompleteListener, IOnSuccessListener
    {      
        bool publishLoginDone = false;
        bool silentSignIn = false;
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            // check if the user is signed in
            FirebaseAuth mAuth2 = FirebaseAuth.Instance;
            FirebaseUser currentUser = mAuth2.CurrentUser;

            if (currentUser != null)
            {
                silentSignIn = true;
                // signed into Firebase, so get the token and save it in user object
                if (AppManager.User == null)
                {
                    AppManager.User = new UserDetails();
                    AppManager.User.UserId = currentUser.Uid;
                    var tokenResult = currentUser.GetToken(false);
                    tokenResult.AddOnSuccessListener(this);
                }
                else
                {
                    if (!publishLoginDone)
                    {
                        Xamarin.Forms.MessagingCenter.Send("NavigateToNextPage", "LoginDone");
                        publishLoginDone = true;
                    }

                    AppManager.GetFavoritesFromFirebase(AppManager.User.UserId);
                }
            }
            else
            {
                try
                {
                    var credential = TwitterAuthProvider.GetCredential(App.token, App.tokensecret);                                   
                    FirebaseAuth mAuth = FirebaseAuth.Instance;
                    Android.Gms.Tasks.Task signedInTask = mAuth.SignInWithCredential(credential);
                    signedInTask.AddOnCompleteListener(this);
                }
                catch (Exception ex)
                {
                    string j = ex.Message;
                }

            }
                   
        }

        public  void OnComplete(Android.Gms.Tasks.Task task)
        {
            try
            {
                FirebaseAuth mAuth = FirebaseAuth.Instance;
                if (task.IsSuccessful)
                {
                    FirebaseUser user = mAuth.CurrentUser;
                    string k = user.ProviderId;
                    AppManager.User.UserId = user.Uid;
                    var u = user.GetToken(false);
                    u.AddOnSuccessListener(this);
                }
                else
                {
                   //   Toast.MakeText(this, "Authentication failed.", ToastLength.Short).Show();
                }
            }
            catch(Java.Lang.Exception ex)
            {
                string j = ex.Message;
            }          
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            GetTokenResult t = result as GetTokenResult;
            AppManager.User.FirebaseToken = t.Token;    
            
            if (!publishLoginDone && !silentSignIn)
            {
                Xamarin.Forms.MessagingCenter.Send("NavigateToNextPage", "LoginDone");
                publishLoginDone = true;
            }

            AppManager.GetFavoritesFromFirebase(AppManager.User.UserId);
        }


        private void signOut()
        {
            FirebaseAuth mAuth = FirebaseAuth.Instance;
            mAuth.SignOut();
        }
    }
}
