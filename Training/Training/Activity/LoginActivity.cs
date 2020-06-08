using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;

using Android.Gms.Common;
using Android.Util;




using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Newtonsoft.Json;
using Org.Json;
using Training.Model;
using Xamarin.Auth;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace Training.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/LoginTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity, IFacebookCallback,GraphRequest.IGraphJSONObjectCallback, IOnSuccessListener,IOnFailureListener

    {

        ImageButton btn_SigninGoogleButton;
        GoogleSignInOptions gso;
        GoogleApiClient googleApiClient;


        FirebaseAuth firebaseAuth;
        int fb_requestcode=2;
        ICallbackManager mCallBackManager;
        MyProfileTracker mProfileTracker;
        ImageButton btn_fb,btn_twitter;
        string Name;
        Button btnLogin;
        EditText email, password;
        CheckBox mCbxRemMe;

        Android.App.AlertDialog.Builder dialog;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          //  await TryToGetPermission();
            FacebookSdk.SdkInitialize(this.ApplicationContext);


            SetContentView(Resource.Layout.LoginLayout);
          


            btn_fb = FindViewById<ImageButton>(Resource.Id.imageButton1);
            btn_twitter = FindViewById<ImageButton>(Resource.Id.imageButton2);
            btn_SigninGoogleButton = FindViewById<ImageButton>(Resource.Id.imageButton3);
            mCallBackManager = CallbackManagerFactory.Create();


            LoginManager.Instance.RegisterCallback(mCallBackManager, this);
            btn_fb.Click += (o, e) =>
            {

                LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "public_profile", "user_friends","email" });

            };


                email = FindViewById<EditText>(Resource.Id.editText_email);
            password = FindViewById<EditText>(Resource.Id.editText_password);
            btnLogin = FindViewById<Button>(Resource.Id.buttonlogin);
            mCbxRemMe = FindViewById<CheckBox>(Resource.Id.cbxRememberMe);
            dialog = new Android.App.AlertDialog.Builder(this);



            gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
               .RequestIdToken("823954020212-pous4rh94fc1i9rm1i3g46fi2j4ot4q1.apps.googleusercontent.com")
               .RequestEmail()
               .Build();

            googleApiClient = new GoogleApiClient.Builder(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso).Build();
            googleApiClient.Connect();

           firebaseAuth = GetFirebaseAuth();


            btn_twitter.Click += Btn_twitter_Click;
            //  btnLogin.Click += BtnLogin_Click;
        }



        #region Twitter signin
        private void Btn_twitter_Click(object sender, EventArgs e)
        {

            var auth = new OAuth1Authenticator(
                                consumerKey: "oT9PQ1bWS5opHVGolCbkDAUEX",
                                consumerSecret: "652Tmze5cpPZyomaW4TAud5zDvX45ZHaothgbfSje4I6CMUZKs",
                                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                                callbackUrl: new Uri("http://mobile.twitter.com"));
            auth.AllowCancel = true;
            auth.Completed += twitter_auth_Completed;
            var ui = auth.GetUI(this);
            StartActivity(ui);
         

        }

        private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                Toast.MakeText(this, "Logged-In", ToastLength.Short).Show();
                //Finish();
            }
        }
        #endregion


        #region google signin
        private FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(this);
            FirebaseAuth mAuth;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("training-ea64f")
                    .SetApplicationId("training-ea64f")
                    .SetApiKey("AIzaSyAYuOLei2bceIwSMgSVf5boMJPMnWxOS4c")
                    .SetDatabaseUrl("https://training-ea64f.firebaseio.com")
                    .SetStorageBucket("training-ea64f.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(this, options);
                mAuth = FirebaseAuth.Instance;
            }
            else
            {
                mAuth = FirebaseAuth.Instance;
            }
            return mAuth;
        }



        private void SigninButton_Click(object sender, System.EventArgs e)
         {

            //if (firebaseAuth.CurrentUser == null)
            //{
            //    var intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            //    StartActivityForResult(intent, 1);
            //}
            //else
            //{
            //    firebaseAuth.SignOut();
            //}

            var intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
               StartActivityForResult(intent, 1);     
        }
        private void LoginWithFirebase(GoogleSignInAccount account)
        {
            var credentials = GoogleAuthProvider.GetCredential(account.IdToken, null);
            firebaseAuth.SignInWithCredential(credentials).AddOnSuccessListener(this)
                .AddOnFailureListener(this);
        }


        #endregion

        protected override void OnResume()    
        {
            base.OnResume();
            this.btnLogin.Click += this.BtnLogin_Click;
            btn_SigninGoogleButton.Click += SigninButton_Click;

        }
        protected override void OnPause()
        {
            base.OnPause();
            this.btnLogin.Click -= this.BtnLogin_Click;
            btn_SigninGoogleButton.Click -= SigninButton_Click;

        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DashboardActivity));

            if (CheckValidation(email.Text, password.Text))
            {
                User user = new User()
                {
                    Email = email.Text,
                    Password = password.Text
                };

                intent.PutExtra("User", JsonConvert.SerializeObject(user));

                if (mCbxRemMe.Checked)
                {
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Email", email.Text.Trim());
                    edit.PutString("Password", password.Text.Trim());
                    edit.Apply();
                }
                this.StartActivity(intent);
                this.Finish();
            }
        }
        public bool CheckValidation(string email, string password)
        {
            Android.App.AlertDialog alert = dialog.Create();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter All Fields");
                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.Show();
                return false;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter Valid Email");

                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.SetButton2("CANCEL", (c, ev) =>
                {

                });
                alert.Show();
                return false;
            }
            else
            {
                return true;
                //alert.SetTitle("Login");
                //alert.SetMessage("Success");

                //alert.SetButton("OK", (c, ev) =>
                //{
                //    Intent intent = new Intent(this, typeof(VerificationCodeActivity));
                //    StartActivity(intent);

                //   intent.PutExtra("email", userObj.Email);
                //    StartActivity(intent);
                //});
                //alert.SetButton2("CANCEL", (c, ev) =>
                //{

                //});
                //alert.Show();
            }
        }



        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 64206)
            {
                mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
            else if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                if (result.IsSuccess)
                {
                    GoogleSignInAccount account = result.SignInAccount;
                    LoginWithFirebase(account);

                }
            }
        }


        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            GraphRequest request = GraphRequest.NewMeRequest(AccessToken.CurrentAccessToken, this);
            Bundle parameters = new Bundle();
            parameters.PutString("fields", "id,name,age_range,email");
            request.Parameters = parameters;
            request.ExecuteAsync();
        }
        public void OnCompleted(JSONObject json, GraphResponse response)
        {
            Intent intent;
            if (json != null)
            {
                string data = json.ToString();
                FacebookResult result = JsonConvert.DeserializeObject<FacebookResult>(data);
                Console.WriteLine(json.ToString());
                Name = result.email;
            }
                intent = new Intent(this, typeof(DashboardActivity));
                intent.PutExtra("Name", Name);
                this.StartActivity(intent);
           


        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }



       // #region permisson
       // async Task TryToGetPermission()
       // {
       //     if ((int)Build.VERSION.SdkInt >= 23)
       //     {
       //         await GetPermission();
       //         return;
       //     }
       // }

       // const int RequestLocationId = 0;

       // readonly string[] PermissionGroupLocation =
       //{
       //     Manifest.Permission.AccessCoarseLocation,
       //     Manifest.Permission.AccessFineLocation
       // };

       // async Task GetPermission()
       // {
       //     string permission = Manifest.Permission.AccessFineLocation;

       //     if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
       //     {
       //         Toast.MakeText(this, "Loaction Permission Granted", ToastLength.Short).Show();
       //         return;
       //     }

       //     if (ShouldShowRequestPermissionRationale(permission))
       //     {
       //         Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
       //         alert.SetTitle("Permissions Needed");
       //         alert.SetMessage("The application need special permissions to continue");
       //         alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
       //         {
       //             RequestPermissions(PermissionGroupLocation, RequestLocationId);
       //         });

       //         alert.SetNegativeButton("Cancel", (senderAlert, args) =>
       //         {
       //             Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
       //         });

       //         Dialog dialog = alert.Create();
       //         dialog.Show();


       //         return;
       //     }

       //     RequestPermissions(PermissionGroupLocation, RequestLocationId);
       // }

       // public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
       // {
       //     switch (requestCode)
       //     {
       //         case RequestLocationId:
       //             {
       //                 if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
       //                 {
       //                     Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();

       //                 }
       //                 else
       //                 {

       //                     Toast.MakeText(this, "Special permissions denied", ToastLength.Short).Show();

       //                 }
       //             }
       //             break;
       //     }

       // }

       // #endregion
    }
}