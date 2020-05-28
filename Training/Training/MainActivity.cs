using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Training.Model;
using Android.Content.PM;
using Android.Util;
using Android.Support.Design.Widget;
using Java.Security;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;
using Android.Content;
using System.Collections.Generic;
using Java.Interop;

namespace Training
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, IFacebookCallback
    {
        ICallbackManager mCallBackManager;
        MyProfileTracker mProfileTracker;

        TextView usernameText, emailText, photoText;
        LoginButton facebookLoginButton;



        //int permsRequestCode = 200;
        //String[] perms = { "android.permission.FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION" };
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            SetContentView(Resource.Layout.FaceBooklayout);

            mProfileTracker = new MyProfileTracker();
            mProfileTracker.mOnProfileChanged += MProfileTracker_mOnProfileChanged;
            mProfileTracker.StartTracking();
            Button facebookButton = FindViewById<Button>(Resource.Id.button1);
            if (AccessToken.CurrentAccessToken != null)
            {
                facebookButton.Text = "Logged out";
            }


            // Set our view from the "main" layout resource
            //facebookLoginButton = FindViewById<LoginButton>(Resource.Id.loginButton1);
            usernameText = FindViewById<TextView>(Resource.Id.usernameText);
            //facebookLoginButton.SetReadPermissions("user_friends");

            mCallBackManager = CallbackManagerFactory.Create();
            //facebookLoginButton.RegisterCallback(mCallBackManager, this);


            LoginManager.Instance.RegisterCallback(mCallBackManager, this);
            facebookButton.Click += (o, e) =>
              {
                  
                  if (AccessToken.CurrentAccessToken != null)
                  {
                      LoginManager.Instance.LogOut();
                      facebookButton.Text = "Log in";
                  }
                  else
                  {
                      LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "public_profile", "user_friends" });
                      facebookButton.Text = "Log OUT";
                  }
              };



            //PackageInfo info = this.PackageManager.GetPackageInfo("com.companyname.training", PackageInfoFlags.Signatures);
            //foreach(Android.Content.PM.Signature signature in info.Signatures)
            //{
            //    MessageDigest md = MessageDigest.GetInstance("SHA");
            //    md.Update(signature.ToByteArray());

            //    string keyhash = Convert.ToBase64String(md.Digest());
            //    Console.WriteLine("KeyHash",keyhash);
            //}

        }

        private void MProfileTracker_mOnProfileChanged(object sender, OnProfrofileChangedArgs e)
        {

             usernameText.Text=e.mProfile.FirstName;
        }

        public void OnCancel()
        {
            //  throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            //throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var a = result as LoginResult;
         
            LoginResult loginResult = result as LoginResult;
           // usernameText.Text = loginResult.AccessToken.UserId;
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
    public class MyProfileTracker:ProfileTracker
    {
        public event EventHandler<OnProfrofileChangedArgs> mOnProfileChanged;
        
        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile currentProfile)
        {
            if (mOnProfileChanged != null)
            {

             mOnProfileChanged.Invoke(this, new OnProfrofileChangedArgs(currentProfile));
            }
        }
    }
    public class OnProfrofileChangedArgs:EventArgs
    {
        public Profile mProfile;
        public OnProfrofileChangedArgs(Profile profile)
        {
            mProfile = profile;
        }
    }
}