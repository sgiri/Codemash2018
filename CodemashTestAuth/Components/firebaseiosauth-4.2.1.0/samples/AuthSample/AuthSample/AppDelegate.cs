﻿using Foundation;
using UIKit;
using System;
using Firebase.Core;
using Google.SignIn;
using Facebook.CoreKit;
using Firebase.Auth;
using CoreTelephony;
using UserNotifications;
using Firebase.CloudMessaging;

namespace AuthSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
	{
		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}

		// ClientID can be found in the GoogleService-Info.plist file
		// You can get the GoogleService-Info.plist file at https://developers.google.com/mobile/add
		//const string clientId = "<Your ClientID>";
		const string clientId = "542613023302-hpssce1aq4tbarcpj8qoagcsss9riffd.apps.googleusercontent.com";

		// Replace here you own Facebook App Id and App Name, if you don't have one go to
		// https://developers.facebook.com/apps
		//string appId = "Your_Id_Here";
		//string appName = "Your_App_Display_Name";
		string appId = "765057006871425";
		string appName = "XamTest";

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			(Window.RootViewController as UINavigationController).PushViewController (new MenuViewController (), true);

			App.Configure ();

			if (UIDevice.CurrentDevice.CheckSystemVersion (10, 0)) {
				UNUserNotificationCenter.Current.Delegate = this;
				Messaging.SharedInstance.Delegate = this;
				Messaging.SharedInstance.ShouldEstablishDirectChannel = true;
			}

			Settings.AppID = appId;
			Settings.DisplayName = appName;

			// This method verifies if you have been logged to Facebook into the app before, and keep you logged in after you reopen or kill your app.
			return ApplicationDelegate.SharedInstance.FinishedLaunching (application, launchOptions);
		}

		[Export ("messaging:didReceiveMessage:")]
		public void DidReceiveMessage (Messaging messaging, RemoteMessage remoteMessage)
		{
			
		}

		public void DidRefreshRegistrationToken (Messaging messaging, string fcmToken)
		{
			
		}

		public override void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			
		}

		// Support for iOS 9 or later
		public override bool OpenUrl (UIApplication app, NSUrl url, NSDictionary options)
		{
			var openUrlOptions = new UIApplicationOpenUrlOptions (options);
			return OpenUrl (app, url, openUrlOptions.SourceApplication, openUrlOptions.Annotation);
		}

		// Support for iOS 8 or before
		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// Handle Sign In
			var result = SignIn.SharedInstance.HandleUrl (url, sourceApplication, annotation);

			if (result)
				return result;
			
			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
		}

		public static void ShowMessage (string title, string message, UIViewController fromViewController, Action okAction = null)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var alert = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (obj) => okAction?.Invoke ()));
				fromViewController.PresentViewController (alert, true, null);
			} else {
				var alert = new UIAlertView (title, message, null, "Ok", null);
				alert.Canceled += (sender, e) => okAction?.Invoke ();
				alert.Show ();
			}
		}

		public static void ShowMessage (string title, string message, UIViewController fromViewController, string cancelTitle, Action cancelAction, string otherTitle, Action otherAction)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var alert = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);
				var btnCancel = UIAlertAction.Create (cancelTitle, UIAlertActionStyle.Default, (obj) => cancelAction?.Invoke ());
				var btnOther = UIAlertAction.Create (otherTitle, UIAlertActionStyle.Default, (obj) => otherAction?.Invoke ());

				alert.AddAction (btnCancel);
				alert.AddAction (btnOther);
				alert.PreferredAction = btnOther;

				fromViewController.PresentViewController (alert, true, null);
			} else {
				var alert = new UIAlertView (title, message, null, cancelTitle, otherTitle);
				alert.Dismissed += (sender, e) => {
					var a = (UIAlertView)sender;

					if (e.ButtonIndex == a.CancelButtonIndex)
						cancelAction?.Invoke ();
					else
						otherAction?.Invoke ();
					};
				alert.Show ();
			}
		}
	}
}

