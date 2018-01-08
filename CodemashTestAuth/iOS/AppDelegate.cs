using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Firebase.Auth;
using Google.SignIn;

namespace TestAuth.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();

            Firebase.Core.App.Configure();

            // You can get the GoogleService-Info.plist file at https://developers.google.com/mobile/add
            var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");
            SignIn.SharedInstance.ClientID = googleServiceDictionary["CLIENT_ID"].ToString();

            SignIn.SharedInstance.SignOutUser();


            ////
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }


        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var openUrlOptions = new UIApplicationOpenUrlOptions(options);
            return SignIn.SharedInstance.HandleUrl(url, openUrlOptions.SourceApplication, openUrlOptions.Annotation);
        }

        // For iOS 8 and older
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return SignIn.SharedInstance.HandleUrl(url, sourceApplication, annotation);
        }
    }
}
